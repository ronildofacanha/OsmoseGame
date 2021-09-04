using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abcde : MonoBehaviour
{
    public Button[] Buttons;
   

    public void click()
    {
        foreach (Button btn in Buttons)
        {
                btn.interactable = false;
        }
    }

}
