using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDCrosshair : MonoBehaviour
{
    public float hitIndicatorShowDuration = 0.5f;
    public float deathIndicatorShowDuration = 1.0f;
    
    public RawImage hitMaker;
    public RawImage deathMarker;

    void Awake()
    {
        hitMaker.gameObject.SetActive(false);
        deathMarker.gameObject.SetActive(false);
    }
    
    public void ShowHitMarker(bool lethal)
    {
        hitMaker.gameObject.SetActive(true);
        StartCoroutine(UpdateFrame(hitMaker.gameObject, hitIndicatorShowDuration));

        if (lethal)
        {
            deathMarker.gameObject.SetActive(true);
            StartCoroutine(UpdateFrame(deathMarker.gameObject, deathIndicatorShowDuration));
        }
    }

    IEnumerator UpdateFrame(GameObject objectToUpdate, float time)
    {
        yield return new WaitForSeconds(time);

        objectToUpdate.SetActive(false);
    }
}