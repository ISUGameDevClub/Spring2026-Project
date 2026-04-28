using System;
using UnityEngine;

public class MaintainRotationOnUpdate : MonoBehaviour
{
    private Vector3 rotation;
    
    private void Start()
    {
        rotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(rotation);
    }
}
