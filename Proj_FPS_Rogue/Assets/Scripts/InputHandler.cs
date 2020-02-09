using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region References

    private MovementController _movementController;
    private ActionController _actionController;

    #endregion

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _actionController = GetComponent<ActionController>();
        
    }

    private void Update()
    {
        GetMovementInputData();
        GetShootingInputData();
    }

    void GetMovementInputData()
    {
        _movementController.inputVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        _movementController.isRunClicked = Input.GetKeyDown(KeyCode.LeftShift);
        _movementController.isRunReleased = Input.GetKeyUp(KeyCode.LeftShift);

        if(_movementController.isRunClicked)
            _movementController.isRunning = true;

        if(_movementController.isRunReleased)
            _movementController.isRunning = false;

        _movementController.isJumpClicked = Input.GetKeyDown(KeyCode.Space);
    }

    void GetShootingInputData()
    {
        _actionController.isPrimaryClicked = Input.GetKey(KeyCode.Mouse0);
    }
}