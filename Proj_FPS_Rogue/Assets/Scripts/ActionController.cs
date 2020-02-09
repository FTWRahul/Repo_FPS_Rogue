using UnityEngine;

public class ActionController : MonoBehaviour
{
    public bool isPrimaryClicked;
    
    
    private Gun _primaryGun;
    private PlayerCameraController _playerCameraController;
    
    private void Start()
    {
        _primaryGun = GetComponentInChildren<Gun>();
        _playerCameraController = GetComponentInChildren<PlayerCameraController>();
    }

    private void Update()
    {
        _playerCameraController.mouseInputVector =  new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        if (isPrimaryClicked)
        {
            _primaryGun.Shoot();
        }
    }

    public void AddRecoil()
    {
        _playerCameraController.mouseInputVector.y += .2f;
    }
}