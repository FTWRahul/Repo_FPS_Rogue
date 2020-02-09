using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHud : MonoBehaviour
{
    private HUDCrosshair _crosshair;
    private HealthBar _healthBar;
    private Canvas _canvas;

    private HealthState _healthState;
    private CharacterData _characterData;

    public void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _crosshair = GetComponentInChildren<HUDCrosshair>();
        _healthBar = GetComponentInChildren<HealthBar>();

        _healthState = GetComponentInParent<HealthState>();
        _healthState.onHealthChange.AddListener(UpdateHealthBar);

        _characterData = GetComponentInParent<CharacterData>();
        _characterData.onLastDamageChanged.AddListener(UpdateCrosshair);
    }

    private void UpdateHealthBar(int health)
    {
        _healthBar.UpdateBar(health);
    }

    private void UpdateCrosshair()
    {
        _crosshair.ShowHitMarker(_characterData.lastShoot.isLethal);
    }
    
    
}