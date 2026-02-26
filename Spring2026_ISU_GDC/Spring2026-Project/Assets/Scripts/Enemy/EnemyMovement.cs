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
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigid = GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = Mathf.Abs(playerTransform.position.x - transform.position.x);
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
                    Debug.Log("I can attack you, but i want you to dodge! I am " + tag + ", and I will attack in : " + pauseTimer.ToString());
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
        float direction = playerTransform.position.x - transform.position.x;
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
}
