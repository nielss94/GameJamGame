using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndHold : MonoBehaviour
{
    public int currentHold;
    public GameObject nextLevel;
    public GameObject endScreen;


	// Use this for initialization
	void Start ()
    {
        currentHold = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (currentHold >= 3)
        {
            if(nextLevel != null)
            {
                Debug.Log("LEVEL COMPLETE!");
                GameObject go = Instantiate(endScreen) as GameObject;
                go.GetComponent<ScreenCanvas>().customText.text = "YOU WIN!";
                go.GetComponent<ScreenCanvas>().optionButton.transform.GetChild(0).GetComponent<Text>().text = "Next level";
                go.GetComponent<ScreenCanvas>().optionButton.onClick.AddListener(() => LevelController.instance.LoadLevel(nextLevel));
                currentHold = 0;
            }else
            {
                Destroy(gameObject);
                LevelController.instance.NextStage();
            }
            
        }
	}

    
}
