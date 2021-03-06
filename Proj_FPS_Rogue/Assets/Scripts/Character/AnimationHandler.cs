﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHandler : MonoBehaviour
{
    #region References

    private CharacterData _characterData;
    private Animator _animator;
    private HealthState _healthState;

    #endregion

    #region Parameters id

    private static readonly int Stand = Animator.StringToHash("AnimState_Stand");
    private static readonly int Run = Animator.StringToHash("AnimState_Run");
    private static readonly int Jump = Animator.StringToHash("AnimState_Jump");
    private static readonly int InAir = Animator.StringToHash("AnimState_InAir");

    private static readonly int PrimaryFire = Animator.StringToHash("Action_PrimaryFire");
    private static readonly int SecondaryFire = Animator.StringToHash("Action_SecondaryFire");
    private static readonly int Reloading = Animator.StringToHash("TriggerAction_Reloading");

    private static readonly int GetHit = Animator.StringToHash("Hit_Reaction");

    private static readonly int RunTree = Animator.StringToHash("Sprint_Weight");

    #endregion

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        _characterData = GetComponent<CharacterData>();
        _animator = GetComponentInChildren<Animator>();
        _healthState = GetComponent<HealthState>();
        _healthState.onDamage.AddListener(SetHit);
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
        _animator.SetFloat(RunTree, _characterData.sprint ? 1 : 0);
    }

    private void UpdateAction()
    {
        _animator.SetBool(PrimaryFire, _characterData.action == CharacterData.Action.PRIMARY_FIRE);

        if (_characterData.action == CharacterData.Action.RELOADING &&
            !_animator.GetCurrentAnimatorStateInfo(2).IsName("Reload"))
        {
            _animator.SetTrigger(Reloading);
        }
    }

    private void SetHit(int damage, int health)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(2).IsName("Damage_Reaction"))
        {
            _animator.SetTrigger(GetHit);
        }
    }
    

}
