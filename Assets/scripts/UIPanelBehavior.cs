using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelBehavior : MonoBehaviour
{
    public void triggerNextPanel() 
    {
        FindObjectOfType<GameManager>().nextScreen();
    }
}
