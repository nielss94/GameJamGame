using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator CheckIfLoss()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        if(friendlies.Length <= 0 && GameObject.Find("PaperPile").GetComponent<PaperPile>().folding == false && GameObject.Find("PaperPile").GetComponent<PaperPile>().paperStock <= 0){
            Debug.Log("Game over!");
        }
    }
}
