using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHold : MonoBehaviour
{
    public static EndHold instance;
    public int currentHold;
    public GameObject nextLevel;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        currentHold = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (currentHold >= 3)
        {
            if(nextLevel != null)
            {
                LevelController.instance.ChangeStage(nextLevel);
            }else
            {
                Debug.Log("LEVEL COMPLETE!");

            }
            
        }
	}
}
