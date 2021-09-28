using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{

    public GameObject turret;
    private float speed = 25;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turret.transform.Rotate(0, Time.deltaTime * speed, 0);
    }
}
