using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    [SerializeField] private Button[] _PlayerButtons;
    [SerializeField] Image personalObjImage;
    [SerializeField] Text playerName;
    [SerializeField] Text BestScoreText;

    private Player choosedPlayer;

    void Start()
    {
        foreach (Button btn in _PlayerButtons)
        {
            btn.onClick.AddListener(() => Player1BTN_Click(btn.GetComponent<PlayerInformation>()._player));
        }
    }

    public void Player1BTN_Click(Player _player1)
    {
        choosedPlayer = _player1;
        PresentPlayer();
    }


    public void StartGameBTN_Click()
    {
        if(choosedPlayer == null)
        {
            playerName.text = "MAYBE PLAYER? CHOOSE!";
        }
        else
        {
            GameManager.Instance.StartGame(choosedPlayer);
            gameObject.SetActive(false);
        }
    }

    public void BestScore(int bs)
    {
        Debug.Log("USTAWIAM BEST SCORE!");
        BestScoreText.text = "Best Score: " + bs;  
    }

    void PresentPlayer()
    {
        personalObjImage.sprite = choosedPlayer.PersonalObjImage;
        playerName.text = choosedPlayer.name;
    }

    
}
