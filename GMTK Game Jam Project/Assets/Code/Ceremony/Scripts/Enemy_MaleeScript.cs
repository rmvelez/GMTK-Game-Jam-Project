using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MaleeScript : MonoBehaviour
{
    //Variables
    /*
    public float walkingSpeed;
    private Transform targetObj;
    */

    public float speed = 10f;
    private float angle;
    public float Health;
    public float attackRange;
    public int damage;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 characterPos;
    private Vector2 enemyPos;
    private Vector2 lookDir;
    private GameObject objectToLook;
    public Transform attackPos;
    public float attackTimer;
    private float attackTimerHolder;
    private bool canAttack = true;
    



    //Methods

    //There is a bug enemy slowing down when as get closer to character
    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)gameObject.GetComponent<Transform>().position + (direction * speed));
        
    }
    //There is a bug enemy slowing down when as get closer to character


    private void ChangeRotationOfEnemy()
    {
        characterPos = objectToLook.GetComponent<Transform>().position;
        enemyPos = gameObject.GetComponent<Transform>().position;
        lookDir = characterPos - enemyPos;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        gameObject.GetComponent<Transform>().eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            angle
            );

    }

    private void Attack()
    {
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        
        foreach (var item in enemiesToDamage)
        {
            if (item.tag != null && item.tag == "Player" && canAttack == true)
            {
                //Player Damage
                item.GetComponent<Player>().TakeDamage(damage);
                canAttack = false;
            }
            
            
        }
        //Can attack?
        if (attackTimer <= 0f)
        {
            canAttack = true;
            attackTimer = attackTimerHolder;
        }
        else if(canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    public void TakeDamage(float decreaseHealth)
    {
        Health -= decreaseHealth;
        Debug.Log(decreaseHealth + " Damage taken");
    }

    private void Die()
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        objectToLook = GameObject.Find("Player");
        attackTimerHolder = attackTimer;



    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
        
            Attack();
            
            
        
        
        
    }

    private void FixedUpdate()
    {
        movement = objectToLook.GetComponent<Transform>().position - this.transform.position;
        MoveCharacter(movement);
        ChangeRotationOfEnemy();
    }
}
