using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    
    void HitStopFunc(float duration)
    {
        StartCoroutine(WaitForTime(duration));
    }

    IEnumerator WaitForTime(float duration)
    {
        Time.timeScale =0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            HitStopFunc(.3f);
        }
    }
}
