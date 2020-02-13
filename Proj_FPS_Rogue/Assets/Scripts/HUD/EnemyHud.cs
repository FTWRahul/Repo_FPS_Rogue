using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHud : MonoBehaviour
{
    public float timeToHide;
    private bool IsVisible => _canvas.enabled;
    
    public GameObject floatingDamage;
    
    private HealthBar _healthBar;
    private HealthState _healthState;
    private Canvas _canvas;
    private Camera _camera;
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _camera = Camera.main;
        
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthState = GetComponentInParent<HealthState>();
        
        _healthState.onHealthChange.AddListener(UpdateHealthBar);
        _healthState.onDamage.AddListener(GetDamage);
        
        _canvas.enabled = false;
    }

    private void Update()
    {
        if (IsVisible)
        {
            RotateCanvas();
        }
    }

    private void GetDamage(int health, int damage)
    {
        StopCoroutine(HideCanvas());
        UpdateHealthBar(health);
        FloatDamage(damage);
        StartCoroutine(HideCanvas());
    }

    private void RotateCanvas()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
    private void UpdateHealthBar(int health)
    {
        if(!_canvas.enabled)
            _canvas.enabled = true;
        _healthBar.UpdateBar(health);
    }

    private void FloatDamage(int damage)
    {
        TextMeshProUGUI fDamage = Instantiate(floatingDamage, _healthBar.transform.position + Vector3.up * 0.5f + Vector3.right * Random.Range(-1f,1f), transform.rotation, this.transform).GetComponent<TextMeshProUGUI>();
        fDamage.text = damage.ToString();
    }

    IEnumerator HideCanvas()
    {
        yield return new WaitForSeconds(timeToHide);
        _canvas.enabled = false;
    }
}
