using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject[] cameraPositions;

	// Use this for initialization
	void Start () {
        Invoke("GetCameraPositions", 1);   
	}
    
	
	// Update is called once per frame
	void Update () {
        if(cameraPositions.Length > 0)
        {
            transform.position = Vector3.Lerp(transform.position,
           new Vector3(GameObject.Find("CameraPosStage" + LevelController.instance.stageNumber).transform.position.x,
           transform.position.y, transform.position.z), 1 * Time.deltaTime);
        }
	}

    void GetCameraPositions()
    {
        cameraPositions = GameObject.FindGameObjectsWithTag("CameraPos");
    }
}
