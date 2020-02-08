using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private MovementController _movementController;

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        GetMovementInputData();
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
}