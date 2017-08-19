using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using System;

public class FriendlyOrigami : MonoBehaviour, IHittable
{
    public int attack;
    public int health;
    public int attackSpeed;
    public CreatureType type;
    public float attackTimer;

    public float movementSpeed;
    private RaycastHit hit;
    private bool hitEnd = false;

	// Use this for initialization
	void Start ()
    {
        attackTimer = attackSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
	}

    void Movement()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10));
        if( Physics.Raycast(transform.position, new Vector3(0,0,1), out hit, 10))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) > 2)
            {
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                hitEnd = false;
                //Timertje
                if (attackTimer <= 0)
                {
                    Attack(hit.transform.GetComponent<EnemyOrigami>());
                    attackTimer = attackSpeed;
                }
            }
            else if (hit.transform.CompareTag("EndHold") && hitEnd == false)
            {
                hitEnd = true;
                EndHold.instance.currentHold += 1;
            }
        }
        else
        {
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }
        
    }

    void Attack(EnemyOrigami enemy)
    {
        if(type == CreatureType.Flying)
        {
            if(enemy.type == CreatureType.Ground)
            {
                enemy.TakeDamage(attack + 10);
            }
            else
            {
                enemy.TakeDamage(attack);
            }
        }
        else if (type == CreatureType.Ground)
        {
            if (enemy.type == CreatureType.Water)
            {
                enemy.TakeDamage(attack + 10);
            }
            else
            {
                enemy.TakeDamage(attack);
            }
        }
        else if (type == CreatureType.Water)
        {
            if (enemy.type == CreatureType.Flying)
            {
                enemy.TakeDamage(attack + 10);
            }
            else
            {
                enemy.TakeDamage(attack);
            }
        }
      
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
