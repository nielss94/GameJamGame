using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using System;
using UnityEngine.UI;

public class EnemyOrigami : MonoBehaviour, IHittable
{
    public int attack;
    public int health;
    public int attackSpeed;
    public CreatureType type;
    public float attackTimer;
    public Text hitText;
    public GameObject hitParticle;

    private GameObject canvas;

    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        attackTimer = attackSpeed;
        canvas = transform.GetChild(0).gameObject;
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

        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) < 2)
            {
                if (attackTimer <= 0)
                {
                    Attack(hit.transform.GetComponent<FriendlyOrigami>());
                    attackTimer = attackSpeed;
                }
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

        GameObject particle = Instantiate(hitParticle, new Vector3(go.transform.position.x - 0.8f, go.transform.position.y - 1, go.transform.position.z - 1), hitParticle.transform.rotation) as GameObject;
        Destroy(particle.gameObject, .3f);
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
