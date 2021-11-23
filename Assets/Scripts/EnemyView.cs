using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void OnEnableChanged(bool enabled)
    {
        _model.SetActive(enabled);
    }

    public void LookToDirection(Vector3 direction)
    {
        transform.up = direction;
    }
}
