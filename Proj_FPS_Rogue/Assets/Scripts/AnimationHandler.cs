using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    #region References

    private CharacterData _characterData;
    private Animator _animator;
    
    #endregion

    #region Parameters id
    
    private static readonly int Stand = Animator.StringToHash("AnimState_Stand");
    private static readonly int Run = Animator.StringToHash("AnimState_Run");
    private static readonly int Jump = Animator.StringToHash("AnimState_Jump");
    private static readonly int InAir = Animator.StringToHash("AnimState_InAir");
    
    private static readonly int PrimaryFire = Animator.StringToHash("Action_PrimaryFire");
    private static readonly int SecondaryFire = Animator.StringToHash("Action_SecondaryFire");
    private static readonly int Reloading = Animator.StringToHash("Action_Reloading");

    #endregion

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        _characterData = GetComponent<CharacterData>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        UpdateLocomotion();
        UpdateAction();
    }

    private void UpdateLocomotion()
    {
        _animator.SetBool(Stand, _characterData.locoState == CharacterData.LocoState.STAND);
        _animator.SetBool(Run, _characterData.locoState == CharacterData.LocoState.GROUND_MOVE);
        _animator.SetBool(Jump, _characterData.locoState == CharacterData.LocoState.JUMP);
        _animator.SetBool(InAir, _characterData.locoState == CharacterData.LocoState.IN_AIR);
    }

    private void UpdateAction()
    {
        _animator.SetBool(PrimaryFire, _characterData.action == CharacterData.Action.PRIMARY_FIRE);
    }
}
