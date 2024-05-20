using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerRotation _playerRotation;
    [SerializeField] TileObjectPlacer _tileObjectPlacer;
    [SerializeField] ExitApp _exitApp;
    [SerializeField] TileSelection _tileSelection;
    private Vector2 _direction;
    private bool _canRotate = false;
    private bool _showExitPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (_showExitPanel) return;
        _playerMovement.Move(_direction);
    }

    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
      
    }
    public void OnLook(InputValue value)
    {
        if (_showExitPanel) return;
        if (_canRotate) _playerRotation.RotateMouse(value.Get<Vector2>());
    }
    public void OnLockRotation(InputValue value)
    {
        _canRotate = value.Get<float>()>=1?true:false;
    }

    public void OnExit(InputValue value)
    {
        _showExitPanel = !_showExitPanel;
        Cursor.lockState = _showExitPanel?CursorLockMode.None:CursorLockMode.Confined;
        _exitApp.SetExitPanel(_showExitPanel);
    }

    public void OnFire()
    {
        if (_showExitPanel) return;
        _tileObjectPlacer.PlaceTile();
    }

    public void OnCursor(InputValue value)
    {
        if (_showExitPanel) return;
        _tileSelection.SetMousePos(value.Get<Vector2>());
    }
}
