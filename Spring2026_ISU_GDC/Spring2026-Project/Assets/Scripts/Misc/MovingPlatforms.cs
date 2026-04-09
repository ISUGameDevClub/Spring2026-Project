using UnityEngine;
using ISUGameDev.SpearGame.Player.Movement;

namespace ISUGameDev.SpearGame
{
    public class MovingPlatforms : MonoBehaviour
    {
        [SerializeField] private Vector3[] destinations;
        [SerializeField] private float speed;

        private Vector3 startPos;
        private int location = -1;
        private bool returning = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!returning)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinations[location + 1], speed * Time.deltaTime);
                if (transform.position == destinations[location + 1])
                {
                    location++;
                    if (location == destinations.Length - 1)
                    {
                        returning = true;
                    }
                }
            }
            else
            {
                if (location != 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinations[location - 1], speed * Time.deltaTime);
                    if (transform.position == destinations[location - 1])
                    {
                        location--;
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                    if (transform.position == startPos)
                    {
                        location--;
                        returning = false;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.GetComponent<PlayerMovement>().SetMovingPlatform(gameObject, transform.position);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.GetComponent<PlayerMovement>().SetMovingPlatform(null, Vector3.zero);
            }
        }
    }
}
