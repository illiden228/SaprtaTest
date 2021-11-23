using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitModel : ICollisionable
{
    private int _value;
    private float _collisionRadius;
    private bool _enabled = true;
    private Vector3 _position;

    public event Action<Vector3> PositionChanged;
    public event Action<bool> EnableChanged;

    public FruitModel(int value = 1, float collisionRadius = 1)
    {
        _value = value;
        _collisionRadius = collisionRadius;
    }

    public void GameStart(Vector3 position)
    {
        SetPosition(position);
        Restart();
    }

    private void SetPosition(Vector3 position)
    {
        _position = position;
        PositionChanged?.Invoke(_position);
    }

    public void Restart()
    {
        _enabled = true;
        EnableChanged?.Invoke(_enabled);
    }

    public bool Enabled() => _enabled;

    public Vector3 GetPosition() => _position;

    public float GetRadius() => _collisionRadius;

    public void OnCollision(ICollisionable collision)
    {
        if (collision is PlayerModel)
        {
            (collision as PlayerModel).TakeFruits(_value);
            Destroy();
        }
    }

    public void Destroy()
    {
        _enabled = false;
        EnableChanged?.Invoke(_enabled);
    }
}
