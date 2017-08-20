using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeBlock : MonoBehaviour {
    
    private Vector3 lp;
    public List<GameObject> connectDots;
    private Player player;
    public bool selected;
    public GameObject selectedDot;
    public GameObject dotSet;
    public GameObject standardDotSet;
    public PaperPile paperPile;
    public GameObject selectedLane;

    void Start()
    {

        player = GameObject.Find("Main Camera").GetComponent<Player>();
        dotSet = gameObject.transform.GetChild(0).gameObject;
        foreach (Transform t in dotSet.transform)
        {
            connectDots.Add(t.gameObject);
        }
    }

    void OnMouseDown()
    {
        player.selectedPiece = gameObject;
        selected = true;
    }

    void OnMouseUp()
    {
        selected = false;
        if (selectedDot != null)
        {
            Debug.Log("Release with selected dot: " + selectedDot.transform.name);
            if (selectedDot.GetComponent<Dot>().HasDotSet())
            {
                SetNewDotSet(selectedDot.GetComponent<Dot>().dotSet);
                switch (selectedDot.name)
                {
                    case "Water":
                        Instantiate(selectedDot.GetComponent<Dot>().animatingObject,
                       new Vector3(Camera.main.transform.position.x + 15, selectedDot.GetComponent<Dot>().animatingObject.transform.position.y,
                       selectedDot.GetComponent<Dot>().animatingObject.transform.position.z), selectedDot.GetComponent<Dot>().animatingObject.transform.rotation);
                        break;
                    case "Flying":
                        Instantiate(selectedDot.GetComponent<Dot>().animatingObject,
                       new Vector3(Camera.main.transform.position.x + 7.5f, selectedDot.GetComponent<Dot>().animatingObject.transform.position.y,
                       selectedDot.GetComponent<Dot>().animatingObject.transform.position.z), selectedDot.GetComponent<Dot>().animatingObject.transform.rotation);
                        break;
                    case "Ground":
                        Instantiate(selectedDot.GetComponent<Dot>().animatingObject,
                       new Vector3(Camera.main.transform.position.x, selectedDot.GetComponent<Dot>().animatingObject.transform.position.y,
                       selectedDot.GetComponent<Dot>().animatingObject.transform.position.z), selectedDot.GetComponent<Dot>().animatingObject.transform.rotation);
                        break;
                }
               
                GameController.instance.StartCoroutine(GameController.instance.DeactivateObjectForSeconds(transform.parent.gameObject, 1));
            }else
            {
                //Add selected lane
                player.SpawnOrigami(selectedDot.GetComponent<Dot>().origami, selectedLane, LevelController.instance.stageNumber);
                ResetDotSet();
                paperPile.folding = false;
                Destroy(transform.parent.gameObject);
            }
            selectedDot = null;
        }
        else
        {
            Debug.Log("Release without selected dot");
        }
        player.selectedPiece = null;
    }

    void Update()
    {
        if (selected)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
              //  Touch touch = Input.GetTouch(0); 
               // if (touch.phase == TouchPhase.Moved) 
               // {
                    lp = Input.mousePosition;
                    //lp = touch.position;
                    GameObject dot = null;
                    foreach (GameObject go in connectDots)
                    {
                       
                        Vector2 goPos = player.GetComponent<Camera>().WorldToScreenPoint(go.transform.position);
                        
                        if (Vector2.Distance(lp, new Vector2(goPos.x, goPos.y)) < 30)
                        {
                            go.GetComponent<MeshRenderer>().material.color = Color.red;
                            dot = go;
                        }
                        else
                        {
                            go.GetComponent<MeshRenderer>().material.color = Color.white;
                        }
                        
                    }
                    if(dot != null)
                    {
                        selectedDot = dot;
                    }else
                    {
                        selectedDot = null;
                    }
              //  }
            }
        }
    }

    void SetNewDotSet(GameObject newDotSet)
    {
        if (dotSet != null)
        {
            Destroy(dotSet);
        }
        GameObject go = Instantiate(newDotSet, newDotSet.transform.position, newDotSet.transform.rotation) as GameObject;
        go.transform.SetParent(transform,false);
        dotSet = go;

        connectDots.Clear();
        foreach (Transform t in dotSet.transform)
        {
            connectDots.Add(t.gameObject);
        }
    }
    
    void ResetDotSet()
    {
        if (dotSet != null)
        {
            Destroy(dotSet);
        }
        GameObject go = Instantiate(standardDotSet, standardDotSet.transform.position, standardDotSet.transform.rotation) as GameObject;
        go.transform.SetParent(transform, false);
        dotSet = go;

        connectDots.Clear();
        foreach (Transform t in dotSet.transform)
        {
            connectDots.Add(t.gameObject);
        }
    }

}
