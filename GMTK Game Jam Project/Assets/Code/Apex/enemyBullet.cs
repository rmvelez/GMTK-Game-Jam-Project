using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRb;
    Collider2D bullCollider;
    [SerializeField] public int bulletDamage;

    [SerializeField] private GameObject particle;



    // Start is called before the first frame update
    void Start()
    {
        bullCollider = gameObject.GetComponent<Collider2D>();
        //bullCollider.isTrigger = true;
        Destroy(gameObject, 0.7f);
    }

    //turns trigger off as soon as it leaves player hitbox
   // private void OnTriggerExit2D(Collider2D other) { if (other.gameObject.CompareTag("Player")) { bullCollider.isTrigger = false; } }

    //spawns impact particle
    private void SpawnParticle()
    {
        GameObject part = (GameObject)GameObject.Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(part, 5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bulletRb.angularVelocity = 0;
        destObj _other = other.collider.gameObject.GetComponent<destObj>();
        //Wall
        if (other.gameObject.CompareTag("Wall"))
        {

            SpawnParticle();
            _other.hp -= bulletDamage;
            Destroy(gameObject);
        }
        //Enemy
        if (other.gameObject.CompareTag("Player"))
        {
            if (_other != null)
            {
                _other.hp -= bulletDamage;
                SpawnParticle();
                Destroy(gameObject);
                Debug.Log("Hit player");
            }

        }
        //Destructable Object
        if (other.gameObject.CompareTag("Destructable Object"))
        {
            if (_other != null)
            {
                _other.TakeDamage(bulletDamage);
                SpawnParticle();
                Destroy(gameObject);
            }
        }
    }
}
