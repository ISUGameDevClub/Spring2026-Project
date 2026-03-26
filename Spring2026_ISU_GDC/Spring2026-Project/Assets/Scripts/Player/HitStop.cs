using System.Collections;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player
{
    public class HitStop : MonoBehaviour
    {
        private void HitStopFunc(float duration)
        {
            StartCoroutine(WaitForTime(duration));
        }

        private IEnumerator WaitForTime(float duration)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1;
        }

        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("s"))
            {
                HitStopFunc(.3f);
            }
        }
    }
}