using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker
{
    private ICollisionable _player;
    private List<ICollisionable> _collisionables;

    public bool IsReady => _collisionables != null ? _collisionables.Count > 0 : false;

    public event Action<ICollisionable> Collision;
    
    public CollisionChecker(ICollisionable player)
    {
        _player = player;
    }

    public void GameStart(List<ICollisionable> collisionables)
    {
        _collisionables = collisionables;
    }

    public void GameEnd()
    {
        _collisionables = null;
    }

    public void CheckAllCollisionablesWithPlayer()
    {
        for (int i = 0; i < _collisionables.Count; i++)
        {
            if (Vector3.Distance(_collisionables[i].GetPosition(), _player.GetPosition()) < _collisionables[i].GetRadius() + _player.GetRadius())
            {
                if (_collisionables[i].Enabled())
                {
                    _player.OnCollision(_collisionables[i]);
                    _collisionables[i].OnCollision(_player);

                    if (_collisionables == null)
                        return;

                    Collision?.Invoke(_collisionables[i]);
                }
            }
        }
    }
}

public interface ICollisionable
{
    public bool Enabled();
    public Vector3 GetPosition();
    public float GetRadius();
    public void OnCollision(ICollisionable collision);
}
