using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    [SerializeField] private PlayerController pController;
    [SerializeField] private GameObject StartUI;
    [SerializeField] private CameraController camCont;

    [HideInInspector] public Player CurrentPlayer;

    public int bestScore;
    void Start()
    {
        LoadGame();
    }
    
    void LoadGame()
    {
        bestScore = SaveMananger.instance.state.BestScore;
        StartUI.GetComponent<StartUI>().BestScore(bestScore);
        StartUI.SetActive(true);
    }

    public void SaveGame(int s)
    {
        SaveMananger.instance.state.BestScore = s;
        SaveMananger.instance.Save();
    }

    public void StartGame(Player p)
    {
        CurrentPlayer = p;
        Debug.Log(CurrentPlayer.PersonalObj);
        pController.inGameFaceImage.GetComponent<SpriteRenderer>().sprite = CurrentPlayer.faceImage;
        pController.gameObject.SetActive(true);
        camCont.activeBackground(CurrentPlayer.PersonalObjImage);
        PlayModeManager.Instance.setPlayerObject(p.PersonalObj);
        PlayModeManager.Instance.PauseGame(false);
    }


    public void EndGame(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            SaveGame(bestScore);

        }
        pController.gameObject.transform.position = Vector3.zero;
        pController.gameObject.SetActive(false);
        camCont.deactiveBackground();
        StartUI.GetComponent<StartUI>().BestScore(bestScore);
        StartUI.SetActive(true);
    }



}
