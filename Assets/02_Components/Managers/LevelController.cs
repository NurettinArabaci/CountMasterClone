using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    GameObject[] levelPrefabs;

    public static LevelController Instance;

    public int Level
    {
        get { return PlayerPrefs.GetInt("Level", 0); }
        set { PlayerPrefs.SetInt("Level", value); }
    }

    private void Awake()
    {
        Instance = this;

        levelPrefabs = Resources.LoadAll<GameObject>("Levels");

        LevelCreate();
    }

    private void OnEnable()
    {
        EventManager.LevelCompleted += OnLevelComplete;
     
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= OnLevelComplete;
    }

    void OnLevelComplete()
    {
        Level++;

    }

    public void RestartLevelButton()
    {
        Destroy(GameObject.FindGameObjectWithTag(Tags.Level));
        LevelCreate();

    }

    public void NextLevelButton()
    {
        Destroy(GameObject.FindGameObjectWithTag(Tags.Level));
        LevelCreate();

        StackController.playerChildAmount = 1;
    }


    void LevelCreate()
    {
        Instantiate(levelPrefabs[Level%levelPrefabs.Length]);
    }


}
