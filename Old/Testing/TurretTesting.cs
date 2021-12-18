using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTesting : MonoBehaviour
{
    private bool isTesting = false;

    public GameObject testingText;
    public GameObject cannonSpawn, missileSpawn, laserSpawn;

    public GameObject cannonPrefab, missilePrefab, laserPrefab;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && isTesting)
        {
            isTesting = false;
            testingText.SetActive(false);



        } 
        else
        {
            isTesting = true;
            testingText.SetActive(true);
        }

        if (isTesting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                shootCannon();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                shootMissile();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                shootLaser();
        }


    }

    private void shootCannon()
    {
        Vector3 spawnLocation = cannonSpawn.transform.position;
        Instantiate(cannonPrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
    }

    private void shootMissile()
    {
        Vector3 spawnLocation = missileSpawn.transform.position;
        Instantiate(missilePrefab, spawnLocation, Quaternion.Euler(0, 0, -90));
    }

    private void shootLaser()
    {
        Vector3 spawnLocation = laserSpawn.transform.position;
        Instantiate(laserPrefab, spawnLocation, Quaternion.Euler(0, 90, 0));
    }


}
