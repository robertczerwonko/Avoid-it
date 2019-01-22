using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystic : MonoBehaviour {

    [SerializeField] private SpriteRenderer[] joystickRenders;
    

    public void disableJoystick()
    {
        foreach(SpriteRenderer sr in joystickRenders)
        {
            if(sr.enabled == true)
            {
                sr.enabled = false;
            }
        }

    }

}
