using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private bool isGameOver;
    private bool isSporeForm;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int moveSpeed=10;
    private Vector2 movement;
    public float movey;

    private bool hasKey1;
    private bool hasKey2;
    private bool hasKey3;
    private bool hasKey4;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isSporeForm = false;
        hasKey1 = false; hasKey2 = false; hasKey3 = false; hasKey4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        movey = movement.x = Input.GetAxisRaw("Horizontal");
        movey = movement.y = Input.GetAxisRaw("Vertical");

        
    }

    //Used to grab keycard
    void GrabKeycard()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            Debug.Log("Grabbed Keycard");
            //need a way to differentiate which keycard the player is picking up
            //TODO: fix this, only works for key1
            hasKey1 = true;
        }
    }

    //movement
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movey * moveSpeed, rb.velocity.y);
       
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    //Collision triggers
    //so far, for keycard
    private void OnCollisionStay(Collision col)
    {
        destObj _col = col.collider.gameObject.GetComponent<destObj>();

        if (col.gameObject.CompareTag("Keycard"))
        {
            GrabKeycard();
            //destroy keycard 
            //TODO: (doesn't work yet, needs to be worded differently?)
            //DestroyObject();

        }
    }

    // checks for collisions with other game objects
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LevelEnd1")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        if (other.gameObject.tag == "LevelEnd2")
        {
            SceneManager.LoadScene("LevelThree");
        }
        if(other.gameObject.tag == "LevelEnd3")
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
