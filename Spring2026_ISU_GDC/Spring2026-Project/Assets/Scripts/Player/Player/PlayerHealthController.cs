using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private GameObject layoutGroup;
    [SerializeField] private GameObject featherPrefab;
    [SerializeField] private int maxHealthFeathers;
    private List<Feather> featherList;
    private int curFeatherIndex = -1;
    public bool cannotTakeDamage = false;

    private void Start()
    {
        featherList = new List<Feather>();
        InitializeHealthIcons();
    }
   
    
    private void InitializeHealthIcons()
    {
        for (int i = 0; i < maxHealthFeathers; i++)
        { 
            Debug.Log("Creating feather");
           GameObject featherObj = Instantiate(featherPrefab, layoutGroup.transform);
           Feather featherComponent = featherObj.GetComponent<Feather>();
           featherList.Add(featherComponent);
           curFeatherIndex++;
        }
    }
    
    public void TakeOneDamage()
    {
        if (curFeatherIndex < 0) { return;}
        
        if (cannotTakeDamage == false)
        {
            featherList[curFeatherIndex].isActive = false;
            curFeatherIndex--;
            
            if (curFeatherIndex == 0)
            {
                //player death
                PlayerDeath();
            }
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("PLAYER DIED");
    }
}
