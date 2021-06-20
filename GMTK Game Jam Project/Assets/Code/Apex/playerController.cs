using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public bool isGameOver;
    public bool isSporeForm;

    private spore _spore;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] public float moveSpeed=10;
    private Vector2 movement;
    public float movey;

    [SerializeField] private bool hasKey1;
    [SerializeField] private bool hasKey2;
    [SerializeField] private bool hasKey3;
    [SerializeField] private bool hasKey4;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isSporeForm = false;
        hasKey1 = false; hasKey2 = false; hasKey3 = false; hasKey4 = false;
        _spore = GameObject.Find("SporeBarFill").GetComponent<spore>();
    }

    // Update is called once per frame
    void Update()
    {
        movey = movement.x = Input.GetAxisRaw("Horizontal");
        movey = movement.y = Input.GetAxisRaw("Vertical");

        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }

    
    //movement
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movey * moveSpeed, rb.velocity.y);
       
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * _spore.current *Time.fixedDeltaTime);

        //if (Input.GetKeyDown(KeyCode.Space)) { _spore.GetMore(10); }
    }

    //Collision triggers
    //so far, for keycard
    private void OnCollisionStay(Collision col)
    {
        destObj _col = col.collider.gameObject.GetComponent<destObj>();

        if (col.gameObject.tag == "Keycard1") { hasKey1 = true;}
        if (col.gameObject.CompareTag("Keycard2")) { hasKey2 = true; }
        if (col.gameObject.CompareTag("Keycard3")) { hasKey3 = true; }
        if (col.gameObject.CompareTag("Keycard4")) { hasKey4 = true; }
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
