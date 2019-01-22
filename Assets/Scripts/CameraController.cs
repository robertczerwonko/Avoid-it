using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject guitarBackground;


    public void activeBackground(Sprite pObjImage)
    {

        guitarBackground.GetComponent<SpriteRenderer>().sprite = pObjImage;
        guitarBackground.SetActive(true);

    }

    public void deactiveBackground()
    {
      guitarBackground.SetActive(false);
    }
	
}
