using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControlsScript : MonoBehaviour
{

    [SerializeField] GameObject MenuContainer;
    [SerializeField] GameObject ControlContainer;
    [SerializeField] TMP_Dropdown Control01;
    [SerializeField] TMP_Dropdown Control02;
    [SerializeField] TMP_Dropdown Control03;
    [SerializeField] TMP_Dropdown Control04;
    [SerializeField] TMP_Dropdown Control05;
    [SerializeField] TMP_Dropdown Control06;
    // Update is called once per frame
    void Update()
    {

    }
    public void Control1()
    {
        int val = Control01.value;
        if (val == 0)
        {

        }
        else if (val == 1) 
        {
             
        }
        else if (val == 2)
        {
        
        }
    }
    public void Control2()
    {
        int val = Control02.value;
        if (val == 0)
        {

        }
        else if (val == 1)
        {

        }
        else if (val == 2)
        {

        }
    }
    public void Control3()
    {
        int val = Control03.value;
        if (val == 0)
        {

        }
        else if (val == 1)
        {

        }
        else if (val == 2)
        {

        }
    }
    public void Control4()
    {
        int val = Control04.value;
        if (val == 0)
        {

        }
        else if (val == 1)
        {

        }
        else if (val == 2)
        {

        }
    }
    public void Control5()
    {
        int val = Control05.value;
        if (val == 0)
        {

        }
        else if (val == 1)
        {

        }
        else if (val == 2)
        {

        }
    }
    public void Control6()
    {
        int val = Control06.value;
        if (val == 0)
        {

        }
        else if (val == 1)
        {

        }
        else if (val == 2)
        {

        }
    }
    public void BackButton()
    {
        ControlContainer.SetActive(false);
        MenuContainer.SetActive(true);
    }
}
