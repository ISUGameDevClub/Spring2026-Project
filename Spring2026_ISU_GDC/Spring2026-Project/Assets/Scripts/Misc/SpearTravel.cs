using UnityEngine;

public class SpearTravel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public Vector2 direction;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name =="Player")
        {
            //Destroy(gameObject);
        }
    }
}
