using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Variables
    /* SHOOTING GUY VARIABLES STARTS */
    private Vector2 characterPos;
    private Vector2 enemyPos;
    private Vector2 lookDir;
    private GameObject objectToLook;
    public GameObject prefab;
    public float bulletSpeed;
    public float bulletDamage;
    public float addForceMultiplier;
    public float time;
    public float Health;
    public float screenShakeDuration;
    private float timeHolder;
    private float angle;
    private GameObject lastBulletRef;
    public float activasionRange;
    [HideInInspector] public bool shouldFollow = false;
    /* SHOOTING GUY VARIABLES ENDS */

    

    


    //Methods

    /* SHOOTING GUY METHODS STARTS */
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

    private void Shoot()
    {
        lastBulletRef = Instantiate(prefab, gameObject.GetComponent<Transform>().transform.GetChild(1).position, Quaternion.identity);
        
        //There is a bug here, when you close to enemy bullet speed decreases and when you are far from enemy bullet speed increases
        lastBulletRef.GetComponent<Rigidbody2D>().AddForce( lookDir.normalized * bulletSpeed);
        //There is a bug here, when you close to enemy bullet speed decreases and when you are far from enemy bullet speed increases
        
        lastBulletRef.GetComponent<BulletScript>().bulletDamage = bulletDamage;
        //Screenshake Duration
        lastBulletRef.GetComponent<BulletScript>().howMuchShake = screenShakeDuration;

        //Add force Strenght
        lastBulletRef.GetComponent<BulletScript>().addForceStrenght = addForceMultiplier;


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

    //!!!  IF YOU WANT TO DAMAGE THIS OBJECT ACCESS THIS SCRIPT AND CALL(give float value for how much to give damage): TakeDamage(float)  !!!
    public void TakeDamage(float decreaseHealth)
    {
        Health -= decreaseHealth;
        Debug.Log(decreaseHealth + " Damage taken");
    }

    private void Die()
    {
        Destroy(gameObject);
    }



    public void TakeDamageForceAplied(Vector2 direction, float strenght)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * strenght);
    }


    /* SHOOTING GUY METHODS ENDS */





    // Start is called before the first frame update
    void Start()
    {
        /* SHOOTING GUY CODES STARTS */
        timeHolder = time;
        objectToLook = GameObject.Find("Player");
        /* SHOOTING GUY CODES ENDS */

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollow == true)
        {
            /* SHOOTING GUY CODES STARTS */
            Time();
            /* SHOOTING GUY CODES ENDS */
            if (Health <= 0)
            {
                Die();
            }
        }
        
    }

    private void FixedUpdate()
    {

        if (shouldFollow == true)
        {

        /* SHOOTING GUY CODES STARTS */
        ChangeRotationOfEnemy();
            /* SHOOTING GUY CODES ENDS */
        }

    }


}
