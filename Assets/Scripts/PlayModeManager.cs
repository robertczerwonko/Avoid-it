using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayModeManager : MonoBehaviour {

    #region Sigleton
    public static PlayModeManager Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private int maximumSpawnedObjects = 30;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject PlayModeUI;
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Joystic joystick2;


    private GameObject playerObject;
    private int spawnedObjects;
    private int allDestroyedObjectsByPlayer;
    private int score;

    private float timeBetweenSpawn = 0.7f;

    private List<GameObject> tempSpawnedObjetcs = new List<GameObject>();

    private bool readyToSpawn = true;
    [SerializeField]private bool pauseGame = false;


    void Update()
    {
        if (!pauseGame)
        {
            StartFun();
        }
    }


    void StartFun()
    {
        if (spawnedObjects < maximumSpawnedObjects && readyToSpawn)
        {
            StartCoroutine(SpawnPrefab());
        }
    }

    IEnumerator SpawnPrefab()
    {
        readyToSpawn = false;

        GameObject tempObject = Instantiate(choosePreFab(), choosePosition(), Quaternion.identity) as GameObject;
        tempSpawnedObjetcs.Add(tempObject);
        spawnedObjects++;
        yield return new WaitForSeconds(timeBetweenSpawn);
        readyToSpawn = true;
    }

    GameObject choosePreFab()
    {
        return Random.Range(0, 2) == 0 ? prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)] : playerObject;
    }

    Vector3 choosePosition()
    {
        return spawnPoints[Random.Range(0,spawnPoints.Length)].position;
    }

    void makeMoreFun()
    {
        allDestroyedObjectsByPlayer++;
        if(allDestroyedObjectsByPlayer >= 10)
        {
            Debug.Log("UTRUDNIAM!");
            allDestroyedObjectsByPlayer = 0;
            if (timeBetweenSpawn > 0.3f)
            {
                timeBetweenSpawn -= 0.1f;
            }
        }
    }

    void editScore()
    {
        PlayModeUI.SetActive(true);
        score++;
        if (score > GameManager.Instance.bestScore)
            GameManager.Instance.SaveGame(score);
        scoreText.text = score.ToString();
    }

    void resetScore()
    {
        PlayModeUI.SetActive(false);
        score = 0;
        spawnedObjects = 0;
        timeBetweenSpawn = 1f;
        allDestroyedObjectsByPlayer = 0;
        scoreText.text = score.ToString();
    }

    void clearObjectList()
    {
        foreach(GameObject obj in tempSpawnedObjetcs)
        {
            Destroy(obj);
        }
        tempSpawnedObjetcs.Clear();
    }
    #region public methods

    public void PauseGame(bool b)
    {
        pauseGame = b;
    }


    public void setPlayerObject(PersonalObject gObj)
    {
        foreach(GameObject go in prefabsToSpawn)
        {
            if(go.GetComponent<InstrumentCollider>().pObj == gObj)
            {
                playerObject = go;
                break;
            }
        }

    }
    public void DestroyObject(GameObject gO,bool addPoint)
    {
        tempSpawnedObjetcs.Remove(gO);
        spawnedObjects--;
        Destroy(gO);
        if (addPoint)
        {
            editScore();
            makeMoreFun();
        }
    }

    public void EndGame()
    {
        joystick2.disableJoystick();
        PauseGame(true);
        clearObjectList();
        GameManager.Instance.EndGame(score);
        resetScore();
    }

    #endregion
}
