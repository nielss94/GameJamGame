﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperPile : MonoBehaviour {

    public GameObject paper;
    public GameObject currentPaper;
    public int paperStock;
    public bool folding;
    public Text papersLeft;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        papersLeft.text = paperStock.ToString();
    }
	
	void OnMouseDown()
    {
        if (!folding)
        {
            if (currentPaper == null)
            {
                if (paperStock > 0)
                {
                    GameObject go = Instantiate(paper, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), paper.transform.rotation) as GameObject;
                    currentPaper = go;
                    go.GetComponent<Paper>().paperPile = this;
                    paperStock--;
                }
                else
                {
                    Debug.Log("Paper is out of stock!");
                }

            }
            else
            {
                Debug.Log("There is already a paper that spawned");
            }
        }
        
        
        
    }
    
}
