using System.Collections;
using UnityEngine;

public class FlashHit : MonoBehaviour
{

    bool isHit;
    void update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag.Equals("Player"))
        {
            if (!isHit)
            {
                StartCoroutine(FlashSprite(hit));
            }
            
        }
    }

    IEnumerator FlashSprite(GameObject hit)
    {
        isHit = true;
        Color32 baseColor = hit.GetComponent<SpriteRenderer>().color;
        Color32 flashColor = new Color32(255,255,255,150);
        yield return new WaitForSeconds(.25f);
        hit.GetComponent<SpriteRenderer>().color = flashColor;
        yield return new WaitForSeconds(.25f);
         hit.GetComponent<SpriteRenderer>().color = baseColor;
         yield return new WaitForSeconds(.25f);
        hit.GetComponent<SpriteRenderer>().color = flashColor;
        yield return new WaitForSeconds(.25f);
         hit.GetComponent<SpriteRenderer>().color = baseColor;
         isHit = false;
    }
}
