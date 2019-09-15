using System;
using System.Collections.Generic;
using Factory;
using Pool;
using UnityEngine;

public class CharacterSpawner<T> : GameSpawner where T : AbstractViewEntity
{
    public override Action OnSpawnerReady { get; set; }

    protected string characterPath;

    private const string POOL_NAME = "Pool";
    private string charactersPrefabPath;

    private PooleableFactory<T> characterFactory;
    private Transform container;

    public CharacterSpawner(string charactersPrefabPath, Transform container, Action OnSpawnerReady = null, bool createInitPool = true)
    {
        this.OnSpawnerReady += OnSpawnerReady;
        GameObject concreteContainer = new GameObject(typeof(T).ToString() + " " + POOL_NAME);
        concreteContainer.transform.SetParent(container);
        this.container = concreteContainer.transform;
        this.charactersPrefabPath = charactersPrefabPath;
        this.SetDictionary();
        if (createInitPool)
            this.characterFactory = new PooleableFactory<T>(this.CreateCharacter, this.OnInitialPoolFinished);
        else
            this.characterFactory = new PooleableFactory<T>(this.CreateCharacter, 0, this.OnInitialPoolFinished);
    }

    public T SpawnCharacter()
    {
        T character = this.characterFactory.GetPoolItem();
        character.OnReturnedItem += this.ReturnBaseCharacter;
        character.transform.SetParent(null);
        character.EnableObject();
        GameEventSystem.instance.DispatchEvent(new ViewSpawnedEntityEvent() { viewEntity = character });
        return character;
    }

    private void SetDictionary()
    {
        this.characterPath = this.charactersPrefabPath + typeof(T).ToString();
    }

    private T CreateCharacter()
    {
        T character = (GameObject.Instantiate<T>((Resources.Load<T>(this.characterPath))));
        character.transform.SetParent(this.container);
        character.DisableObject();
        return character;
    }

    private void ReturnBaseCharacter(IPooleable character)
    {
        Transform characterTransform = ((AbstractViewEntity)character).transform;
        characterTransform.SetParent(this.container);
        character.OnReturnedItem -= this.ReturnBaseCharacter;
        character.DisableObject();
        this.characterFactory.ReturnPoolItem((T)character);
    }

    private void OnInitialPoolFinished()
    {
        this.OnSpawnerReady?.Invoke();
    }
}