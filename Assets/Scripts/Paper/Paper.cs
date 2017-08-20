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
        Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
        transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);


        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
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
            GameObject go = Instantiate(swipeBlock, new Vector3(Camera.main.transform.position.x + 7.5f, Camera.main.transform.position.y - 2.5f, Camera.main.transform.position.z + 7.5F), swipeBlock.transform.rotation) as GameObject;
            go.transform.Find("SwipeBlock").GetComponent<SwipeBlock>().selectedLane = selectedLane;
            go.transform.Find("SwipeBlock").GetComponent<SwipeBlock>().paperPile = paperPile;
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
        selectedLane.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
