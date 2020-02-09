using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public static float mouseSensitivity = 2f;
    public Transform playerBody;
    [SerializeField] private Vector2 yMinMax;
    float xAxisClamp = 0;
    public Vector2 mouseInputVector;
    private InputHandler _inputHandler;
    
    void Awake()
    {
        _inputHandler = GetComponentInParent<InputHandler>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.enabled = true;
    }

    // Update is called once per frame
    void Update ()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        mouseInputVector = _inputHandler.mounseXY;
        float rotAmountX = mouseInputVector.x * mouseSensitivity;
        float rotAmountY = mouseInputVector.y * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if(xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }
        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);
    }

    public static void ChangeSensitivity(float newSen)
    {
        mouseSensitivity = newSen;
    }
}
