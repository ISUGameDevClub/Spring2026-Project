using System.Collections;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{

    bool Falling;
    
    
    async Task OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && Falling == false)
        {
           
            
            Debug.Log("Touched");

            
            
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;

            Falling = true;
           
         StartCoroutine(PlatformFall());
         

        }

        
        }

        public IEnumerator PlatformFall()
        {   
            Debug.Log("Fall first one");

            if (Falling == true)
            {
                    
                yield return new WaitForSeconds(2.5f);
                 
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                
                Debug.Log("Fall");
                


                gameObject.transform.position = new Vector3(6.58f, -2.45f, 0f);



                Falling = false;

            }

        }
}
