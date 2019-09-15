using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using System.Linq;

public class LocalAIInput : AbstractInputEntity
{
    private Dictionary<AIState, Func<FSMState<EmptyFSMStateData>>> entityDefaultTypeToAction;
    private GenericFSM<AIState, EmptyFSMStateData> aiInputFSM { get; set; }
    private AIBlackboard aIBlackboard;

    //TODO: check this, this may come from who initialices the AI, because it will be hardcodedfor now
    private AbstractAIPlanResolver aiplanResolver;
    private DefaultAIActionResolver aiActionResolver;
    private AbstractAIThinker aiThinker;
    //change this to be called upon use only
    private AIPatrolSetup patrolSetup;
    //

    public LocalAIInput(AbstractInputController inputController) : base(inputController)
    {
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);
        List<PathNode> randomMapNodes = ManagersService.instance.GetManager<GameMap>().mapManager.GetNodes(new MapNodesRandomRequester<PathNode>(2, new RepeatAllowedRequesterPolicy<PathNode>(), new DistanceBasedFilterZone<PathNode>(this.LogicEntity.ViewEntity, 25)));

        this.patrolSetup = new AIPatrolSetup(new List<AIPatrolPosition>(randomMapNodes.Select(pathNode => new AIPatrolPosition(pathNode)).ToList()), new InstantPatrolTimePolicy(), new FixedListedPositionAIPatrolBehaviour(this.LogicEntity, new OrderedPatrolCoordinator(this.LogicEntity)));
        this.aIBlackboard = new AIBlackboard(this.LogicEntity, new AIBlackboardSetup(patrolSetup));
        this.ThinkActions();
        this.ResolveAIComponents();
    }

    public override AbstractInputEntityStateSnapshot TempGatherState()
    {
        return new LocalAIInputEntityStateSnapshot();
    }

    public override void UpdateInput()
    {
        this.aiplanResolver.UpdatePlan();
        this.aiInputFSM?.Update();
        this.aIBlackboard.UpdateAIBackboard();
    }

    private void ThinkActions()
    {
        this.aiActionResolver = new DefaultAIActionResolver(this.aIBlackboard);
        this.aiThinker = new AIGOAPThinker(this.aIBlackboard, new AIGOAPDecisionTreeThinker(new SimpleAIGoalDecisionTree().GetAIGOAPGoal(this.LogicEntity)));
        this.aiplanResolver = new AIChainPlanResolver(() => this.aiInputFSM.Feed(AIState.Fail));
    }

    private void SetConfigConnectionStates()
    {
        List<IAIAction> oderedToDoAction = null;
        SimpleFSMState idleState = new SimpleFSMState();
        SimpleFSMState resolveState = new SimpleFSMState();
        SimpleFSMState executingState = new SimpleFSMState();
        SimpleFSMState failedState = new SimpleFSMState();
        SimpleFSMState finishedState = new SimpleFSMState();

        idleState.OnEnterAction += () =>
        {
            this.aiInputFSM.Feed(AIState.Resolve);
        };

        resolveState.OnEnterAction += () =>
        {
            oderedToDoAction = this.aiThinker.ResolvePlan(this.aiActionResolver.GetAvailableActions);
            this.aiInputFSM.Feed(AIState.Executing);
        };

        failedState.OnEnterAction += () =>
        {
            //if formulating the plan failed look for another plan or add / change actions. 
            //this.StopAllCoroutines();
            this.aiInputFSM.Feed(AIState.Idle);
        };

        executingState.OnEnterAction += () =>
        {
            this.aiplanResolver.ExecutePlan(oderedToDoAction);
        };

        finishedState.OnEnterAction += () =>
        {
            this.aiInputFSM.Feed(AIState.Idle);
        };

        finishedState.OnExitAction += () =>
        {
            // idk like wait the "human time" to do another plan 
        };

        this.entityDefaultTypeToAction[AIState.Idle] = () => idleState;
        this.entityDefaultTypeToAction[AIState.Resolve] = () => resolveState;
        this.entityDefaultTypeToAction[AIState.Fail] = () => failedState;
        this.entityDefaultTypeToAction[AIState.Executing] = () => executingState;
        this.entityDefaultTypeToAction[AIState.Finished] = () => finishedState;
    }

    private void ResolveAIComponents()
    {
        this.entityDefaultTypeToAction = new Dictionary<AIState, Func<FSMState<EmptyFSMStateData>>>();
        this.SetConfigConnectionStates();
        FSMConfig<AIState, EmptyFSMStateData> AIStateConfig = new FSMConfig<AIState, EmptyFSMStateData>(this.GetAIFSMConfigData(), this.GetAIPlanStates());
        this.aiInputFSM = new GenericFSM<AIState, EmptyFSMStateData>(AIStateConfig, new FSMRestrictedTransitioner<AIState, EmptyFSMStateData>(AIStateConfig));
        this.aiInputFSM.Feed(AIState.Idle);
    }

    private FSMStateLinksData<AIState> GetAIFSMConfigData()
    {
        FSMStateLinksData<AIState> configData = new FSMStateLinksData<AIState>();

        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Idle, AIState.Idle));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Idle, AIState.Resolve));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Resolve, AIState.Fail));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Fail, AIState.Idle));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Fail, AIState.Resolve));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Resolve, AIState.Executing));
        configData.Add(FSMConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Executing, AIState.Finished));

        return configData;
    }

    private Dictionary<AIState, FSMState<EmptyFSMStateData>> GetAIPlanStates()
    {
        Dictionary<AIState, FSMState<EmptyFSMStateData>> entityActions = new Dictionary<AIState, FSMState<EmptyFSMStateData>>();

        for (int i = 0, count = (int)AIState.Count; i < count; i++)
            entityActions[(AIState)i] = this.entityDefaultTypeToAction[(AIState)i]();

        return entityActions;
    }
}

public enum AIState
{
    Idle,
    Resolve,
    Fail,
    Executing,
    Finished,
    Count
}