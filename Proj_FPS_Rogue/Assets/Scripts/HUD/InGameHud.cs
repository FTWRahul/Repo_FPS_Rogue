using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHud : MonoBehaviour
{
    private HUDCrosshair _crosshair;
    private HealthBar _healthBar;
    private Canvas _canvas;

    private HealthState _healthState;

    public void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _crosshair = GetComponentInChildren<HUDCrosshair>();
        _healthBar = GetComponentInChildren<HealthBar>();

        _healthState = GetComponentInParent<HealthState>();
        _healthState.onDamage.AddListener(UpdateHealthBar);
        _healthState.onLastDamageChanged.AddListener(UpdateCrosshair); 
    }

    private void UpdateHealthBar(int health, int damage)
    {
        _healthBar.UpdateBar(health);
    }

    private void UpdateCrosshair(Damage damage)
    {
        _crosshair.ShowHitMarker(damage.isLethal);
    }
}