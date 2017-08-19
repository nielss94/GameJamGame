using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using System;

public class EnemyOrigami : MonoBehaviour, IHittable
{
    public int attack;
    public int health;
    public int attackSpeed;
    public CreatureType type;
    public float attackTimer;

    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        attackTimer = attackSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit))
        {
            if (attackTimer <= 0)
            {
                Attack(hit.transform.GetComponent<FriendlyOrigami>());
                attackTimer = attackSpeed;
            }    
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Attack(FriendlyOrigami enemy)
    {
        if (type == CreatureType.Flying)
        {
            if (enemy.type == CreatureType.Ground)
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
}
