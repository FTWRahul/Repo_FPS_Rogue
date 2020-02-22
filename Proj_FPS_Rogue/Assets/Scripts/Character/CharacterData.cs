using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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

    #region DEBUG

    [BoxGroup("DEBUG")][ReadOnly] public LocoState locoState;
    [BoxGroup("DEBUG")][ReadOnly] public Action action;

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