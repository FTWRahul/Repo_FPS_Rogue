using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDCrosshair : MonoBehaviour
{
    public float hitIndicatorShowDuration = 0.5f;
    public float deathIndicatorShowDuration = 1.0f;
    public float damageIndicatorShowDuration = 1.0f;
    
    public RawImage hitMaker;
    public RawImage deathMarker;
    public RawImage damageIndicator;
    
    float _damageAngle;
    private Camera _camera;

    void Awake()
    {
        hitMaker.gameObject.SetActive(false);
        deathMarker.gameObject.SetActive(false);
        damageIndicator.gameObject.SetActive(false);
        _camera = Camera.main;
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

    private void UpdateHitDirectionIndicator(Quaternion cameraRotation)
    {
        var cameraForward = cameraRotation * Vector3.forward;
        var camDirPlane = Vector3.ProjectOnPlane(cameraForward, Vector3.up);
        var camAngle = Vector3.Angle(Vector3.forward, camDirPlane);
        var cross2 = Vector3.Cross(Vector3.forward, camDirPlane);
        if (cross2.y < 0)
            camAngle = -camAngle;

        var angle = MathHelper.SignedAngle(camAngle, _damageAngle);

        var rot = Quaternion.Euler(0, 0, 180 - angle);
        damageIndicator.transform.rotation = rot;
    }
    
    public void ShowHitDirectionIndicator(float damageAngle)
    {
        _damageAngle = damageAngle;
        UpdateHitDirectionIndicator(_camera.transform.rotation);
        damageIndicator.gameObject.SetActive(true);
        StartCoroutine(UpdateFrame(damageIndicator.gameObject, damageIndicatorShowDuration));
    }
    
    IEnumerator UpdateFrame(GameObject objectToUpdate, float time)
    {
        yield return new WaitForSeconds(time);

        objectToUpdate.SetActive(false);
    }
}