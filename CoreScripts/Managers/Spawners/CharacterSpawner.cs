using Pool;
using System;
using UnityEngine;

public class CharacterSpawner<T> : GameSpawner where T : AbstractViewEntity
{
    public override Action OnSpawnerReady { get; set; }

    private const string POOL_NAME = "Pool";

    private PooleableFactory<T> characterFactory;
    private T characterPrefab;
    private Transform poolContainer;
    private Transform prefabContainer;

    public CharacterSpawner(T characterPrefab, Transform container, Transform prefabContainer, GameplayCoroutineManager gameplayCoroutineManager, Action OnSpawnerReady = null, bool createInitPool = true)
    {
        this.OnSpawnerReady += OnSpawnerReady;
        GameObject concreteContainer = new GameObject(typeof(T).ToString() + " " + POOL_NAME);
        concreteContainer.transform.SetParent(container);
        this.poolContainer = concreteContainer.transform;
        this.prefabContainer = prefabContainer;
        this.characterPrefab = characterPrefab;
        if (createInitPool)
            this.characterFactory = new PooleableFactory<T>(this.CreateCharacter, gameplayCoroutineManager, this.OnInitialPoolFinished);
        else
            this.characterFactory = new PooleableFactory<T>(this.CreateCharacter, gameplayCoroutineManager, 0, this.OnInitialPoolFinished);
    }

    public T SpawnCharacter()
    {
        T character = this.characterFactory.GetPoolItem();
        character.OnReturnedItem += this.ReturnBaseCharacter;
        character.transform.SetParent(this.prefabContainer);
        character.EnableObject();
        return character;
    }

    private T CreateCharacter()
    {
        T character = (GameObject.Instantiate<T>(this.characterPrefab as T));
        character.transform.SetParent(this.poolContainer);
        character.DisableObject();
        return character;
    }

    private void ReturnBaseCharacter(IPooleable character)
    {
        Transform characterTransform = ((AbstractViewEntity)character).transform;
        characterTransform.SetParent(this.poolContainer);
        character.OnReturnedItem -= this.ReturnBaseCharacter;
        character.DisableObject();
        this.characterFactory.ReturnPoolItem((T)character);
    }

    private void OnInitialPoolFinished()
    {
        this.OnSpawnerReady?.Invoke();
    }
}