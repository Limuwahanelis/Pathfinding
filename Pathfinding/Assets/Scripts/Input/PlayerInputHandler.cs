using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerRotation _playerRotation;
    Vector2 _direction;
    bool _canRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerMovement.Move(_direction);
    }

    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
      
    }
    public void OnLook(InputValue value)
    {
        if(_canRotate) _playerRotation.RotateMouse(value.Get<Vector2>());
    }
    public void OnLockRotation(InputValue value)
    {
        _canRotate = value.Get<float>()>=1?true:false;
    }
}
