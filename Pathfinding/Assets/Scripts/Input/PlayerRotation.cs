using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float mouseRotationSpeed = 0.2f;
    [SerializeField] float keyboardRotationSpeed = 2f;

    [SerializeField] GameObject focalPoint;

    float screenX = Screen.width;
    float screenY = Screen.height;

    float yaw = 0.0f;
    float row = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        yaw = transform.rotation.eulerAngles.y;
        row = transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Mouse.current.position.x.value;
        float mouseY = Mouse.current.position.y.value;
        if (mouseX < 0 || mouseX > screenX || mouseY < 0 || mouseY > screenY)
            return;
    }
    public void RotateMouse(Vector2 delta)
    {
        yaw += delta.x * mouseRotationSpeed;
        row -= delta.y * mouseRotationSpeed;
        Rotate();
    }
    public void ResetPivot()
    {
        //focalPoint.transform.rotation = Quaternion.identity;
        //yaw = 0;
    }
    private void Rotate()
    {
        Quaternion rot = Quaternion.Euler(row, yaw, 0);
        transform.rotation = rot;
    }
}
