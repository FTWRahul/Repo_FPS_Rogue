using UnityEngine;

public class ActionController : MonoBehaviour
{
    public bool isPrimaryClicked;

    public bool isIntractPressed;

    private bool HasInput => isPrimaryClicked == true;
    
    private Gun _primaryGun;
    private PlayerCameraController _playerCameraController;
    private CharacterData _characterData;
    
    private void Start()
    {
        _primaryGun = GetComponentInChildren<Gun>();
        _playerCameraController = GetComponentInChildren<PlayerCameraController>();
        _characterData = GetComponent<CharacterData>();
    }

    private void Update()
    {
        //why here?
        //_playerCameraController.mouseInputVector =  new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        if (isPrimaryClicked)
        {
            _primaryGun.Shoot();
        }

        if (isIntractPressed)
        {
            _primaryGun.TryEquipingPart();
        }
        
        SetData();
    }

    //why here?
    public void AddRecoil()
    {
        _playerCameraController.mouseInputVector.y += .2f;
    }

    private void SetData()
    {
        if (HasInput)
        {
            if (isPrimaryClicked)
            {
                _characterData.SetAction(CharacterData.Action.PRIMARY_FIRE);
            }
            
        }
        else
        {
            _characterData.SetAction(CharacterData.Action.NONE);
        }
    }
}