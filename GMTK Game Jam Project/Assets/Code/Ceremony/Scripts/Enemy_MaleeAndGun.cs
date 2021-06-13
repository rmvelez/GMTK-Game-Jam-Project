using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MaleeAndGun : MonoBehaviour
{
    //Variables
    /* MALEE STARTS*/

    public float speed = 10f;
    private float angle;
    public float attackRange;
    public float attackTimer;
    public float screenShakeDurationForMalee;
    public float addForceStrenghtMalee;
    private float attackTimerHolder;
    public int damage;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 characterPos;
    private Vector2 enemyPos;
    private Vector2 lookDir;
    private GameObject objectToLook;
    public Transform attackPos;
    
    private bool canAttack = true;

    /* MALEE ENDS*/

    /* GUN STARTS*/
    public GameObject prefab;
    private GameObject lastBulletRef;
    public float bulletSpeed;
    public float bulletDamage;
    public float addForceMultiplierBullet;
    public float screenShakeForBulletDuration;
    public float time;
    private float timeHolder;
    /* GUN ENDS*/

    /* COMMON */
    public float Health;
    /* COMMON */

    //Methods
    /* MALEE STARTS*/

    //There is a bug enemy slowing down when as get closer to character
    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)gameObject.GetComponent<Transform>().position + (direction.normalized * speed));

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

                //Screen Shake
                GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShakeManager>().TriggerShake(screenShakeDurationForMalee);

                //Add force Strenght
                Vector2 direction = item.gameObject.GetComponent<Transform>().transform.position - gameObject.transform.position;

                item.gameObject.GetComponent<Player>().TakeDamageForceAplied(direction, addForceStrenghtMalee);
            }


        }
        //Can attack?
        if (attackTimer <= 0f)
        {
            canAttack = true;
            attackTimer = attackTimerHolder;
        }
        else if (canAttack == false)
        {
            attackTimer -= UnityEngine.Time.deltaTime;
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    /* MALEE ENDS*/

    /* GUN STARTS*/

    private void Shoot()
    {
        lastBulletRef = Instantiate(prefab, gameObject.GetComponent<Transform>().transform.GetChild(1).position, Quaternion.identity);

        //There is a bug here, when you close to enemy bullet speed decreases and when you are far from enemy bullet speed increases
        lastBulletRef.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * bulletSpeed);
        //There is a bug here, when you close to enemy bullet speed decreases and when you are far from enemy bullet speed increases

        lastBulletRef.GetComponent<BulletScript>().bulletDamage = bulletDamage;
        //Screenshake Duration
        lastBulletRef.GetComponent<BulletScript>().howMuchShake = screenShakeForBulletDuration;

        //Add force Strenght
        lastBulletRef.GetComponent<BulletScript>().addForceStrenght = addForceMultiplierBullet;

        lastBulletRef.GetComponent<Transform>().eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            angle
            );
    }

    private void Time()
    {
        time -= UnityEngine.Time.deltaTime;
        if (time <= 0)
        {
            time = timeHolder;
            Shoot();
        }
    }

    public void TakeDamageForceAplied(Vector2 direction, float strenght)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * strenght);
    }

    /* GUN ENDS*/

    /* COMMON */
    public void TakeDamage(float decreaseHealth)
    {
        Health -= decreaseHealth;
        Debug.Log(decreaseHealth + " Damage taken");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    /* COMMON */


    // Start is called before the first frame update
    void Start()
    {

        /* MALEE STARTS*/
        rb = this.GetComponent<Rigidbody2D>();
        objectToLook = GameObject.Find("Player");
        attackTimerHolder = attackTimer;
        /* MALEE ENDS*/

        /* GUN STARTS*/
        timeHolder = time;
        objectToLook = GameObject.Find("Player");
        /* GUN ENDS*/

        /* COMMON */

        /* COMMON */

    }

    // Update is called once per frame
    void Update()
    {

        /* MALEE STARTS*/
        Attack();
        /* MALEE ENDS*/

        /* GUN STARTS*/
        Time();
        /* GUN ENDS*/

        /* COMMON */
        if (Health <= 0)
        {
            Die();
        }
        /* COMMON */

    }

    private void FixedUpdate()
    {

        /* MALEE STARTS*/
        movement = objectToLook.GetComponent<Transform>().position - this.transform.position;
        MoveCharacter(movement);
        ChangeRotationOfEnemy();
        /* MALEE ENDS*/

        /* GUN STARTS*/

        /* GUN ENDS*/

    }
}
