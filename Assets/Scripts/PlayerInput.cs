using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _torqueAmount = 3f;
    [SerializeField] private float _groundCheckDistance = 3f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private float _gravity = -25f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationTransitionTime = 0.2f;
    [SerializeField] private float _maxSqrVelocityForAnimation = 5f;

    private Coroutine _animationCoroutine;
    
    private bool _isOnGround => 
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, _groundCheckDistance + 1f, _groundLayer);

    private bool _hasJustLanded = false;

    private void Awake()
    {
        // the free asset I'm using has this coded to running. Should write a wrapper
        _animator.SetFloat("State",1f);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        Debug.Log(_rb.velocity.sqrMagnitude);
        LerpToAnimationValue(_rb.velocity.sqrMagnitude / _maxSqrVelocityForAnimation);
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(transform.forward * _speed * Time.deltaTime);
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            _rb.AddForce(-transform.forward * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddTorque(Vector3.down * _torqueAmount * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddTorque(Vector3.up * _torqueAmount * Time.deltaTime);
        }
    }

    private void LerpToAnimationValue(float target)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }
        
        _animationCoroutine = StartCoroutine(AnimationTransitionCoroutine(target));
    }

    private IEnumerator AnimationTransitionCoroutine(float target)
    {
        target = Mathf.Clamp01(target);
        float startValue = _animator.GetFloat("Vert");
        float elapsedTime = 0f;

        while (elapsedTime < _animationTransitionTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / _animationTransitionTime;
            
            float current = Mathf.Lerp(startValue, target, progress);
            _animator.SetFloat("Vert", current);
            
            yield return null;
        }
        
        // at the end, make sure we made it exactly
        _animator.SetFloat("Vert", target);
        _animationCoroutine = null;
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && _isOnGround && !_hasJustLanded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
        }

        if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.velocity += Vector3.up * (_gravity * _lowJumpMultiplier * Time.deltaTime);
        }

        if (_rb.velocity.y < 0 && !_isOnGround)
        {
            _rb.velocity += Vector3.up * (_gravity * _fallMultiplier * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space) && !_isOnGround)
        {
            _hasJustLanded = true;
        }

        if (!Input.GetKey(KeyCode.Space) && _hasJustLanded)
        {
            _hasJustLanded = false;
        }
    }
}
