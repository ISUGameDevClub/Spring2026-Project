using System;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference dogBark;
    
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(dogBark);
        }
    }
}
