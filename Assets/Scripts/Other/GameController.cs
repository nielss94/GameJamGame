using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

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


    public IEnumerator DeactivateObjectForSeconds(GameObject g, float t)
    {
        g.SetActive(false);
        yield return new WaitForSeconds(t);
        
        g.SetActive(true);
    }

    public IEnumerator SpawnOrigami(Origami origami, GameObject lane, GameObject hands)
    {
        Debug.Log("AOOO?");
        GameObject go = Instantiate(hands, new Vector3(Camera.main.transform.position.x, hands.transform.position.y, Camera.main.transform.position.z + 7.5f), hands.transform.rotation) as GameObject;
        yield return new WaitForSeconds(2.5f);
        Debug.Log("AOOO2?");
        Destroy(go);
        GameObject.Find("Main Camera").GetComponent<Player>().SpawnOrigami(origami, lane, LevelController.instance.stageNumber);
    }
}
