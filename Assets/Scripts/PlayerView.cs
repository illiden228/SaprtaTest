using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public event Action<Vector3> Click;
    public event Action FrameUpdated;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTarget.z = 0;
            Click?.Invoke(moveTarget);
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 moveTarget = Camera.main.ScreenToWorldPoint(touch.position);
            moveTarget.z = 0;
            Click?.Invoke(moveTarget);
        }

        FrameUpdated?.Invoke();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void LookAt(Vector3 target)
    {
        Vector3 direction = transform.position - target;
        transform.up = direction;
    }
}
