using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _attackTriggerName = "onAttack";
    [SerializeField] private string _moveBoolName = "isMoving";
    [SerializeField] private string _jumpStartName = "onJump";
    [SerializeField] private string _landingName = "onLanding";
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Values")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 5f;

    private float _xAxis, _zAxis;
    private Vector3 _dir = new Vector3();
    private bool _canJump = true;

    private Animator _animator;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.angularDrag = 1f;
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _animator.SetFloat(_xAxisName, _xAxis);
        _animator.SetFloat(_zAxisName, _zAxis);

        if (Input.GetKeyDown(_attackKey))
        {
            _animator.SetTrigger(_attackTriggerName);

            if(_xAxis != 0 || _zAxis != 0)
            {
                _animator.SetBool(_moveBoolName, true);
            }
            else
            {
                _animator.SetBool(_moveBoolName, false);
            }
        }

        if (Input.GetKeyDown(_jumpKey) && _canJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if(_xAxis != 0 || _zAxis != 0)
        {
            Movement(_xAxis, _zAxis);
        }
    }

    private void Jump()
    {
        _canJump = !_canJump;

        _animator.SetTrigger(_jumpStartName);

        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(float xAxis, float zAxis)
    {
        _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;

        _rb.MovePosition(transform.position + _dir * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            _canJump = true;

            _animator.SetTrigger(_landingName);
        }
    }
}
