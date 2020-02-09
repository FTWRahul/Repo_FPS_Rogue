using DG.Tweening;
using UnityEngine;


public class FloatingDamage : MonoBehaviour
{
    public Vector2 offsetEndMax;
    public float offsetSpeed;
    public float scaleSpeed;
    public float duration;

    private void Start()
    {
        Invoke("DestroyPopup", duration);
    }

    private void Update()
    {
        transform.DOScale(Vector3.zero,  scaleSpeed);
        /*transform.DOLocalMove(new Vector3(Random.Range(-offsetEndMax.x, offsetEndMax.x),Random.Range(-offsetEndMax.y, offsetEndMax.y), 0), offsetSpeed);*/
    }

    void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
