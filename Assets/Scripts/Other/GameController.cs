using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

	// Use this for initialization
	void Start () {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator CheckIfLoss()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        if(friendlies.Length <= 0 && GameObject.Find("PaperPile").GetComponent<PaperPile>().folding == false && GameObject.Find("PaperPile").GetComponent<PaperPile>().paperStock <= 0){
            Debug.Log("Game over!");
        }
    }

    public IEnumerator LoadLevel(GameObject level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene");
        yield return async;
        yield return new WaitForSeconds(0.5f);
        LevelController.instance.StartLevel(level);
        GameObject.Find("PaperPile").GetComponent<PaperPile>().paperStock = level.GetComponent<Level>().papers;
    }
}
