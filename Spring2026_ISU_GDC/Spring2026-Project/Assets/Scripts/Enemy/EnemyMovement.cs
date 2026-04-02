using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //variables for enemy speed, range, and cooldown.
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private float enemyRange;
    [SerializeField]
    private float enemyCooldownSeconds;
    private float stunTimer = 0.0f;

    //Optional field for additional time before the enemy attacks, when it is in range of player.
    [SerializeField]
    private float enemyPauseTime;

    private float pauseTimer = 0.0f;

    private Transform playerTransform;
    private Rigidbody2D enemyRigid;
    private float playerDist;
    private float timer = 0.0f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(stunTimer < 0.0f) {
            playerDist = Mathf.Abs(playerTransform.position.x - transform.position.x);
            if (playerDist > enemyRange)
            {
                Move();
            }
            else if (playerDist <= enemyRange)
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
                    }
                    else
                    {
                        Attack();
                        timer = enemyCooldownSeconds;
                    }
                }
            }
        }
        timer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;

    }

    private void Move()
    {
        float direction = playerTransform.position.x - transform.position.x;
        // if positive, then enemy is to the left. (needs to move right/positive)
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
            enemyRigid.linearVelocity = new Vector2(1 * enemySpeed, enemyRigid.linearVelocity.y);
        }
        else
        {
            //if direction < 0, then enemy is to the right. (needs to move left/negative)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180 ,0));
            enemyRigid.linearVelocity = new Vector2(-1 * enemySpeed, enemyRigid.linearVelocity.y);
        }

    }

    private void Attack()
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

    public void hitstun(float duration)
    {
        if (stunTimer < duration)
        {
            stunTimer = duration;
        }
    }
}
