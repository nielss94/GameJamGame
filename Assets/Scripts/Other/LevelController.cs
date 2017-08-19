using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        StartLevel();
    }

    public void StartLevel()
    {
        GameObject go = Instantiate(currentStage, currentStage.transform.position, currentStage.transform.rotation) as GameObject;
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

    public void ResetOrigamiPos()
    {
        GameObject[] allOrigamis = GameObject.FindGameObjectsWithTag("Friendly");
        foreach(GameObject go in allOrigamis)
        {
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z - 20);
        }
    }
}
