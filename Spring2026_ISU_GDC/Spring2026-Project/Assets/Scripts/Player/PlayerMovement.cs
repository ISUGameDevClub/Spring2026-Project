    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Tooltip("Player's top speed.")]
    private float moveSpeed = 15;
    [SerializeField] [Tooltip("How fast player speeds up.")]
    private float acceleration = 7;
    [SerializeField] [Tooltip("How fast player slows down.")]
    private float decceleration = 7;
    [SerializeField] [Tooltip("Determines how acceleration works.\n< 1: Acceleration starts strong and then becomes weaker.\n1: Linear acceleration.\n> 1: Starts slow then becomes strong.")]
    private float velPower = 0.9f;
    [Space]
    [SerializeField] [Tooltip("How much the player slows down when grounded and not pressing any movement input.")]
    private float frictionAmount = 0.2f;

    [Header("Jump")]
    [SerializeField] [Tooltip("How much force the player's jump has.")]
    private float jumpForce = 20;
    [SerializeField] [Range(0, 1)] [Tooltip("How much the upwards velocity from a jump decreases when the jump input is released early.")]
    private float jumpCutMultiplier = 0.5f;
    [SerializeField] [Tooltip("How much time a player still has to jump after walking off an edge.")]
    private float jumpCoyoteTime = 0.15f;
    [SerializeField] [Tooltip("How much time a player can press the jump button before landing and still jump.")]
    private float jumpBufferTime = 0.1f;
    [SerializeField] [Tooltip("How much the player's gravity is multiplied by when jump height peak is reached.")]
    private float jumpHangGravityMult = 0.9f;
    [SerializeField] [Tooltip("What minimum velocity the player's jump needs to hit to activate the jump hang gravity multiplier.")]
    private float jumpHangThreshold = 1.5f;

    [Space]

    [SerializeField] [Tooltip("How many walljumps the player can do before having to touch the ground. Set to 0 to disable wall jumps.")]
    private int numWallJumps = 1;
    [SerializeField] [Tooltip("If enabled, overrides \"numWalJumps\" and gives the player unlimited wall jumps.")]
    private bool infWallJumps = true;
    [SerializeField] [Tooltip("The horizontal force that gets applied to the player when they wall jump.")]
    private float wallJumpForce = 15;
    [SerializeField] [Range(0, 1)] [Tooltip("The multiplier applied to jumps when performed off a wall.")]
    private float wallJumpHeightMult = 0.8f;
    [SerializeField] [Range(0, 1)] [Tooltip("How much the player's input movement is multiplied by after performing a wall jump.\nDesigned to make it so the player can't immediately return to the same wall for infinite wall jumps.")]
    private float wallJumpMovementMult = 0.1f;
    [SerializeField] [Tooltip("How long the player's movement is affected after a wall jump.")]
    private float wallJumpMovementTime = 0.3f;
    [SerializeField] [Tooltip("Maximum speed a player falls when pressed against a wall.")]
    private float wallSlidingSpeed = 2;

    [Space]

    [SerializeField] [Tooltip("How much the gravity is mutiplied by when falling.")]
    private float fallGravityMultiplier = 2;

    [Space]

    [SerializeField] [Tooltip("The top falling speed a player can reach while not holding the jump button.")]
    private float topFallingSpeedRelease = 20;
    [SerializeField] [Tooltip("The top falling speed a player can reach while holding the jump button.")]
    private float topFallingSpeedHold = 17;

    [Space]

    [SerializeField] [Tooltip("How many double jumps the player can perform. Set to 0 to disable double jumps.")]
    private int numDoubleJumps = 0;

    [HideInInspector]
    [Header("Camera")]
    [SerializeField] private GameObject cameraFollow;
    //private CameraFollowObject cameraFollowObject;
    private float fallSpeedyDampingChangeThreshold;

    [Header("Other")]
    [SerializeField]
    [Tooltip("The minimum distance from the center of the player downwards to detect the ground. Used for determining when the player is grounded.")]
    private float distanceFromFloor = 1.02f;
    [SerializeField] [Tooltip("The minimum distance from the center of the player outwards to detect a wall. Used for detecting when the player is pressed up against a wall.")]
    private float distanceFromWall = 0.6f;

    [Header("Private")]
    Rigidbody2D rb;
    private float accelerationDefault;
    private float deccelerationDefault;
    private float lastGroundedTime;
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
       // Debug.Log("Jump");
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
