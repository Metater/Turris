using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Builder : MonoBehaviour
{
    private bool building = false;
    public GameObject gun;
    public GameObject buildUI;
    public Tilemap tilemap;

    private GameObject selectedTower;
    public GameObject baseTower;
    public GameObject cannonTower;
    public GameObject missileTower;
    public GameObject laserTower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (building)
            {
                building = false;
                gun.SetActive(true);
                buildUI.SetActive(false);
            }
            else
            {
                building = true;
                gun.SetActive(false);
                buildUI.SetActive(true);
            }
        }

        if (building && Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                int hitX = (int) hit.point.x;
                int hitY = (int) (hit.point.y + 0.5);
                int hitZ = (int) hit.point.z;
                Vector3 hitVector = new Vector3(hitX, hitY, hitZ);
                Debug.Log("(" + hitX + ", " + hitY + ", " + hitZ + ")");

                Instantiate(baseTower,hitVector, Quaternion.Euler(270,0,0));


            }

        }

    }



}
