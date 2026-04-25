using System;
using UnityEngine;
using UnityEngine.UI;

public class Feather : MonoBehaviour
{
    public bool isActive;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private void Update()
    {
        if (isActive)
        {
            GetComponent<Image>().sprite = activeSprite;
        }
        else
        {
            GetComponent<Image>().sprite = inactiveSprite;
        }
    }
}
