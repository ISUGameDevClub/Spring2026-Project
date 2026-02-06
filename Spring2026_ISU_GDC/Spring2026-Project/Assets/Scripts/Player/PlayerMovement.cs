using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15;
    [SerializeField] private float acceleration = 7;
    [SerializeField] private float decceleration = 7;
    [SerializeField] private float velPower = 0.9f;
    [Space]
    [SerializeField] private float frictionAmount = 0.2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 20;
    [SerializeField] [Range(0, 1)] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float jumpCoyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float jumpHangGravityMult = 0.9f;
    [SerializeField] private float jumpHangThreshold = 1.5f;

    [Space]

    [SerializeField] private int numWallJumps = 1;
    [SerializeField] private float wallJumpForce = 15;
    [SerializeField] [Range(0, 1)] private float wallJumpHeightMult = 0.8f;
    [SerializeField] [Range(0, 1)] private float wallJumpMovementMult = 0.1f;
    [SerializeField] private float wallJumpMovementTime = 0.3f;

    [Space]

    [SerializeField] private float fallGravityMultiplier = 2;

    [Space]

    [SerializeField] private float topFallingSpeedRelease = 20;
    [SerializeField] private float topFallingSpeedHold = 17;

    [Space]

    [SerializeField] private int numDoubleJumps = 0;

    [HideInInspector]
    [Header("Camera")]
    [SerializeField] private GameObject cameraFollow;
    //private CameraFollowObject cameraFollowObject;
    private float fallSpeedyDampingChangeThreshold;

    [Header("Other")]
    [SerializeField] private bool infWallJumps = true;
    [SerializeField] private float distanceFromFloor = 1.02f;
    [SerializeField] private float wallSlidingSpeed = 2;
    [SerializeField] private float distanceFromWall = 0.6f;

    [Header("Private")]
    Rigidbody2D rb;
    private float accelerationDefault;
    private float deccelerationDefault;
    [SerializeField] private float lastGroundedTime;
    private float lastJumpTime;
    private float gravityScale;
    private bool isJumping;
    //used for jumpcut
    private bool jumpInputReleased;
    private bool touchingWall;
    private int numWallJumpsDefault;
    private int wallDirection;
    private bool isFacingRight;
    private bool canTurn;
    private bool isWallSliding;
    private int numDoubleJumpsDefault;

    /*
    RunningParticles runningParticles;
    bool isRunningParticles;

    WallSlidingParticles wallSlidingParticles;
    bool isWallSlidingParticles;
    */

    float moveInput;
    // Start is called before the first frame update
    void Start()
    {
        lastJumpTime = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        //runningParticles = GameObject.Find("RunningPS").GetComponent<RunningParticles>();
        //wallSlidingParticles = GameObject.Find("WallSlidingPS").GetComponent<WallSlidingParticles>();
        isJumping = true;
        gravityScale = rb.gravityScale;
        touchingWall = false;
        //isRunningParticles = false;
        //isWallSlidingParticles = false;

        accelerationDefault = acceleration;
        deccelerationDefault = decceleration;
        numWallJumpsDefault = numWallJumps;
        numDoubleJumpsDefault = numDoubleJumps;
        isFacingRight = true;
        canTurn = true;
        isWallSliding = false;

        //cameraFollowObject = cameraFollow.GetComponent<CameraFollowObject>();

        //fallSpeedyDampingChangeThreshold = CameraManager.instance.fallSpeedyDampingChangeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see what direction the player is holding
        moveInput = Input.GetAxisRaw("Horizontal");

        //Jump Input
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = 0;
        }

        //If we are falling past a certain threshold
        /*
        if (rb.velocity.y < fallSpeedyDampingChangeThreshold && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        */

        //If we are standing still or moving up
        /*
        if (rb.velocity.y >= 0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.lerpedFromPlayerFalling)
        {
            //reset so it can bee called again
            CameraManager.instance.lerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
        */
    }
    void FixedUpdate()
    {
        #region Check Floor
        //Updating lastGroundedTime
        lastGroundedTime += Time.deltaTime;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, -Vector2.up, distanceFromFloor);
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
        //Check for wall
        touchingWall = false;
        hit = Physics2D.RaycastAll(transform.position, Vector2.left, distanceFromWall);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Ground")
            {
                touchingWall = true;
                wallDirection = -1;
            }
        }

        hit = Physics2D.RaycastAll(transform.position, Vector2.right, distanceFromWall);
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
        //calculate the direction the player wants to move in
        float targetSpeed = moveInput * moveSpeed;
        //calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.linearVelocity.x;
        //change acceleration rate depending on situation
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        //calculates the movement value to apply to the player
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
        #endregion

        #region Friction
        //check if we are grounded and trying to stop (not holding a direction)
        if (lastGroundedTime == 0 && moveInput == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.linearVelocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Timer
        lastJumpTime += Time.deltaTime;
        #endregion

        #region Jump
        if ((lastJumpTime < jumpBufferTime && lastGroundedTime < jumpCoyoteTime) && !isJumping)
            Jump();
        else if (jumpInputReleased && numDoubleJumps > 0 && lastGroundedTime != 0 && lastJumpTime == Time.deltaTime && !touchingWall)
        {
            Jump();
            numDoubleJumps--;
            jumpInputReleased = false;
        }
        #endregion

        #region Regrab
        float maxFallSpeed = Input.GetButton("Jump") ? -topFallingSpeedHold : -topFallingSpeedRelease;
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
        #endregion

        #region Jump Gravity
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        #endregion

        #region Jump Cut
        if (!jumpInputReleased && lastGroundedTime != 0 && !Input.GetButton("Jump"))
            OnJumpUp();
        #endregion

        #region Jump Hang
        if (lastGroundedTime != 0 && Mathf.Abs(rb.linearVelocity.y) < jumpHangThreshold)
        {
            //Reduce gravity
            rb.gravityScale = gravityScale * jumpHangGravityMult;
        }
        #endregion

        #region Wall Jump
        if (isJumping && lastJumpTime < jumpBufferTime && (numWallJumps > 0 || infWallJumps) && jumpInputReleased && touchingWall)
        {
            Walljump();
        }
        #endregion

        #region Turn Check
        if (moveInput != 0 && canTurn)
        {
            TurnCheck();
        }
        #endregion

        #region Wall Slide
        //Checks if against wall for wall sliding
        if (rb.linearVelocity.y < 0 && touchingWall && moveInput == wallDirection && lastGroundedTime != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        //Wallsliding
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        #endregion
        /*
        #region Particles

        #region Running
        if (!isRunningParticles && moveSpeed - Mathf.Abs(rb.velocity.x) < 2 && lastGroundedTime == 0)
        {
            isRunningParticles = true;
            runningParticles.EnableParticles(true, moveInput);
        }
        else if (isRunningParticles && (moveSpeed - Mathf.Abs(rb.velocity.x) >= 2 || lastGroundedTime != 0))
        {
            isRunningParticles = false;
            runningParticles.EnableParticles(false, moveInput);
        }
        #endregion

        #region Wall Sliding
        if (isWallSliding != isWallSlidingParticles)
        {
            isWallSlidingParticles = isWallSliding;
            wallSlidingParticles.EnableParticles(isWallSliding, wallDirection);
            Debug.Log(wallDirection);
        }

        #endregion

        #endregion
        */
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        Debug.Log("Jump");
    }

    private void Walljump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(-Vector2.right * wallDirection * wallJumpForce, ForceMode2D.Impulse);
        numWallJumps--;
        canTurn = false;
        if ((wallDirection == 1 && isFacingRight) || (wallDirection == -1 && !isFacingRight))
        {
            Turn();
        }
        StartCoroutine(WalljumpMovementTimer());
    }

    private IEnumerator WalljumpMovementTimer()
    {
        acceleration *= wallJumpMovementMult;
        decceleration *= wallJumpMovementMult;
        yield return new WaitForSeconds(wallJumpMovementTime);
        acceleration = accelerationDefault;
        decceleration = deccelerationDefault;
        canTurn = true;
    }

    private void OnJumpUp()
    {
        if (rb.linearVelocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector2.down * rb.linearVelocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }

        jumpInputReleased = true;
    }

    private void TurnCheck()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);

        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        isFacingRight = !isFacingRight;
        //Turn the camera follow object
        //cameraFollowObject.CallTurn();
    }
}
