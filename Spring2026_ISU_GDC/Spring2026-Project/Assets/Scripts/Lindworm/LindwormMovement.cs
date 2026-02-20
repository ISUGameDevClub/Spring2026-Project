using UnityEngine;
using UnityEngine.Splines;

public class LindwormMovement : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float stunLength = 0.5f;
    private float stunCounter = 0;
    private float positionPercent;
    private bool forward = true;
    
    
    void Start()
    {
    }

    
    void Update()
    {
        stunCounter -= Time.deltaTime;

        if (stunCounter <= 0f)
        {
            if (forward)
            {
                positionPercent += speed * Time.deltaTime;
            }
            else
            {
                positionPercent -= speed * Time.deltaTime;
            }

            Vector3 currentPosition = spline.EvaluatePosition(positionPercent);
            transform.position = currentPosition;
            if (positionPercent > 1f && forward)
            {
                forward = false;
            }
            else if (positionPercent < 0f && !forward)
            {
                forward = true;
            }
        }
        

    }
}
