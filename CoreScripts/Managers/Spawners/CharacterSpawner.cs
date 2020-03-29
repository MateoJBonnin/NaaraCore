using Pool;
using System;
using UnityEngine;

public class CharacterSpawner<ViewEntity> : GameSpawner where ViewEntity : AbstractViewEntity
{
    public override Action OnSpawnerReady { get; set; }

    private const string POOL_NAME = "Pool";

    private PooleableFactory<ViewEntity> characterFactory;
    private ViewEntity characterPrefab;
    private Transform poolContainer;
    private Transform prefabContainer;

    public CharacterSpawner(ViewEntity characterPrefab, Transform container, Transform prefabContainer, GameplayCoroutineManager gameplayCoroutineManager, Action OnSpawnerReady = null, bool createInitPool = true)
    {
        this.OnSpawnerReady += OnSpawnerReady;
        GameObject concreteContainer = new GameObject(typeof(ViewEntity).ToString() + " " + POOL_NAME);
        concreteContainer.transform.SetParent(container);
        this.poolContainer = concreteContainer.transform;
        this.prefabContainer = prefabContainer;
        this.characterPrefab = characterPrefab;
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
        character.EnableObject();
        return character;
    }

    private ViewEntity CreateCharacter()
    {
        ViewEntity character = (GameObject.Instantiate<ViewEntity>(this.characterPrefab as ViewEntity));
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
        this.characterFactory.ReturnPoolItem((ViewEntity)character);
    }

    private void OnInitialPoolFinished()
    {
        this.OnSpawnerReady?.Invoke();
    }
}