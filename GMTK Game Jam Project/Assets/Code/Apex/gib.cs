using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gib : MonoBehaviour
{
    [SerializeField] private GameObject bSquirt;
    // Start is called before the first frame update
    void Start()
    {
        GameObject effy = Instantiate(bSquirt, transform.position, Quaternion.identity);
        Destroy(effy, 5f);
    }
}
