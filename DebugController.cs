using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{

    public GameObject Debugger;
    private bool debugActive = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (debugActive)
            {
                Debugger.SetActive(false);
                debugActive = false;
            }
            else
            {
                Debugger.SetActive(true);
                debugActive = true;
            }
        }
    }
}
