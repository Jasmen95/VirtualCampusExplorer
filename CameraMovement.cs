using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 0.5f;
    [SerializeField] private float mouseXSpeed = 1.0f; // Separate speeds for X and Y axes
    [SerializeField] private float mouseYSpeed = 0.5f; // Adjust this value for Y-axis speed
    private CinemachineFreeLook cinemachine;
    private PlayerControls playerInput;

    private void Awake()
    {
        playerInput = new PlayerControls();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        Vector2 delta = playerInput.Player.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * lookSpeed * Time.deltaTime * mouseXSpeed; // Apply X-axis speed
        cinemachine.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime * mouseYSpeed; // Apply Y-axis speed

        // Use mouse delta to control camera movement
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        cinemachine.m_XAxis.Value += mouseDelta.x * lookSpeed * Time.deltaTime * mouseXSpeed; // Apply X-axis speed
        cinemachine.m_YAxis.Value -= mouseDelta.y * lookSpeed * Time.deltaTime * mouseYSpeed; // Apply Y-axis speed and invert Y-axis movement
    }
}
