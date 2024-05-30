using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IJumpable
{
    void JumpPlate(float jumpPower);
}
public class PlayerController : MonoBehaviour, IJumpable
{
    [Header("Movement")]
    public float MoveSpeed;
    public float JumpPower;
    private Vector2 _curMovementInput;
    public LayerMask GroundLayerMask;

    [Header("Look")]
    public Transform CameraContainer;
    public float MinXLook;
    public float MaxXLook;
    public float LookSensitivity;
    private Vector2 _mouseDelta;
    private float _camCurXRot;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
        Debug.DrawRay(transform.position + (transform.forward * 0.2f) + (transform.up * 0.07f), Vector3.down * .1f, Color.red);
        Debug.DrawRay(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.07f), Vector3.down * .1f, Color.red);
        Debug.DrawRay(transform.position + (transform.right * 0.2f) + (transform.up * 0.07f), Vector3.down * .1f, Color.red);
        Debug.DrawRay(transform.position + (-transform.right * 0.2f) + (transform.up * 0.07f), Vector3.down * .1f, Color.red);
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        dir *= MoveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    private void CameraLook()
    {
        _camCurXRot += _mouseDelta.y * LookSensitivity;
        _camCurXRot = Mathf.Clamp(_camCurXRot, MinXLook, MaxXLook);
        CameraContainer.localEulerAngles = new Vector3(-_camCurXRot, 0f, 0f);

        transform.eulerAngles += new Vector3(0f, _mouseDelta.x * LookSensitivity, 0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    private bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.07f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.07f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.07f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.07f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, GroundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void JumpPlate(float jumpPower)
    {
        _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
