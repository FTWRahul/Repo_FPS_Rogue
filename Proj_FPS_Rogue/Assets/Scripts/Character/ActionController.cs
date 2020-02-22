using NaughtyAttributes;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    #region DEBUG

    [BoxGroup("DEBUG")][ReadOnly] public bool isPrimaryClicked;
    [BoxGroup("DEBUG")][ReadOnly] public bool isInteractPressed;

    #endregion
    
    private bool HasInput => isPrimaryClicked == true;

    #region REFERENCES

    private Gun _primaryGun;
    private PlayerCameraController _playerCameraController;
    private CharacterData _characterData;

    #endregion

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

        if (isInteractPressed)
        {
            _primaryGun.TryEquipingPart();
        }
        
        SetData();
    }

    //NOt working
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