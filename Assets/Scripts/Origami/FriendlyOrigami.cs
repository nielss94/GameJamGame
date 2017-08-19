using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using System;
using UnityEngine.UI;

public class FriendlyOrigami : MonoBehaviour, IHittable
{
    public int attack;
    public int health;
    public float attackSpeed;
    public CreatureType type;
    public float attackTimer;
    public Text hitText;
    public float movementSpeed;

    private GameObject canvas;
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
            canvas = transform.GetChild(0).gameObject;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
	}

    void Movement()
    {
        if( Physics.Raycast(transform.position, new Vector3(1,0,0), out hit, 10))
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
                hit.transform.GetComponent<EndHold>().currentHold += 1;
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
        Text go = Instantiate(hitText, canvas.transform.position, hitText.rectTransform.rotation) as Text;
        go.text = damage.ToString();
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        Destroy(go.gameObject, .3f);
    }
}
