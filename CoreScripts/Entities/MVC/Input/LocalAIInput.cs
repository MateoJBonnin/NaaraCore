using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using System.Linq;

public class LocalAIInput : MonoBehaviour, IInputControllable
{
    public LogicEntity LogicEntity { get; set; }

    private Dictionary<AIState, Func<FSMState>> entityDefaultTypeToAction;
    private GenericFSM<AIState> aiInputFSM { get; set; }
    private GameCoroutineManager coroutineManager;
    private AIBlackboard aIBlackboard;

    //TODO: check this, this may come from who initialices the AI, because it will be hardcodedfor now
    private AbstractAIPlanResolver aiplanResolver;
    private DefaultAIActionResolver aiActionResolver;
    private AbstractAIThinker aiThinker;
    //change this to be called upon use only
    private AIPatrolSetup patrolSetup;
    //

    public void SetLogic(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
        List<PathNode> randomMapNodes = ManagersService.instace.GetManager<GameMap>().mapManager.GetNodes(new MapNodesRandomRequester<PathNode>(2, new RepeatAllowedRequesterPolicy<PathNode>(), new DistanceBasedFilterZone<PathNode>(this.LogicEntity.ViewEntity, 25)));

        this.patrolSetup = new AIPatrolSetup(new List<AIPatrolPosition>(randomMapNodes.Select(pathNode => new AIPatrolPosition(pathNode)).ToList()), new InstantPatrolTimePolicy(), new FixedListedPositionAIPatrolBehaviour(this.LogicEntity, new OrderedPatrolCoordinator(this.LogicEntity)));
        this.aIBlackboard = new AIBlackboard(this.LogicEntity, new AIBlackboardSetup(patrolSetup));
        this.ThinkActions();
    }

    private void Start()
    {
        this.coroutineManager = GameCoroutineManager.instance;

        this.ThinkActions();
        this.ResolveAIComponents();
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
            this.StopAllCoroutines();
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
        this.entityDefaultTypeToAction = new Dictionary<AIState, Func<FSMState>>();
        this.SetConfigConnectionStates();
        FSMConfig<AIState> AIStateConfig = new FSMConfig<AIState>(this.GetAIFSMConfigData(), this.GetAIPlanStates());
        this.aiInputFSM = new GenericFSM<AIState>(AIStateConfig, new FSMRestrictedTransitioner<AIState>(AIStateConfig));
        this.aiInputFSM.Feed(AIState.Idle);
    }

    private JSONObject GetAIFSMConfigData()
    {
        JSONObject configData = new JSONObject();

        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Idle, AIState.Idle));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Idle, AIState.Resolve));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Resolve, AIState.Fail));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Fail, AIState.Idle));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Fail, AIState.Resolve));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Resolve, AIState.Executing));
        configData.Add(FSMConfig<AIState>.StateToConfig(AIState.Executing, AIState.Finished));

        return configData;
    }

    private Dictionary<AIState, FSMState> GetAIPlanStates()
    {
        Dictionary<AIState, FSMState> entityActions = new Dictionary<AIState, FSMState>();

        for (int i = 0, count = (int)AIState.Count; i < count; i++)
            entityActions[(AIState)i] = this.entityDefaultTypeToAction[(AIState)i]();

        return entityActions;
    }

    private void Update()
    {
        this.LogicEntity.Update();
        this.aiplanResolver.UpdatePlan();
        this.aiInputFSM?.Update();
        this.aIBlackboard.UpdateAIBackboard();
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