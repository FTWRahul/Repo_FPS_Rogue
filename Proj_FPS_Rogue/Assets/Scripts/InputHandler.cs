using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region References

    private MovementController _movementController;
    //private something for shooting
    private PlayerCameraController _playerCameraController;
    private Gun _gun;

    #endregion

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _playerCameraController = GetComponentInChildren<PlayerCameraController>();
        _gun = _playerCameraController.GetComponent<Gun>();
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
        //TODO: Set all weapon input here like for movement
        _playerCameraController.mouseInputVector =  new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _gun.Shoot();
        }
    }

    public void AddRecoil()
    {
        _playerCameraController.mouseInputVector.y += .2f;
    }
}