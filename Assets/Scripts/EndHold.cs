using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndHold : MonoBehaviour
{
    public int currentHold;
    public GameObject nextLevel;
    public GameObject endScreen;
    public bool isEndOfLevel;
    private GameObject[] allEnemies = new GameObject[0];
    // Use this for initialization
    void Start ()
    {
        
        currentHold = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isEndOfLevel)
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        
        if (currentHold >= 3 || (currentHold >= 1 && allEnemies.Length <= 0 && isEndOfLevel))
        {
            if(nextLevel != null)
            {
                Debug.Log("LEVEL COMPLETE!");
                GameObject go = Instantiate(endScreen) as GameObject;
                go.transform.parent = transform;
                go.GetComponent<ScreenCanvas>().customText.text = "YOU WIN!";
                go.GetComponent<ScreenCanvas>().optionButton.transform.GetChild(0).GetComponent<Text>().text = "Next level";
                go.GetComponent<ScreenCanvas>().optionButton.onClick.AddListener(() => GameController.instance.StartCoroutine(GameController.instance.LoadLevel(nextLevel)));
                currentHold = 0;
            }else
            {
                Destroy(gameObject);
                LevelController.instance.NextStage();
            }
            
        }
	}

    
}
