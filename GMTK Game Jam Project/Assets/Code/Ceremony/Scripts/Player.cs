using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //!!!  IF YOU WANT TO DAMAGE THIS OBJECT ACCESS THIS SCRIPT AND CALL(give float value for how much to give damage): TakeDamage(float)  !!!

    //Variables
    public float health;
    public float playerBulletSpeed;
    public float damageByBullet;
    private float angle;
    public float fireRate;
    public float playerBulletForce;
    private float fireRateHolder;
    public float maxMovementSpeed = 10f;
    public float minMovementSpeed = 0f;
    public float howMuchIncreasePerHit;
    public float howMuchDecreasePerHit;
    public float currentMovementSpeed;
    private bool canFire = true;
    private bool sporeMode = false;
    public GameObject playerBullet;
    private Vector2 lookDir;
    public Transform attackPos;
    public float attackRange;
    private bool canAttack = true;
    private float attackTimerHolder;
    public float attackTimer;
    public float damageBySporeAttack;
    public float sporeAttackForce;




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

    //Add force for hit by something

    public void TakeDamageForceAplied(Vector2 direction, float strenght)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * strenght);
    }

    private void PlayerRotatiton()
    {
      
        lookDir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gameObject.transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        gameObject.GetComponent<Transform>().eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            angle
            );
    }

    private void Shoot()
    {
        if (canFire == true)
        {
            GameObject lastBulletRef = Instantiate(playerBullet, gameObject.GetComponent<Transform>().transform.GetChild(0).position, Quaternion.identity);
            lastBulletRef.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * playerBulletSpeed);
            lastBulletRef.GetComponent<PlayerBullet>().damageByBullet = damageByBullet;

            lastBulletRef.GetComponent<PlayerBullet>().addForceStrenght = playerBulletForce;

            lastBulletRef.GetComponent<PlayerBullet>().movementSpeedIncrease = howMuchIncreasePerHit;

            lastBulletRef.GetComponent<PlayerBullet>().movementSpeedDecrease = howMuchDecreasePerHit;

            //bullet rotattion
            lastBulletRef.GetComponent<Transform>().eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y,
                angle
                );
            // set to can't shoot
            canFire = false;
        }

        

    }

    public void IncreaseMovementSpeed(float howMuchIncrease)
    {
        if (currentMovementSpeed < maxMovementSpeed)
        {
            currentMovementSpeed += howMuchIncrease;
        }
        if (currentMovementSpeed > maxMovementSpeed)
        {
            currentMovementSpeed = maxMovementSpeed;
        }
        
    }

    public void DecreaseMovementSpeed(float howMuchIncrease)
    {
        if (currentMovementSpeed > minMovementSpeed)
        {
            currentMovementSpeed -= howMuchIncrease;
        }
        if (currentMovementSpeed < minMovementSpeed)
        {
            currentMovementSpeed = minMovementSpeed;
        }
    }

    private void SporeModeAttack()
    {
        //!! RICK & DECKER THERE IS A BUG OverlapCircleAll() NOT DETECTING MORE THAN 1 OBJECT !!
        //If there is any comment about bug except this probably I solved already just comment stays
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        bool hitAnyEnemy = false;

        foreach (var item in enemiesToDamage)
        {
            Vector2 direction = item.gameObject.GetComponent<Transform>().transform.position - gameObject.transform.position;
            if (item.tag != null && item.tag == "Enemy_Gun" && canAttack == true)
            {
                //Damage
                item.gameObject.GetComponent<EnemyScript>().TakeDamage(damageBySporeAttack);
                hitAnyEnemy = true;

                //Add force
                item.gameObject.GetComponent<EnemyScript>().TakeDamageForceAplied(direction, sporeAttackForce);

                Debug.LogWarning(item.tag);
            }

            if (item.tag != null && item.tag == "Enemy_Malee" && canAttack == true)
            {
                //Damage
                item.gameObject.GetComponent<Enemy_MaleeScript>().TakeDamage(damageBySporeAttack);
                hitAnyEnemy = true;

                //Add force
                item.gameObject.GetComponent<Enemy_MaleeScript>().TakeDamageForceAplied(direction, sporeAttackForce);
                Debug.LogWarning(item.tag);
            }

            if (item.tag != null && item.tag == "Enemy_MaleeAndGun" && canAttack == true)
            {
                //Damage
                item.gameObject.GetComponent<Enemy_MaleeAndGun>().TakeDamage(damageBySporeAttack);
                hitAnyEnemy = true;

                //Add force
                item.gameObject.GetComponent<Enemy_MaleeAndGun>().TakeDamageForceAplied(direction, sporeAttackForce);
                Debug.LogWarning(item.tag);
            }



        }
        if (hitAnyEnemy == true)
        {
            canAttack = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }



    // Start is called before the first frame update
    void Start()
        {
        
         fireRateHolder = fireRate;
         attackTimerHolder = attackTimer;

       
        }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        PlayerRotatiton();
        //Turn to Spore Mode
        if (Input.GetMouseButton(0) && sporeMode == false)
        {
            Shoot();
        }
        else if(Input.GetMouseButton(0) && sporeMode == true)
        {
            SporeModeAttack();
        }

        if (currentMovementSpeed >= maxMovementSpeed)
        {
            sporeMode = true;
            currentMovementSpeed = maxMovementSpeed;
        }

        if (currentMovementSpeed <= minMovementSpeed)
        {
            sporeMode = false;
            currentMovementSpeed = minMovementSpeed;
        }
        if (sporeMode == true)
        {
            currentMovementSpeed -= Time.deltaTime;
        }

        //attakc timer for spore
        //Can attack?
        if (attackTimer <= 0f)
        {
            canAttack = true;
            attackTimer = attackTimerHolder;
        }
        else if (canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }
        


        if (fireRate <= 0 && canFire == false)
        {
            canFire = true;
            fireRate = fireRateHolder;
        }
        else if (fireRate > 0 && canFire == false)
        {
            fireRate -= Time.deltaTime;
        }
    }
}
