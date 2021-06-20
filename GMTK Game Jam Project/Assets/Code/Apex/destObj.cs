using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destObj : MonoBehaviour
{
    //Destuctable Object script

    // This script can be put on a player, enemy, or destrutrable object such as a wall, crate, table, chair, etc.

    //HP is the amount of health an enemy has
    //Remains is the death sprite such as a dead guard on the ground or a broken and splintered table
    // Keycard is the keycard prefab spawned on death

    //holdingKeycard is true if the enemy is captain/ special and is holding a card that drops on death

    public int hp;
    public bool isEnemy;
    public GameObject remains;
    public GameObject remains2;
    public GameObject remains3;
    public GameObject remains4;
    public GameObject remains5;
    public int force;
    public GameObject deathEffect;
    public GameObject _keycard;
    private spore _spore;

    public bool holdingKeycard;

    //Todo: add death sound effect
     void Start()
    {
        _spore = GameObject.Find("SporeBarFill").GetComponent<spore>();
    }

    void Update()
    {
        
        //die when dead
        if (hp <= 0)
        {       
            //For keycard
            if (holdingKeycard == true)
            { GameObject kkeycard = Instantiate(_keycard, transform.position, transform.rotation) as GameObject; }

            //leave behind a corpse sprite and up to 4 gibs
            if (remains != null)
            { GameObject body = Instantiate(remains, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); }
            if (remains2 != null)
            { GameObject body2 = Instantiate(remains2, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); Rigidbody2D rb2 = body2.GetComponent<Rigidbody2D>(); GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * force; }
            if (remains3 != null)
            { GameObject body3 = Instantiate(remains3, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); Rigidbody2D rb3 = body3.GetComponent<Rigidbody2D>(); GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * force; }
            if (remains4 != null)
            { GameObject body4 = Instantiate(remains4, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); Rigidbody2D rb4 = body4.GetComponent<Rigidbody2D>(); GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * force; }
            if (remains5 != null)
            { GameObject body5 = Instantiate(remains5, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); Rigidbody2D rb5 = body5.GetComponent<Rigidbody2D>(); GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * force; }
            //rb.AddForce(transform.right * force)_

            //Death effect
            if (deathEffect != null)
            { GameObject effy = Instantiate(deathEffect, transform.position, Quaternion.identity);  Destroy(effy, 5f);}

            if (isEnemy == true)
            {
                _spore.GetMore(10);
                Debug.Log("Added to charge bar");
            }

            Destroy(gameObject);
        }
    }
    //_sporeo.current += 10;

    //TakeDamage is how things take damage. Things that deal damage should have a damage int and pass that on
    public void TakeDamage(int damage)
    {
        hp -= damage;

    }
   /* public void CheckForSporeStuff() 
    {
        Debug.Log("Checked");
        if (hp <= 0) 
        {
            SporeStuff();
            Debug.Log("Called Spore stuff");
        }
    }
    public void SporeStuff()
    {
        if (isEnemy == true) 
        {
           _spore.GetMore(10);
            Debug.Log("Added to charge bar");
        }
    }*/
}
