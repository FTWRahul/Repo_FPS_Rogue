using System;
using NaughtyAttributes;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [BoxGroup("DEBUG")][ReadOnly] public Vector2 mounseXY;
    
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
        GetInteractionInputData();
    }

    void GetMovementInputData()
    {
        _movementController.inputVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        _movementController.isSprinting = Input.GetKey(KeyCode.LeftShift);

        _movementController.isRunning = _movementController.inputVector != Vector2.zero;
        
        _movementController.isJumpClicked = Input.GetKeyDown(KeyCode.Space);
    }

    void GetShootingInputData()
    {
        mounseXY = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _actionController.isPrimaryClicked = Input.GetKey(KeyCode.Mouse0);
    }

    void GetInteractionInputData()
    {
        _actionController.isInteractPressed = Input.GetKeyDown(KeyCode.E);
    }
}