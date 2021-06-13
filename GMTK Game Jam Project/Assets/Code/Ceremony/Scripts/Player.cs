using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //!!!  IF YOU WANT TO DAMAGE THIS OBJECT ACCESS THIS SCRIPT AND CALL(give float value for how much to give damage): TakeDamage(float)  !!!

    //Variables
    public float health;


    //Methods
    public void TakeDamage(float decreaseHealth)
    {
        //decrease helth
        health -= decreaseHealth;
        Debug.Log(decreaseHealth + " Damage Given This Object" + gameObject.name);

        //force
        /*
        gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * forceMultiplier);
        */
    }
    
    private void Die()
    {
        Destroy(gameObject);
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
}
