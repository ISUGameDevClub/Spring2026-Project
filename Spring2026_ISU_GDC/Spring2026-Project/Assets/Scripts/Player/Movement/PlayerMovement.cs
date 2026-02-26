using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject cameraFollow;
    private float fallSpeedyDampingChangeThreshold;

    [Header("Private")]
    private Rigidbody2D rb;
    private PlayerMovementConstants movementConstants;

    private float acceleration;
    private float decceleration;
    private float accelerationDefault;
    private float deccelerationDefault;

    private float lastGroundedTime;
    private float lastJumpTime;
    private float gravityScale;
    private bool isJumping;
    private bool jumpInputReleased;
    private bool touchingWall;
    private int numWallJumps;
    private int numWallJumpsDefault;
    private int wallDirection;
    private bool isFacingRight;
    private bool canTurn;
    private bool isWallSliding;
    private int numDoubleJumps;
    private int numDoubleJumpsDefault;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementConstants = GetComponent<PlayerMovementConstants>();

        isJumping = true;
        gravityScale = rb.gravityScale;
        touchingWall = false;

        acceleration = movementConstants.Acceleration;
        decceleration = movementConstants.Decceleration;
        accelerationDefault = acceleration;
        deccelerationDefault = decceleration;

        numWallJumps = movementConstants.NumWallJumps;
        numWallJumpsDefault = numWallJumps;
        numDoubleJumps = movementConstants.NumDoubleJumps;
        numDoubleJumpsDefault = numDoubleJumps;

        isFacingRight = true;
        canTurn = true;
        isWallSliding = false;

        lastJumpTime = 1;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = 0;
        }
    }

    void FixedUpdate()
    {
        #region Check Floor
        lastGroundedTime += Time.deltaTime;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, -Vector2.up, movementConstants.DistanceFromFloor);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Ground" && lastJumpTime != 0)
            {
                lastGroundedTime = 0;
                isJumping = false;
                jumpInputReleased = false;
                numWallJumps = numWallJumpsDefault;
                numDoubleJumps = numDoubleJumpsDefault;
            }
        }
        #endregion

        #region Check Wall
        touchingWall = false;
        hit = Physics2D.RaycastAll(transform.position, Vector2.left, movementConstants.DistanceFromWall);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Ground")
            {
                touchingWall = true;
                wallDirection = -1;
            }
        }

        hit = Physics2D.RaycastAll(transform.position, Vector2.right, movementConstants.DistanceFromWall);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Ground")
            {
                touchingWall = true;
                wallDirection = 1;
            }
        }
        #endregion

        #region Run
        float targetSpeed = moveInput * movementConstants.MoveSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, movementConstants.VelPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
        #endregion

        #region Friction
        if (lastGroundedTime == 0 && moveInput == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(movementConstants.FrictionAmount));
            amount *= Mathf.Sign(rb.linearVelocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Timer
        lastJumpTime += Time.fixedDeltaTime;
        #endregion

        #region Jump
        if ((lastJumpTime < movementConstants.JumpBufferTime && lastGroundedTime < movementConstants.JumpCoyoteTime) && !isJumping)
            Jump();
        else if (jumpInputReleased && numDoubleJumps > 0 && lastGroundedTime != 0 && lastJumpTime == Time.fixedDeltaTime && !touchingWall)
        {
            Jump();
            numDoubleJumps--;
            jumpInputReleased = false;
        }
        #endregion

        #region Regrab
        float maxFallSpeed = Input.GetButton("Jump") ? -movementConstants.TopFallingSpeedHold : -movementConstants.TopFallingSpeedRelease;
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
        #endregion

        #region Jump Gravity
        rb.gravityScale = (rb.linearVelocity.y < 0) ? gravityScale * movementConstants.FallGravityMultiplier : gravityScale;
        #endregion

        #region Jump Cut
        if (!jumpInputReleased && lastGroundedTime != 0 && !Input.GetButton("Jump"))
            OnJumpUp();
        #endregion

        #region Jump Hang
        if (lastGroundedTime != 0 && Mathf.Abs(rb.linearVelocity.y) < movementConstants.JumpHangThreshold)
        {
            rb.gravityScale = gravityScale * movementConstants.JumpHangGravityMult;
        }
        #endregion

        #region Wall Jump
        if (isJumping && lastJumpTime < movementConstants.JumpBufferTime &&
            (numWallJumps > 0 || movementConstants.InfWallJumps) &&
            jumpInputReleased && touchingWall)
        {
            Walljump();
        }
        #endregion

        #region Turn Check
        if (moveInput != 0 && canTurn)
            TurnCheck();
        #endregion

        #region Wall Slide
        isWallSliding = rb.linearVelocity.y < 0 && touchingWall && moveInput == wallDirection && lastGroundedTime != 0;
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -movementConstants.WallSlidingSpeed, float.MaxValue));
        }
        #endregion
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * movementConstants.JumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    private void Walljump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * movementConstants.JumpForce, ForceMode2D.Impulse);
        rb.AddForce(-Vector2.right * wallDirection * movementConstants.WallJumpForce, ForceMode2D.Impulse);
        numWallJumps--;
        canTurn = false;
        if ((wallDirection == 1 && isFacingRight) || (wallDirection == -1 && !isFacingRight))
            Turn();
        StartCoroutine(WalljumpMovementTimer());
    }

    private IEnumerator WalljumpMovementTimer()
    {
        acceleration *= movementConstants.WallJumpMovementMult;
        decceleration *= movementConstants.WallJumpMovementMult;
        yield return new WaitForSeconds(movementConstants.WallJumpMovementTime);
        acceleration = accelerationDefault;
        decceleration = deccelerationDefault;
        canTurn = true;
    }

    private void OnJumpUp()
    {
        if (rb.linearVelocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector2.down * rb.linearVelocity.y * (1 - movementConstants.JumpCutMultiplier), ForceMode2D.Impulse);
        }
        jumpInputReleased = true;
    }

    private void TurnCheck()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight)
            Turn();
        else if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight)
            Turn();
    }

    private void Turn()
    {
        Vector3 rotator = transform.rotation.eulerAngles;
        rotator.y = isFacingRight ? 180f : 0f;
        transform.rotation = Quaternion.Euler(rotator);
        isFacingRight = !isFacingRight;
    }
}
