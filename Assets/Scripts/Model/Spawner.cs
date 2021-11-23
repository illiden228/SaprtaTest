using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner
{
    private Vector2 _mapBorder;
    private ObjectFactory _factory;
    private List<ICollisionable> _collisionables = new List<ICollisionable>();
    private EnemyCollection _enemies = new EnemyCollection();
    private FruitCollection _fruits = new FruitCollection();
    private Vector3 _playerPosition;
    private int _countEnemies;
    private int _countFruits;

    public EnemyCollection Enemies => _enemies;
    public FruitCollection Fruits => _fruits;
    public List<ICollisionable> Collisionables => _collisionables;

    public Action<ICollisionable> SpawnedCollisionable;

    public Spawner(Vector2 mapBorder, ObjectFactory factory, int countEnemies, int countFruits, Vector3 playerPosition)
    {
        _mapBorder = mapBorder;
        _factory = factory;
        _playerPosition = playerPosition;
        _countEnemies = countEnemies;
        _countFruits = countFruits;
    }

    public void FillMap()
    {
        for (int i = 0; i < _countEnemies; i++)
        {
            SpawnEnemy();
        }

        for (int i = 0; i < _countFruits; i++)
        {
            SpawnFruit();
        }
    }

    public void ResetCollisionable(ICollisionable collisionable)
    {
        if (collisionable is EnemyModel)
        {
            EnemyModel model = collisionable as EnemyModel;
            model.Restart(GetRandomPosition());
        }
        if (collisionable is FruitModel)
        {
            FruitModel model = collisionable as FruitModel;
            model.GameStart(GetRandomPosition());
        }
    }

    private void SpawnEnemy()
    {
        EnemyView newEnemy = _factory.Get(GenerateObjectType.Enemy).GetComponent<EnemyView>();
        EnemyModel newEnemyModel = new EnemyModel();
        EnemyPresenter newEnemyPresenter = new EnemyPresenter(newEnemy, newEnemyModel);

        newEnemyPresenter.Enable();
        newEnemyModel.GameStart(_mapBorder, GetRandomPosition());
        _enemies.Add(newEnemyModel, newEnemy, newEnemyPresenter);

        if (newEnemyModel is ICollisionable)
        {
            SpawnedCollisionable?.Invoke(newEnemyModel);
            _collisionables.Add(newEnemyModel);
        }
    }

    private void SpawnFruit()
    {
        FruitView newFruit = _factory.Get(GenerateObjectType.Fruit).GetComponent<FruitView>();
        FruitModel newFruitModel = new FruitModel();
        FruitPresenter newFruitPresenter = new FruitPresenter(newFruit, newFruitModel);

        newFruitPresenter.Enable();
        newFruitModel.GameStart(GetRandomPosition());
        _fruits.Add(newFruitModel, newFruit, newFruitPresenter);

        if (newFruitModel is ICollisionable)
        {
            SpawnedCollisionable?.Invoke(newFruitModel);
            _collisionables.Add(newFruitModel);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(
            Random.Range(-_mapBorder.x, _mapBorder.x),
            Random.Range(-_mapBorder.y, _mapBorder.y)
            );

        if (position.x <= _playerPosition.x + 1 && position.x >= _playerPosition.x - 1)
            position.x += 2;
        if (position.y <= _playerPosition.y + 1 && position.y >= _playerPosition.y - 1)
            position.y += 2;

        return position;
    }
}
