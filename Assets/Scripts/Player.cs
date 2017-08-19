using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Player : MonoBehaviour {

    public GameObject selectedPiece;

    public GameObject crane;
    public GameObject dragon;
    public GameObject owl;
    public GameObject frog;
    public GameObject trex;
    public GameObject elephant;
    public GameObject manta;
    public GameObject shark;
    public GameObject turtle;
    
    
    public void SpawnOrigami(Origami origami, GameObject lane, int stageNumber)
    {
        string s = "Spawn";
        switch (lane.name)
        {
            case "Lane1":
                s += "1";
                break;
            case "Lane2":
                s += "2";
                break;
            case "Lane3":
                s += "3";
                break;
        }
        switch (stageNumber)
        {
            case 1:
                s += "Stage1";
                break;
            case 2:
                s += "Stage2";
                break;
            case 3:
                s += "Stage3";
                break;
        }
        Debug.Log(s);

        switch (origami)
        {
            case Origami.Crane:
                Debug.Log("Spawn a crane");
                Instantiate(crane, GameObject.Find(s).transform.position, crane.transform.rotation);
                break;
            case Origami.Dragon:
                Debug.Log("Spawn a dragon");
                Instantiate(dragon, GameObject.Find(s).transform.position, dragon.transform.rotation);
                break;
            case Origami.Owl:
                Debug.Log("Spawn an owl");
                Instantiate(owl, GameObject.Find(s).transform.position, owl.transform.rotation);
                break;
            case Origami.Frog:
                Debug.Log("Spawn a frog");
                Instantiate(frog, GameObject.Find(s).transform.position, frog.transform.rotation);
                break;
            case Origami.TRex:
                Debug.Log("Spawn a TRex");
                Instantiate(trex, GameObject.Find(s).transform.position, trex.transform.rotation);
                break;
            case Origami.Elephant:
                Debug.Log("Spawn an elephant");
                Instantiate(elephant, GameObject.Find(s).transform.position, elephant.transform.rotation);
                break;
            case Origami.Manta:
                Debug.Log("Spawn a manta");
                Instantiate(manta, GameObject.Find(s).transform.position, manta.transform.rotation);
                break;
            case Origami.Shark:
                Debug.Log("Spawn a shark");
                Instantiate(shark, GameObject.Find(s).transform.position, shark.transform.rotation);
                break;
            case Origami.Turtle:
                Debug.Log("Spawn a turtle");
                Instantiate(turtle, GameObject.Find(s).transform.position, turtle.transform.rotation);
                break;
        }
    }
}
