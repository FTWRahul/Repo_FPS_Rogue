using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHud : MonoBehaviour
{
    private HUDCrosshair _crosshair;
    private Canvas _canvas;

    public void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _crosshair = GetComponentInChildren<HUDCrosshair>();
    }
    
    
}