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
            }else
            {
                player.SpawnOrigami(selectedDot.GetComponent<Dot>().origami);
                ResetDotSet();
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
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0); 
                if (touch.phase == TouchPhase.Moved) 
                {
                    lp = touch.position;
                    GameObject dot = null;
                    foreach (GameObject go in connectDots)
                    {
                       
                        Vector2 goPos = player.GetComponent<Camera>().WorldToScreenPoint(go.transform.position);
                        
                        if (Vector2.Distance(lp, new Vector2(goPos.x, goPos.y)) < 60)
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
                }
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
