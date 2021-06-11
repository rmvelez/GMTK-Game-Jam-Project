using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycard : MonoBehaviour
{
    [SerializeField] private int keyID;

    private int blue;
    private int green;
    private int red;
    private int yellow;

    // Each keycard has an interchangable number or color that can be used to identify it
    //Unity hates me tho and I'm sure there's a better way to do this

    void Start()
    {
        /*
        blue=keyID 1;
        green= keyID 2;
        red= keyID 3;
        yellow= keyID 4;
        */
    }

}
