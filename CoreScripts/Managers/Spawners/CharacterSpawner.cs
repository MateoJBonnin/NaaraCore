using Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner<ViewEntity> : GameSpawner where ViewEntity : global::ViewEntity
{
    public override Action OnSpawnerReady { get; set; }

    private const string POOL_NAME = "Pool";

    private List<AbstractViewEntityManagerContainer> entityManagers;
    private PooleableFactory<ViewEntity> characterFactory;
    private ApplicationController applicationController;
    private GameplayController gameplayController;
    private ViewEntity characterPrefab;
    private Transform poolContainer;
    private Transform prefabContainer;

    public CharacterSpawner(ViewEntity characterPrefab, List<AbstractViewEntityManagerContainer> entityManagers, GameplayController gameplayController, ApplicationController applicationController, Transform container, Transform prefabContainer, GameplayCoroutineManager gameplayCoroutineManager, Action OnSpawnerReady = null, bool createInitPool = true)
    {
        this.OnSpawnerReady += OnSpawnerReady;
        GameObject concreteContainer = new GameObject(typeof(ViewEntity).ToString() + " " + POOL_NAME);
        concreteContainer.transform.SetParent(container);
        this.poolContainer = concreteContainer.transform;
        this.prefabContainer = prefabContainer;
        this.entityManagers = entityManagers;
        this.characterPrefab = characterPrefab;
        this.applicationController = applicationController;
        this.gameplayController = gameplayController;
        if (createInitPool)
            this.characterFactory = new PooleableFactory<ViewEntity>(this.CreateCharacter, gameplayCoroutineManager, this.OnInitialPoolFinished);
        else
            this.characterFactory = new PooleableFactory<ViewEntity>(this.CreateCharacter, gameplayCoroutineManager, 0, this.OnInitialPoolFinished);
    }

    public ViewEntity SpawnCharacter()
    {
        ViewEntity character = this.characterFactory.GetPoolItem();
        character.OnReturnedItem += this.ReturnBaseCharacter;
        character.transform.SetParent(this.prefabContainer);
        character.EntityBlackboard.subManagerSystem.GetAllSubManagers().ForEach(manager => manager.ViewEntity = character);
        character.EntityBlackboard.subManagerSystem.SetReadyAllManagers();
        character.EntityBlackboard.subManagerSystem.InitAllManagers();
        character.EnableObject();
        return character;
    }

    private ViewEntity CreateCharacter()
    {
        ViewEntity character = (GameObject.Instantiate<ViewEntity>(this.characterPrefab as ViewEntity));
        character.EntityBlackboard = new ViewEntityBlackboard(this.entityManagers, this.applicationController, this.gameplayController);
        character.transform.SetParent(this.poolContainer);
        character.DisableObject();
        return character;
    }

    private void ReturnBaseCharacter(IPooleable character)
    {
        Transform characterTransform = ((global::ViewEntity)character).transform;
        characterTransform.SetParent(this.poolContainer);
        character.OnReturnedItem -= this.ReturnBaseCharacter;
        character.DisableObject();
        this.characterFactory.ReturnPoolItem((ViewEntity)character);
    }

    private void OnInitialPoolFinished()
    {
        this.OnSpawnerReady?.Invoke();
    }
}