using System;
using System.Collections.Generic;

public class LocalAIInput : AbstractInputEntity
{
    public LogicEntity AILogicEntity { get; private set; }
    public AIBlackboard aIBlackboard;

    private Dictionary<AIState, Func<FSMState<EmptyFSMStateData>>> entityDefaultTypeToAction;
    private EmptyGenericFSM<AIState> aiInputFSM { get; set; }

    //TODO: check this, this may come from who initialices the AI, because it will be hardcodedfor now
    private AbstractAIActionResolver aiActionResolver;
    private AbstractAIPlanResolver aiplanResolver;
    private AbstractAIThinker aiThinker;


    //change this to be called upon use only
    //private AIPatrolSetup patrolSetup;
    //

    public LocalAIInput(AbstractInputController inputController, List<EntityInputLink> entityInputLinks, List<AIManager> managers) : base(inputController, entityInputLinks)
    {
        //TODO FIX THIS??
        this.AILogicEntity = inputController.LogicEntity;
        this.aIBlackboard = new AIBlackboard();

        for (int i = managers.Count - 1; i >= 0; i--)
            this.aIBlackboard.subManagerSystem.RegisterSubManager(new DefaultEntityManagerContainer(managers[i]));
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);
        List<PathNode> randomMapNodes = AILogicEntity.EntityBlackboard.gameplayController.gameplayManagers.GetManager<GameMap>().mapManager.GetNodes(new MapNodesRandomRequester<PathNode>(2, new RepeatAllowedRequesterPolicy<PathNode>(), new DistanceBasedFilterZone<PathNode>(25)));

        //TODO FIX THIS MAYBE THE PATROL CONTEXT CAN HAVE A PATROL SETUP
        //this.patrolSetup = new AIPatrolSetup(new List<AIPatrolPosition>(randomMapNodes.Select(pathNode => new AIPatrolPosition(pathNode)).ToList()), new InstantPatrolTimePolicy(), new FixedListedPositionAIPatrolBehaviour(this.inputController.LogicEntity, new OrderedPatrolCoordinator(this.inputController.LogicEntity)));
        //this.inputController.LogicEntity, new AIBlackboardSetup(patrolSetup));
        this.ThinkActions();
        this.ResolveAIComponents();
    }

    public override void UpdateInput()
    {
        this.aIBlackboard.UpdateAIBackboard();
        this.aiplanResolver.UpdatePlan();
        this.aiInputFSM?.Update();
    }

    private void ThinkActions()
    {
        //this.aiActionResolver = new DefaultAIActionResolver(this.aIBlackboard);
        this.aiThinker = new AIGOAPThinker(this, new AIGOAPDecisionTreeThinker(new SimpleAIGoalDecisionTree().GetAIGOAPGoal(this.inputController.LogicEntity)));
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
        EmptyFSMStateDatabase<AIState> aiStateDatabase = new EmptyFSMStateDatabase<AIState>(this.GetAIPlanStates());
        this.aiInputFSM = new EmptyGenericFSM<AIState>(aiStateDatabase, new EmptyFSMRestrictedTransitioner<AIState>(new EmptyFSMTransitionsConfig<AIState>(this.GetAIFSMConfigData(), aiStateDatabase)));
        this.aiInputFSM.Feed(AIState.Idle);
    }

    private FSMStateLinksData<AIState> GetAIFSMConfigData()
    {
        FSMStateLinksData<AIState> configData = new FSMStateLinksData<AIState>();

        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Idle, AIState.Idle));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Idle, AIState.Resolve));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Resolve, AIState.Fail));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Fail, AIState.Idle));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Fail, AIState.Resolve));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Resolve, AIState.Executing));
        configData.Add(DefaultFSMTransitionsConfig<AIState, EmptyFSMStateData>.StateToConfig(AIState.Executing, AIState.Finished));

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