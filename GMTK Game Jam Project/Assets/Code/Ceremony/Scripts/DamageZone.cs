using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy_Gun")
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(5f);
        }
        else if (collision.gameObject.tag == "Enemy_Malee")
        {
            collision.gameObject.GetComponent<Enemy_MaleeScript>().TakeDamage(5f);
        }
        else if (collision.gameObject.tag == "Enemy_MaleeAndGun")
        {
            collision.gameObject.GetComponent<Enemy_MaleeAndGun>().TakeDamage(5f);
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
