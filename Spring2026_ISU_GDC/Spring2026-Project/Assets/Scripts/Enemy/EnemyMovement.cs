using Nomad.Core.Events;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    //variables for enemy speed, range, and cooldown.
    [SerializeField] public float enemySpeed;
    [SerializeField] public float enemyRange;
    [SerializeField] public float enemyCooldownSeconds;

    //Optional field for additional time before the enemy attacks, when it is in range of player.
    [SerializeField] public float enemyPauseTime;
    private float pauseTimer = 0f;

    private Transform playerTransform;
    private Rigidbody2D enemyRigid;
    private float playerDist;
    private float timer = 0f;
    
    /// <summary>
    /// A continually updated position that the enemy will move towards
    /// </summary>
    private Vector3 targetPosition;
    
    /// <summary>
    /// Event system reference to subscribe OnPlayerMove to event 
    /// </summary>
    private IGameEvent<Vector3> _onPlayerMoveEvent;
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigid = GetComponent<Rigidbody2D>();
        
        _onPlayerMoveEvent = GameEventRegistry.GetEvent<Vector3>("OnPlayerMoveEvent", nameof(EnemyMovement1));
        _onPlayerMoveEvent.Subscribe(this, OnPlayerMove);
    }

    // Update is called once per frame
    void Update()
    {
        //playerDist = Mathf.Abs(playerTransform.position.x - transform.position.x);
        playerDist = Mathf.Abs(targetPosition.x - transform.position.x);
        if (playerDist > enemyRange)
        {
            Move();
        } else if (playerDist <= enemyRange)
        {
            enemyRigid.linearVelocity = new Vector2(0, enemyRigid.linearVelocity.y);
            if (timer <= 0f)
            {
                if (enemyPauseTime > 0)
                {
                    //Debug.Log("I can attack you, but i want you to dodge! I am " + tag + ", and I will attack in : " + pauseTimer.ToString());
                    pauseTimer -= Time.deltaTime;
                    if (pauseTimer <= 0f)
                    {
                        Attack();
                        timer = enemyCooldownSeconds;
                        pauseTimer = enemyPauseTime;
                    }
                } else
                {
                    Attack();
                    timer = enemyCooldownSeconds;
                }
            }
        }
        timer -= Time.deltaTime;
    }

    void Move()
    {
        float direction = targetPosition.x - transform.position.x;
        // if positive, then enemy is to the left. (needs to move right/positive)
        if (direction > 0)
        {
            enemyRigid.linearVelocity = new Vector2(1 * enemySpeed, enemyRigid.linearVelocity.y);
        } else
        {
            //if direction < 0, then enemy is to the right. (needs to move left/negative)
            enemyRigid.linearVelocity = new Vector2(-1 * enemySpeed, enemyRigid.linearVelocity.y);
        }

    }

    void Attack()
    {
        if (tag.ToString().Equals("StabEnemy"))
        {
            Debug.Log("replace with stab attack function");
        }
        else if (tag.ToString().Equals("RangedEnemy"))
        {
            Debug.Log("replace with attack function");
        }
    }

    /// <summary>
    /// An event callback that is called when the player moves.
    /// </summary>
    /// <param name="playerPos">New position of player upon moving</param>
    void OnPlayerMove(in Vector3 newPlayerPosition)
    {
        Debug.Log("Player moved" + newPlayerPosition);
        targetPosition = newPlayerPosition;
    }
}
