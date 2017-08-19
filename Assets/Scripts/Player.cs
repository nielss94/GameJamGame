using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Player : MonoBehaviour {

    public GameObject selectedPiece;
    

    public void SpawnOrigami(Origami origami)
    {
        switch (origami)
        {
            case Origami.Crane:
                Debug.Log("Spawn a crane");
                break;
            case Origami.Dragon:
                Debug.Log("Spawn a dragon");
                break;
            case Origami.Owl:
                Debug.Log("Spawn an owl");
                break;
            case Origami.Frog:
                Debug.Log("Spawn a frog");
                break;
            case Origami.TRex:
                Debug.Log("Spawn a TRex");
                break;
            case Origami.Elephant:
                Debug.Log("Spawn an elephant");
                break;
            case Origami.Manta:
                Debug.Log("Spawn a manta");
                break;
            case Origami.Shark:
                Debug.Log("Spawn a shark");
                break;
            case Origami.Turtle:
                Debug.Log("Spawn a turtle");
                break;
        }
    }
}
