using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class FriendlyOrigami : MonoBehaviour
{
    public int attack;
    public int health;
    public int attackSpeed;
    public CreatureType type;

    public float movementSpeed;
    private RaycastHit hit;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
	}

    void Movement()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10));
        if( Physics.Raycast(transform.position, new Vector3(0,0,1), out hit))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) > 2)
            {
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                Attack();
            }
        }
        
    }

    void Attack()
    {
        
    }
}
