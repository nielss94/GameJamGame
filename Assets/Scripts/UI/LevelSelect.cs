using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public List<GameObject> levels;
    public Button levelButton;
    
	void Start () {
        GetLevels();
	}
	

    void GetLevels()
    {
        foreach (GameObject go in levels)
        {
            Button b = Instantiate(levelButton) as Button;
            b.transform.SetParent(transform);
            b.transform.GetChild(0).GetComponent<Text>().text = go.GetComponent<Level>().levelName + " \n\n  " + go.GetComponent<Level>().papers.ToString() + " papers";
            b.onClick.AddListener(() => LoadLevel(go));
        }
    }

    void LoadLevel(GameObject level)
    {
        GameController.instance.StartCoroutine(GameController.instance.LoadLevel(level));
    }
}
