using UnityEngine;

public class FruitView : MonoBehaviour
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
}
