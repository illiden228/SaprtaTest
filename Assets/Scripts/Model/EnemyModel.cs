using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyModel : Moveable, ICollisionable
{
    private int _damage = 1;
    private float _timeBetweenChangeDirection = 4;
    private float _collisionRadius = 1;
    private Vector3 _direction;
    private float _time = 0;
    private bool _enabled = true;

    public event Action<Vector3> PositionChanged;
    public event Action<bool> EnableChanged;
    public event Action<Vector3> DirectionChanged;

    public EnemyModel(float speed = 2, int damage = 1, float timeBetweenChangeDirection = 4, float collisionRadius = 1) : base (speed)
    {
        _damage = damage;
        _timeBetweenChangeDirection = timeBetweenChangeDirection;
        _collisionRadius = collisionRadius;
    }

    public override void GameStart(Vector2 mapBorder, Vector3 position)
    {
        base.GameStart(mapBorder, position);
        PositionChanged?.Invoke(Position);
        Restart();
    }

    public void Restart(Vector3 position)
    {
        SetCurrentPosition(position);
        PositionChanged?.Invoke(Position);
        Restart();
    }

    public void Restart()
    {
        _enabled = true;
        EnableChanged?.Invoke(_enabled);
    }

    public void UpdatePosition()
    {
        if(_enabled)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _time = _timeBetweenChangeDirection;
                ChangeDirection();
            }

            while (_direction == Vector3.zero)
                ChangeDirection();

            SetTarget(Position + _direction);

            if (!TryMove())
                _direction = -_direction;
            else
                PositionChanged?.Invoke(Position);
        }
    }

    private void ChangeDirection()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        DirectionChanged?.Invoke(_direction);
    }

    public void TryAttack(IDamageable target)
    {
        if (target.IsAlive())
            target.TakeDamage((uint)_damage);
    }

    public Vector3 GetPosition() => Position;

    public float GetRadius() => _collisionRadius;

    public void OnCollision(ICollisionable collision)
    {
        if(collision is IDamageable)
        {
            TryAttack(collision as IDamageable);
            Die();
        }
    }

    public void Die()
    {
        _enabled = false;
        EnableChanged?.Invoke(_enabled);
    }

    public bool Enabled() => _enabled;
}

public interface IDamageable
{
    public bool IsAlive();
    public void TakeDamage(uint damage);
}
