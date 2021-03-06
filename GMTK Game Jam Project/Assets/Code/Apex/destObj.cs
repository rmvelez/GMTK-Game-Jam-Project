using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destObj : MonoBehaviour
{
    //Destuctable Object script

    // This script can be put on a player, enemy, or destrutrable object such as a wall, crate, table, chair, etc.

    //HP is the amount of health an enemy has
    // Sheild could regenerate back over time (after a short delay) in spore form or on armored/ sheilded enimes
    //Remains is the death sprite such as a dead guard on the ground or a broken and splintered table
    // Keycard is the keycard prefab spawned on death

    //holdingKeycard is true if the enemy is captain/ special and is holding a card that drops on death

    public int hp;
    public int sheild;
    public GameObject remains;
    public GameObject deathEffect;
    public GameObject _keycard;

    public bool holdingKeycard;

    //Todo: add death sound effect


    void Update()
    {
        //die when dead
        if (hp <= 0)
        {

            if (deathEffect != null)
            {
                GameObject effy = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(effy, 5f);
            }
            Destroy(gameObject);

            if (holdingKeycard ==true)
            {

                GameObject kkeycard = Instantiate(_keycard, transform.position, transform.rotation) as GameObject;
            }
            if (remains != null)
            {
                GameObject body = Instantiate(remains, transform.position, Quaternion.identity);

                // might not need the following?
                //Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
                //Destroy(gameObject);
            }
        }
    }
    //TakeDamage is how things take damage. Things that deal damage should have a damage int and pass that on
    public void TakeDamage(int damage)
    {
        hp -= damage;


    }
}
