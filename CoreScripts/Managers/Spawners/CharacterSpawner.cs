using System;
using System.Collections.Generic;
using Factory;
using Pool;
using UnityEngine;

public class CharacterSpawner<T> : GameSpawner where T : AbstractViewEntity
{
    public override Action OnSpawnerReady { get; set; }

    protected Dictionary<Type, string> characterPaths;

    private const string POOL_NAME = "Pool";
    private string charactersPrefabPath;

    //private EnemyBehaviourManager enemyBehaviourManager;
    private PooleableFactory<T> characterFactory;
    private Transform container;

    public CharacterSpawner(string charactersPrefabPath, Transform container, Action OnSpawnerReady = null, bool createInitPool = true)
    {
        this.OnSpawnerReady += OnSpawnerReady;
        GameObject concreteContainer = new GameObject(typeof(T).ToString() + " " + POOL_NAME);
        concreteContainer.transform.SetParent(container);
        this.container = concreteContainer.transform;
        this.charactersPrefabPath = charactersPrefabPath;
        this.characterPaths = new Dictionary<Type, string>();
        this.SetDictionary();
        //this.enemyBehaviourManager = new EnemyBehaviourManager();
        if (createInitPool)
            this.characterFactory = new PooleableFactory<T>(CreateCharacter, this.OnInitialPoolFinished);
        else
            this.characterFactory = new PooleableFactory<T>(CreateCharacter, 0, this.OnInitialPoolFinished);
    }

    public T SpawnCharacter()
    {
        T character = this.characterFactory.Create();
        character.OnReturnedItem += ReturnBaseCharacter;
        character.transform.SetParent(null);
        character.EnableObject();
        // enemy.ConfigureBehaviour(new EnemyBehaviour(this.enemyBehaviourManager.SetEnemyBehaviour(StrategicPlans.Follow)));
        return character;
    }

    private void SetDictionary()
    {
        this.characterPaths[typeof(T)] = this.charactersPrefabPath + typeof(T).ToString();
    }

    private T CreateCharacter()
    {
        T character = (GameObject.Instantiate<T>((Resources.Load<T>(this.characterPaths[typeof(T)]))));
        character.transform.SetParent(this.container);
        character.OnReturnedItem += ReturnBaseCharacter;
        character.DisableObject();
        return character;
    }

    private void ReturnBaseCharacter(IPooleable character)
    {
        Transform characterTransform = (Transform)character;
        characterTransform.SetParent(this.container);
        character.OnReturnedItem -= ReturnBaseCharacter;
        character.DisableObject();
        this.characterFactory.ReturnPoolItem((T)character);
    }

    private void OnInitialPoolFinished()
    {
        this.OnSpawnerReady?.Invoke();
    }
}