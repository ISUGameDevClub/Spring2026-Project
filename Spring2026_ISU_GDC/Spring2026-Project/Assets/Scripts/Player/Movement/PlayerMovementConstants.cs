using UnityEngine;

public class PlayerMovementConstants : MonoBehaviour
{
    /*                                              
    ██▄  ▄██  ▄▄▄  ▄▄ ▄▄ ▄▄▄▄▄ ▄▄   ▄▄ ▄▄▄▄▄ ▄▄  ▄▄ ▄▄▄▄▄▄ 
    ██ ▀▀ ██ ██▀██ ██▄██ ██▄▄  ██▀▄▀██ ██▄▄  ███▄██   ██   
    ██    ██ ▀███▀  ▀█▀  ██▄▄▄ ██   ██ ██▄▄▄ ██ ▀██   ██   
    */
    [Header("Movement")]
    [SerializeField, Tooltip("Player's top speed.")]
    private float moveSpeed = 15f;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField, Tooltip("How fast player speeds up.")]
    private float acceleration = 7f;
    public float Acceleration { get => acceleration; private set => acceleration = value; }

    [SerializeField, Tooltip("How fast player slows down.")]
    private float decceleration = 7f;
    public float Decceleration { get => decceleration; private set => decceleration = value; }

    [SerializeField, Tooltip("Determines how acceleration works.\n< 1: Acceleration starts strong and then becomes weaker.\n1: Linear acceleration.\n> 1: Starts slow then becomes strong.")]
    private float velPower = 0.9f;
    public float VelPower { get => velPower; private set => velPower = value; }

    [Space]
    [SerializeField, Tooltip("How much the player slows down when grounded and not pressing any movement input.")]
    private float frictionAmount = 0.2f;
    public float FrictionAmount { get => frictionAmount; private set => frictionAmount = value; }


    /*             
       ██ ▄▄ ▄▄ ▄▄   ▄▄ ▄▄▄▄  
       ██ ██ ██ ██▀▄▀██ ██▄█▀ 
    ████▀ ▀███▀ ██   ██ ██                        
     */
    [Header("Jump")]
    [SerializeField, Tooltip("How much force the player's jump has.")]
    private float jumpForce = 20f;
    public float JumpForce { get => jumpForce; private set => jumpForce = value; }

    [SerializeField, Range(0, 1), Tooltip("How much the upwards velocity from a jump decreases when the jump input is released early.")]
    private float jumpCutMultiplier = 0.5f;
    public float JumpCutMultiplier { get => jumpCutMultiplier; private set => jumpCutMultiplier = value; }

    [SerializeField, Tooltip("How much time a player still has to jump after walking off an edge.")]
    private float jumpCoyoteTime = 0.15f;
    public float JumpCoyoteTime { get => jumpCoyoteTime; private set => jumpCoyoteTime = value; }

    [SerializeField, Tooltip("How much time a player can press the jump button before landing and still jump.")]
    private float jumpBufferTime = 0.1f;
    public float JumpBufferTime { get => jumpBufferTime; private set => jumpBufferTime = value; }

    [SerializeField, Tooltip("How much the player's gravity is multiplied by when jump height peak is reached.")]
    private float jumpHangGravityMult = 0.9f;
    public float JumpHangGravityMult { get => jumpHangGravityMult; private set => jumpHangGravityMult = value; }

    [SerializeField, Tooltip("What minimum velocity the player's jump needs to hit to activate the jump hang gravity multiplier.")]
    private float jumpHangThreshold = 1.5f;
    public float JumpHangThreshold { get => jumpHangThreshold; private set => jumpHangThreshold = value; }

    [Space]
    [SerializeField, Tooltip("How many walljumps the player can do before having to touch the ground. Set to 0 to disable wall jumps.")]
    private int numWallJumps = 1;
    public int NumWallJumps { get => numWallJumps; private set => numWallJumps = value; }

    [SerializeField, Tooltip("If enabled, overrides \"numWallJumps\" and gives the player unlimited wall jumps.")]
    private bool infWallJumps = true;
    public bool InfWallJumps { get => infWallJumps; private set => infWallJumps = value; }

    [SerializeField, Tooltip("The horizontal force that gets applied to the player when they wall jump.")]
    private float wallJumpForce = 15f;
    public float WallJumpForce { get => wallJumpForce; private set => wallJumpForce = value; }

    [SerializeField, Range(0, 1), Tooltip("The multiplier applied to jumps when performed off a wall.")]
    private float wallJumpHeightMult = 0.8f;
    public float WallJumpHeightMult { get => wallJumpHeightMult; private set => wallJumpHeightMult = value; }

    [SerializeField, Range(0, 1), Tooltip("How much the player's input movement is multiplied by after performing a wall jump.\nDesigned to make it so the player can't immediately return to the same wall for infinite wall jumps.")]
    private float wallJumpMovementMult = 0.1f;
    public float WallJumpMovementMult { get => wallJumpMovementMult; private set => wallJumpMovementMult = value; }

    [SerializeField, Tooltip("How long the player's movement is affected after a wall jump.")]
    private float wallJumpMovementTime = 0.3f;
    public float WallJumpMovementTime { get => wallJumpMovementTime; private set => wallJumpMovementTime = value; }

    [SerializeField, Tooltip("Maximum speed a player falls when pressed against a wall.")]
    private float wallSlidingSpeed = 2f;
    public float WallSlidingSpeed { get => wallSlidingSpeed; private set => wallSlidingSpeed = value; }

    [Space]
    [SerializeField, Tooltip("How much the gravity is multiplied by when falling.")]
    private float fallGravityMultiplier = 2f;
    public float FallGravityMultiplier { get => fallGravityMultiplier; private set => fallGravityMultiplier = value; }

    [Space]
    [SerializeField, Tooltip("The top falling speed a player can reach while not holding the jump button.")]
    private float topFallingSpeedRelease = 20f;
    public float TopFallingSpeedRelease { get => topFallingSpeedRelease; private set => topFallingSpeedRelease = value; }

    [SerializeField, Tooltip("The top falling speed a player can reach while holding the jump button.")]
    private float topFallingSpeedHold = 17f;
    public float TopFallingSpeedHold { get => topFallingSpeedHold; private set => topFallingSpeedHold = value; }

    [Space]
    [SerializeField, Tooltip("How many double jumps the player can perform. Set to 0 to disable double jumps.")]
    private int numDoubleJumps = 0;
    public int NumDoubleJumps { get => numDoubleJumps; private set => numDoubleJumps = value; }


    /*                           
    ▄████▄ ▄▄▄▄▄▄ ▄▄ ▄▄ ▄▄▄▄▄ ▄▄▄▄  
    ██  ██   ██   ██▄██ ██▄▄  ██▄█▄ 
    ▀████▀   ██   ██ ██ ██▄▄▄ ██ ██                  
     */
    [Header("Other")]
    [SerializeField, Tooltip("The minimum distance from the center of the player downwards to detect the ground. Used for determining when the player is grounded.")]
    private float distanceFromFloor = 1.02f;
    public float DistanceFromFloor { get => distanceFromFloor; private set => distanceFromFloor = value; }

    [SerializeField, Tooltip("The minimum distance from the center of the player outwards to detect a wall. Used for detecting when the player is pressed up against a wall.")]
    private float distanceFromWall = 0.6f;
    public float DistanceFromWall { get => distanceFromWall; private set => distanceFromWall = value; }
}
