using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingDamage : MonoBehaviour
{
    public float scaleMax;
    public float scaleMin;
    public Ease scaleEase;

    public float xOffsetMax;
    public float xOffsetMin;
    public float yOffsetMax;
    public float yOffsetMin;
    public Ease moveEase;
    
    public float duration;

    private Tween _scaling;
    private Tween _moving;
    private void Start()
    {
        Invoke("DestroyPopup", duration);
    }

    private void Update()
    {
        _scaling = transform.DOScale(Vector3.zero * Random.Range(scaleMin,scaleMax),  duration).SetEase(scaleEase);
        _moving = transform.DOLocalMove(
            Vector3.up * Random.Range(yOffsetMin, yOffsetMax) + Vector3.right * Random.Range(xOffsetMin, xOffsetMax),
            duration).SetEase(moveEase);
        /*transform.DOLocalMove(new Vector3(Random.Range(-offsetEndMax.x, offsetEndMax.x),Random.Range(-offsetEndMax.y, offsetEndMax.y), 0), offsetSpeed);*/
    }

    void DestroyPopup()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _scaling.Kill();
        _moving.Kill();
    }
}
