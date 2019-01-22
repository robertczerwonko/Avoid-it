using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create Player", menuName = "Player")]
public class Player : ScriptableObject {

    public new string name;

    public Sprite faceImage;

    public PersonalObject PersonalObj;


    public Sprite PersonalObjImage;

}
