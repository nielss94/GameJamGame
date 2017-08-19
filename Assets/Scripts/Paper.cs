using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public PaperPile paperPile;
    private RaycastHit hit;

    public GameObject selectedLane;
    public GameObject swipeBlock;
    

	void OnMouseDrag()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = pos.z + 5;
        transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(pos);

        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            if (hit.transform.CompareTag("Lane"))
            {
                SelectLane(hit.transform.gameObject);
            }
        }
    }

    void OnMouseUp()
    {
        if (selectedLane != null)
        {

            paperPile.folding = true;
            GameObject go = Instantiate(swipeBlock, swipeBlock.transform.position, swipeBlock.transform.rotation) as GameObject;
            go.GetComponent<SwipeBlock>().selectedLane = selectedLane;
            go.GetComponent<SwipeBlock>().paperPile = paperPile;
            selectedLane.GetComponent<MeshRenderer>().material.color = Color.white;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Release without a lane selected");
        }
    }

    void SelectLane(GameObject lane)
    {
        if(selectedLane != null)
        {
            selectedLane.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        selectedLane = lane;
        selectedLane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
