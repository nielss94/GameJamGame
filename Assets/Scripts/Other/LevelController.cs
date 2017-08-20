using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public GameObject currentStage;
    public int stageNumber;
    // Use this for initialization
    void Start()
    {
        stageNumber = 1;
        instance = this;
    }

    public void StartLevel(GameObject level)
    {
        GameObject go = Instantiate(level, level.transform.position, level.transform.rotation) as GameObject;
        currentStage = go;
    }

    public void LoadLevel(GameObject level)
    {
        Destroy(currentStage);
        GameObject go = Instantiate(level, level.transform.position, level.transform.rotation) as GameObject;

        currentStage = go;
    }
    

    public void NextStage()
    {
       stageNumber++;
    }
    
}
