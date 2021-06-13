﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //Variables
    public float bulletDamage;

    //Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage to player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
        }

        //Destroy bullet
        if (collision.gameObject.tag != "Enemy_Gun" && collision.gameObject.tag != "Enemy_Malee" && collision.gameObject.tag != "Enemy_Malee" && collision.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
