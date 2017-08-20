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
    private AudioSource audioSource;
    public AudioClip paperHit;
    public AudioClip dieClip;
    public Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
	}

    void Movement()
    {
        if( Physics.Raycast(transform.position, new Vector3(1,0,0), out hit, 10))
        {
            if(Vector3.Distance(transform.position, hit.transform.position) > 2)
            {
                animator.SetBool("Walking", true);
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                animator.SetBool("Walking", false);
                hitEnd = false;
                //Timertje
                if (attackTimer <= 0)
                {
                    StartCoroutine(WaitAndSetFalse("Attacking"));
                    Attack(hit.transform.GetComponent<EnemyOrigami>());
                    attackTimer = attackSpeed;
                }
            }
            else if (hit.transform.CompareTag("EndHold") && hitEnd == false)
            {
                animator.SetBool("Walking", false);
                hitEnd = true;
                hit.transform.parent.GetComponent<EndHold>().currentHold += 1;
            }
        }
        else
        {
            animator.SetBool("Walking", true);
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }
        
    }

    IEnumerator WaitAndSetFalse(string animName)
    {
        animator.SetBool(animName, true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(animName, false);
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
        audioSource.PlayOneShot(paperHit);
        health -= damage;
        Text go = Instantiate(hitText, canvas.transform.position, hitText.rectTransform.rotation) as Text;
        go.text = damage.ToString();
        go.transform.SetParent(canvas.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        Destroy(go.gameObject, .3f);
        if (health <= 0)
        {
            audioSource.PlayOneShot(dieClip);
            transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 0.5f);
            GameController.instance.StartCoroutine(GameController.instance.CheckIfLoss());
        }
    }
}
