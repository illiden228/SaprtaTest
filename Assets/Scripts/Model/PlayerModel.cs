using System;
using UnityEngine;

public class PlayerModel : Moveable, IDamageable, ICollisionable
{
    private readonly int _maxHealth = 3;
    private int _score = 0;
    private int _currentHealth = 0;
    private float _collisionRadius = 1;

    public int MaxHealth => _maxHealth;

    public event Action<Vector3> PositionChanged;
    public event Action<int> FruitsChanged;
    public event Action<int> HealthChanged;
    public event Action<int> Death;

    public PlayerModel(float speed = 3, int health = 3, float collisionRadius = 1) : base(speed)
    {
        _maxHealth = health;
        _collisionRadius = collisionRadius;
    }

    public void UpdatePosition()
    {
        if(IsAlive())
        {
            if (TryMove())
                PositionChanged?.Invoke(Position);
        }
    }

    public override void GameStart(Vector2 mapBorder, Vector3 startPosition)
    {
        base.GameStart(mapBorder, startPosition);
        PositionChanged?.Invoke(Position);

        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);

        _score = 0;
        FruitsChanged?.Invoke(_score);
    }

    public void TakeDamage(uint damage)
    {
        _currentHealth -= (int)damage;
        
        if (!IsAlive())
            Die();

        HealthChanged?.Invoke(_currentHealth);
    }

    private void Die()
    {
        CurrentSpeed = 0;
        Death?.Invoke(_score);
    }

    public void TakeFruits(int value)
    {
        _score += value;
        FruitsChanged?.Invoke(_score);
    }

    public bool IsAlive() => _currentHealth > 0;

    public Vector3 GetPosition() => Position;

    public float GetRadius() => _collisionRadius;

    public void OnCollision(ICollisionable collision) { }

    public bool Enabled() => IsAlive();
}
