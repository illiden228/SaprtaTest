using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectFactory : ScriptableObject
{
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private GameObject _waterPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _fruitPrefab;
    [SerializeField] private GameObject _playerPrefab;

    public GameObject Get(GenerateObjectType type)
    {
        GameObject instance;
        switch (type)
        {
            case GenerateObjectType.Ground:
                instance = Instantiate(_groundPrefab);
                break;
            case GenerateObjectType.Water:
                instance = Instantiate(_waterPrefab);
                break;
            case GenerateObjectType.Enemy:
                instance = Instantiate(_enemyPrefab);
                break;
            case GenerateObjectType.Fruit:
                instance = Instantiate(_fruitPrefab);
                break;
            case GenerateObjectType.Player:
                instance = Instantiate(_playerPrefab);
                break;
            default:
                instance = null;
                break;
        }

        return instance;
    }

    public void Reclaim(GameObject generateable)
    {
        Destroy(generateable);
    }
}

public enum GenerateObjectType
{
    Ground, Water, Enemy, Fruit, Player
}