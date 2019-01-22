using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentCollider : MonoBehaviour {

    public PersonalObject pObj;
    private SpriteRenderer spriteRend;

    private int wallHits;
    private int maxWallHits = 4;

	void Start()
    {
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * Random.Range(3f,4.2f);
        GetComponent<Rigidbody2D>().angularVelocity = 100f;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Borders": wallBang(col);
                break;
            case "Player": playerBang(col);
                break;
        }
       
    }


    void wallBang(Collision2D col)
    {
        wallHits++;
        Color tmp = spriteRend.color;
        tmp = Color.Lerp(Color.white, Color.red, (float)wallHits/10f + 0.1f);
        spriteRend.color = tmp;
        if (wallHits >= maxWallHits)
        {
            PlayModeManager.Instance.DestroyObject(gameObject,false);
        }
    }

    void playerBang(Collision2D col)
    {
        if (GameManager.Instance.CurrentPlayer.PersonalObj == pObj)
        {
            col.gameObject.GetComponent<PlayerController>().ActiveFadeScore();
            PlayModeManager.Instance.DestroyObject(gameObject, true);
        }
        else
        {
            PlayModeManager.Instance.EndGame();
        }
    }
}
