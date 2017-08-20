using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Enums;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public AudioClip propClip;
    private AudioSource audioSource;
    public GameObject endScreen;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator CheckIfLoss()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        if(friendlies.Length <= 0 && GameObject.Find("PaperPile").GetComponent<PaperPile>().folding == false && GameObject.Find("PaperPile").GetComponent<PaperPile>().paperStock <= 0){
            Debug.Log("Game over!");
            GameObject go = Instantiate(endScreen) as GameObject;
            go.transform.parent = transform;
            go.GetComponent<ScreenCanvas>().customText.text = "YOU LOSE!";
            go.GetComponent<ScreenCanvas>().optionButton.transform.GetChild(0).GetComponent<Text>().text = "Try again";
            go.GetComponent<ScreenCanvas>().optionButton.onClick.AddListener(() => StartCoroutine(LoadLevel(LevelController.instance.currentStage)));
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

        GameObject go = Instantiate(hands, new Vector3(Camera.main.transform.position.x, hands.transform.position.y, Camera.main.transform.position.z + 7.5f), hands.transform.rotation) as GameObject;
        audioSource.PlayOneShot(propClip);
        yield return new WaitForSeconds(2.5f);
        Destroy(go);
        GameObject.Find("Main Camera").GetComponent<Player>().SpawnOrigami(origami, lane, LevelController.instance.stageNumber);
    }
}
