using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Dot : MonoBehaviour
{
    public GameObject dotSet;
    public Origami origami;


    void Start()
    {

    }

    public bool HasDotSet()
    {
        if(dotSet != null)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
