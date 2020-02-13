﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public enum LocoState
    {
        STAND,
        GROUND_MOVE,
        JUMP,
        IN_AIR
    }

    public enum Action
    {
        NONE,
        PRIMARY_FIRE,
        SECONDARY_FIRE,
        RELOADING
    }

    #region Character state

    public LocoState locoState;
    public Action action;
    public Damage lastDamage;
    
    #endregion

    #region Events

    

    #endregion
    public void SetAction(Action action)
    {
        //GameDebug.Log("SetAction:" + action);
        this.action = action;
    }

    public void SetLocoState(LocoState locoState)
    {
        //GameDebug.Log("SetLocoState:" + locoState);
        this.locoState = locoState;
    }
}