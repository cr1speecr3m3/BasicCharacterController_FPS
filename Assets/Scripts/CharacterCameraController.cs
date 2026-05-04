using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour
{
    public float activeSensitivity = 1f;
    private float xRotation = 0f;
    private bool freeze;
    public Transform playerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        freeze = false;
    }

    void Update()
    {
        if (freeze || Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * activeSensitivity;

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            toggleFreeze();
            Debug.Log("Freeze toggled: " + freeze);
        }
    }

    void toggleFreeze()
    {
        freeze = !freeze;

        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
