using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float mouseRotationSpeed = 0.2f;

    private float screenX = Screen.width;
    private float screenY = Screen.height;

    private float yaw = 0.0f;
    private float row = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        yaw = transform.rotation.eulerAngles.y;
        row = transform.rotation.eulerAngles.x;
    }

    public void RotateMouse(Vector2 delta)
    {
        yaw += delta.x * mouseRotationSpeed;
        row -= delta.y * mouseRotationSpeed;
        Rotate();
    }
    private void Rotate()
    {
        Quaternion rot = Quaternion.Euler(row, yaw, 0);
        transform.rotation = rot;
    }
}
