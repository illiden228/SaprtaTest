using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable
{
    protected readonly float Speed = 3;
    protected float CurrentSpeed = 0;
    private Vector2 _mapBorder;
    private Vector3 _targetPosition;
    private Vector3 _currentPosition;
    public Vector3 Position => _currentPosition;

    public Moveable(float speed)
    {
        Speed = speed;
    }

    public virtual void GameStart(Vector2 mapBorder, Vector3 currentPosition)
    {
        CurrentSpeed = Speed;
        _mapBorder = mapBorder;
        SetTarget(currentPosition);
        SetCurrentPosition(currentPosition);
    }

    public void SetCurrentPosition(Vector3 currentPosition)
    {
        _currentPosition = currentPosition;
    }

    protected bool TryMove()
    {
        Vector3 movePosition = Vector3.MoveTowards(_currentPosition, _targetPosition, Speed * Time.deltaTime);

        Vector3 positionWithinMap = movePosition;
        positionWithinMap.x = Mathf.Clamp(movePosition.x, -_mapBorder.x, _mapBorder.x);
        positionWithinMap.y = Mathf.Clamp(movePosition.y, -_mapBorder.y, _mapBorder.y);

        if (positionWithinMap.x != movePosition.x && positionWithinMap.y != movePosition.y)
            return false;

        if (Vector3.Distance(_currentPosition, _targetPosition) > 0)
        {
            _currentPosition = positionWithinMap;
            return true;
        }
        else
            return false;
    }

    public virtual void SetTarget(Vector3 target)
    {
        _targetPosition = target;
    }
}
