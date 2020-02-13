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
    public GameObject floatingDamage;
    
    private HealthBar _healthBar;
    private HealthState _healthState;
    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthState = GetComponentInParent<HealthState>();
        
        _healthState.onHealthChange.AddListener(UpdateHealthBar);
        _healthState.onDamage.AddListener(GetDamage);
        _healthBar.gameObject.SetActive(false);
    }
    
    private void GetDamage(int health, int damage)
    {
        StopCoroutine(HideHUD());
        UpdateHealthBar(health);
        FloatDamage(damage);
        StartCoroutine(HideHUD());
    }
    private void UpdateHealthBar(int health)
    {
        if(!_healthBar.gameObject.activeSelf)
            _healthBar.gameObject.SetActive(true);
        _healthBar.UpdateBar(health);
    }

    private void FloatDamage(int damage)
    {
        TextMeshProUGUI fDamage = Instantiate(floatingDamage, _healthBar.transform.position + Vector3.up * 0.5f, Quaternion.identity, this.transform).GetComponent<TextMeshProUGUI>();
        fDamage.text = damage.ToString();
    }

    IEnumerator HideHUD()
    {
        yield return new WaitForSeconds(timeToHide);
        _healthBar.gameObject.SetActive(false);
    }
}
