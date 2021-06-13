using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //Variable
    public float damageByBullet;
    public float addForceStrenght;
    public float movementSpeedIncrease;
    public float movementSpeedDecrease;

    //Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 direction = collision.gameObject.GetComponent<Transform>().transform.position - gameObject.transform.position;
        if (collision.gameObject.tag == "Enemy_Gun")
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damageByBullet);
            //Add force
            collision.gameObject.GetComponent<EnemyScript>().TakeDamageForceAplied(direction, addForceStrenght);

            //movementspeed increase
            IncreaseMovementSpeedVar();
        }
        else if (collision.gameObject.tag == "Enemy_Malee")
        {
            collision.gameObject.GetComponent<Enemy_MaleeScript>().TakeDamage(damageByBullet);
            //Add force
            collision.gameObject.GetComponent<Enemy_MaleeScript>().TakeDamageForceAplied(direction, addForceStrenght);
            //movementspeed increase
            IncreaseMovementSpeedVar();
        }
        else if (collision.gameObject.tag == "Enemy_MaleeAndGun")
        {
            collision.gameObject.GetComponent<Enemy_MaleeAndGun>().TakeDamage(damageByBullet);
            //Add force
            collision.gameObject.GetComponent<Enemy_MaleeAndGun>().TakeDamageForceAplied(direction, addForceStrenght);
            //movementspeed increase
            IncreaseMovementSpeedVar();
        }
        else
        {
            if (collision.gameObject.tag == "Wall")
            {
                DecreaseMovementSpeedVar();
            }
            
        }

        //DestroyBullet
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }

        


    }


    public void IncreaseMovementSpeedVar()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().IncreaseMovementSpeed(movementSpeedIncrease);
    }

    public void DecreaseMovementSpeedVar()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().DecreaseMovementSpeed(movementSpeedDecrease);
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
