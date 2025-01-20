using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // animator stuff // 
    public Animator animator;

    public PlayerManager pm;

    private float _acceleration;
    private float _moveSpeed;

    public InputAction playerMovement;
    public InputAction jumpInput;
    public InputAction dashInput;
    public InputAction abilityInput;

    public GameObject playerSpawn;
    // public GameObject playerEnd; // add an prefab where the level will end & start ending cutscene

    public LayerMask groundLayer;
    public Transform groundCheck;

    public bool pubIsDashing;
    
    private float _moveInput;
    private Rigidbody2D _rb;
    
    private bool _facingRight = true;
    private bool _isGrounded;
    
    private bool _canJump;
    private bool _isJumping;

    private int _jumpCount;

    private float _jumpInput;
    private bool _jumpButtonUp;
    
    private Vector2 _dashDir;
    private bool _isDashing;
    private bool _canDash = true;
    private float _dashInput;

    public LayerMask ladderLayer;
    private bool _touchingLadder;
    
    public LayerMask waterLayer;
    public Transform waterCheck;
    
    private bool _isSubmerged;
    
    private bool _canSwim;
    private bool _isSwimming;

    private readonly float _gravityScale = 1f;
    private float _ctc; // coyote time counter

    public float testDashTime = 0.5f;

    public float yv;

    private void OnEnable()
    {
        playerMovement.Enable();
        dashInput.Enable();
        jumpInput.Enable();
        abilityInput.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        jumpInput.Disable();
        dashInput.Disable();
        abilityInput.Disable();
    }

    private void Awake()
    {
        transform.position = playerSpawn.transform.position;
        _acceleration = pm.acceleration;
        _moveSpeed = pm.moveSpeed;
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _facingRight = true;
        _canJump = true;
        _jumpCount = pm.jumpCount;
    }
    
    private void Update()
    {
        yv = _rb.linearVelocity.y;
        
        #region InputChecks
        if (pm.canMove)
        {
            _moveInput = playerMovement.ReadValue<float>();
            _dashInput = dashInput.ReadValue<float>();
            _jumpInput = jumpInput.ReadValue<float>();
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _moveInput = 0f;
            _dashInput = 0f;
            _jumpInput = 0f;
        }

        if (pm.isStarting)
        {
            transform.position = playerSpawn.transform.position;
        }

        /*if (pm.isEnding)
        {
            transform.position = playerEnd.transform.position;
        }*/

        if (_jumpInput == 0)
        {
            _jumpButtonUp = true;
        }
        else
        {
            _jumpButtonUp = false;
        }
        #endregion

        #region Jump and Ladder
        
        if (!_touchingLadder)
        {
            if (_jumpInput > 0 && _canJump)
            {
                _ctc = 0;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, pm.jumpForce);
                _canJump = false;
                _isJumping = true;
                _jumpCount--;
                Jump();
            }

            if (_jumpInput < 0.01 && _isJumping)
            {
                JumpUp();
            }
        }
        
        else if (_touchingLadder)
        {
            if (_jumpInput > 0)
            {
                _ctc = 0;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x / 2.0f, pm.climbSpeed);
                _canJump = false;
                _isJumping = true;
                _jumpCount--;
                Jump();
            }

            if (_jumpInput < 0.01)
            {
                JumpUp();
            }
        }

        #endregion

        #region Swim

        if (_jumpInput > 0  && _canSwim)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, pm.jumpForce);
            _isSwimming = true;
            Swim();
        }
        
        #endregion

        #region FlipPlayer

        if (_moveInput > 0 && !_facingRight)
        {
            Flip();
            _facingRight = true;
        }
        else if(_moveInput < 0 && _facingRight)
        {
            Flip();
            _facingRight = false;
        }

        #endregion

        #region Timer

        _ctc -= Time.deltaTime;

        #endregion
    }

    private void FixedUpdate()
    {
        #region Checks

        _isGrounded = Physics2D.OverlapBox(groundCheck.position, pm.checkRadius, 0, groundLayer);
        _isSubmerged = Physics2D.OverlapBox(waterCheck.position, pm.waterCheckRadius, 0, waterLayer);
        _touchingLadder = Physics2D.OverlapBox(groundCheck.position, pm.checkRadius, 0, ladderLayer);

        if (!_isGrounded)
        {
            pm.acceleration = _acceleration/2.0f;
            _moveSpeed = pm.moveSpeed/1.5f;
        }
        else
        {
            pm.acceleration = _acceleration;
            _moveSpeed = pm.moveSpeed;
        }
        
        if (_isGrounded && _jumpInput < 0.01 && !_isSubmerged)
        {
            _jumpCount = pm.jumpCount;
            _isJumping = false;

            _canDash = true;
            
            _ctc = pm.coyoteTime;
        }
        if ((_isGrounded && _isSubmerged) || _isSubmerged)
        {
            _canJump = false;
            _isJumping = false;

            _canDash = true;
            
            _isDashing = false;
            pubIsDashing = false;

            
            _jumpCount = 0;
            
            _canSwim = true;
            _isSwimming = true;
        }

        if (!_isSubmerged)
        {
            
            _canSwim = false;
            _isSwimming = false;
        }

        if ((_ctc > 0 && _jumpCount > 0) && _jumpButtonUp)
        {
            if ((_isGrounded && _isSubmerged) || _isSubmerged)
            {
                _canJump = false;
            }
            else
            {
                _canJump = true;
            }
        }
        else
        {
            _canJump = false;
        }

        #endregion

        #region Jump Gravity

        if(_rb.linearVelocity.y < 0 && !_isSubmerged)
        {
            _rb.gravityScale = _gravityScale * pm.fallGravityMultiplier;
            _isJumping = false;
        }
        else if (_rb.linearVelocity.y < 0 && _isSubmerged)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, Mathf.Max(-(pm.maxWaterVel), _rb.linearVelocity.y));
            _rb.gravityScale = _gravityScale * pm.swimGravMultiplier;
        }
        else
        {
            _rb.gravityScale = _gravityScale;
        }
        

        #endregion
        
        #region Dash

        if (_dashInput > 0 && _canDash)
        {
             _canDash = false;
             _isDashing = true;
             pubIsDashing = true;
             _dashDir = new Vector2(transform.localScale.x * 1, 0).normalized;
             StartCoroutine(Dash());
        }
        
        #endregion
        
        #region Run

        if (_isSwimming)
        {
            float targetSpeed = _moveInput * pm.swimSpeed;
            float speedDif = targetSpeed - _rb.linearVelocity.x;
        
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? pm.swimAcceleration : pm.swimDeceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, pm.velPower) * Mathf.Sign(speedDif);

            _rb.AddForce(movement * Vector2.right);
        }
        else
        {
            float targetSpeed = _moveInput * pm.moveSpeed;
            float speedDif = targetSpeed - _rb.linearVelocity.x;
        
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? pm.acceleration : pm.deceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, pm.velPower) * Mathf.Sign(speedDif);

            _rb.AddForce(movement * Vector2.right);
        }
        

        #endregion
        
        #region Friction

        if (_isGrounded && Mathf.Abs(_moveInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.linearVelocity.x), Mathf.Abs(pm.frictionAmount));

            amount *= Mathf.Sign(_rb.linearVelocity.x);
            
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        
        #endregion

        #region Animation Components

        var velocity = _rb.linearVelocity;
        
        float currentSpeed = velocity.x;

        float yVel = velocity.y;

        bool isMoving = !myApproximation(0f, velocity.x, 1E-02f);

        bool appxZero = myApproximation(0f, velocity.y, 1E-02f);

        bool isFalling = !appxZero && velocity.y < 0;

        bool isSwimming = _isSwimming;

        bool isDashing = _isDashing;
        
        animator.SetFloat("playerSpeed", Mathf.Abs(currentSpeed));
        
        animator.SetBool("isMoving", isMoving);
        
        animator.SetBool("isFalling", isFalling);
        
        animator.SetBool("isDashing", isDashing);
        
        animator.SetBool("isSwimming", isSwimming);
        
        animator.SetBool("isJumping", _isJumping);
        
        #endregion
        
        if (_isDashing)
        {
            pm.isInvuln = true;
        }
        else
        {
            pm.isInvuln = false;
        }
    }

    #region Flip
    
    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    #endregion

    #region Jump Script

    void Jump()
    {
        _rb.AddForce(Vector2.up * pm.jumpForce, ForceMode2D.Impulse);
    }

    void JumpUp()
    {
        if (_rb.linearVelocity.y > 0 && _isJumping)
        {
            _rb.AddForce(Vector2.down * _rb.linearVelocity.y * (1 - pm.jumpCutMultiplier), ForceMode2D.Impulse);
            // _rb.AddForce(Vector2.down * _rb.velocity.y * -0.8f, ForceMode2D.Impulse);
        }
    }
    
    #endregion
    
    # region Swim Stuff

    private void Swim()
    {
        _rb.AddForce(Vector2.up * pm.swimForce, ForceMode2D.Impulse);
    }
    
    #endregion

    #region Coroutines

    IEnumerator Dash()
    { 
        _isDashing = true;
        pubIsDashing = true;
        
        
        _rb.gravityScale = 0f;

        float dashEndTime = Time.time + pm.dashTime;
        float originalVel = _rb.linearVelocity.x;

        while (Time.time < dashEndTime)
        {
            _rb.linearVelocity = _dashDir * pm.dashForce;
            yield return null; 
        }

        _rb.gravityScale = 1f;

        // _rb.velocity = new Vector2(originalVel, _rb.velocity.y);
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);


        _isDashing = false;
        pubIsDashing = false;

        _canDash = false;

    }
    
    #endregion

    #region Death

    public void Respawn()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = playerSpawn.transform.position;
        //pm.currentHealth = pm.maxHealth;
    }

    public void DefeatPlayer()
    {
        Respawn();
        //respawn menu here
    }

    #endregion
    
    #region Getters

    public Vector2 GetDash()
    {
        return _dashDir;
    }
    
    #endregion

    
    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }

}