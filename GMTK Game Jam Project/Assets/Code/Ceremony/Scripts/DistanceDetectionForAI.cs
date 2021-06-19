using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetectionForAI : MonoBehaviour
{
    //Variables
    private GameObject playerObj;
    private float activasionRange;



    //Methods
    public void checkPlayerIsNear()
    {
        float magnatude = Vector2.Distance((Vector2)playerObj.GetComponent<Transform>().position, (Vector2)gameObject.transform.position);
        //Debug.Log(magnatude);
        if (magnatude < activasionRange)
        {
            if (gameObject.tag == "Enemy_Gun")
            {
                gameObject.GetComponent<EnemyScript>().shouldFollow = true;
            }
            else if (gameObject.tag == "Enemy_Malee")
            {
                gameObject.GetComponent<Enemy_MaleeScript>().shouldFollow = true;
            }
            else if (gameObject.tag == "Enemy_MaleeAndGun")
            {
                gameObject.GetComponent<Enemy_MaleeAndGun>().shouldFollow = true;
            }


        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (gameObject.tag == "Enemy_Gun")
        {
            activasionRange = gameObject.GetComponent<EnemyScript>().activasionRange;
        }
        else if (gameObject.tag == "Enemy_Malee")
        {
            activasionRange = gameObject.GetComponent<Enemy_MaleeScript>().activasionRange;
        }
        else if (gameObject.tag == "Enemy_MaleeAndGun")
        {
            activasionRange = gameObject.GetComponent<Enemy_MaleeAndGun>().activasionRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerIsNear();
    }
}
