using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spore : MonoBehaviour
{

    public Image mask;
    public const int SPORE_MAX = 100;
    public const int startAmt=20;
    public float current;
    public float sporeDeclineAmt;
    public float spore2eclineAmt2;
    private playerController _player;
    void Awake()
    {
       _player = GameObject.Find("Player").GetComponent<playerController>();
        current = startAmt;
    }
    private void Update()
    {
            
        GetCurrentFill();
        if (current < 0){ current = 0; }
        if (current > 101) { current = 100; }

        if (_player.isSporeForm == false)
        {
            //make bar tick slowly down
            if (current >=0){current -= sporeDeclineAmt * Time.deltaTime;}
        }
        else if (_player.isSporeForm == true)
        {
            //make bar tick slowly down
            if (current >= 0){current -= spore2eclineAmt2 * Time.deltaTime;}
        }
        //if (Input.GetKeyDown(KeyCode.Space)) { GetMore(10); }
    }

    private void GetCurrentFill()
    {
        
        float fillAmount = (float)current / (float)SPORE_MAX;
        mask.fillAmount = fillAmount;
       // _player.moveSpeed = (1 + current) / 2;
    }
    public void GetMore(int more) 
    {
        current += more;
    }
}
