using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public TMP_Text healthAmount;
    public Image healthBar;

    private GameObject selectedObject = null;

    public GameObject baseTower;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;

    public GameObject baseIcon;
    public GameObject tower1Icon;
    public GameObject tower2Icon;
    public GameObject tower3Icon;
    public GameObject selection;




    private void Start()
    {
        currentHealth = maxHealth;
    }



    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3((currentHealth / maxHealth),1,1);
        healthAmount.text = "Health: " + currentHealth;
        select();
    }



    void select()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (selectedObject == null)
            {
                selection.SetActive(true);
                selectedObject = baseTower;
                selection.transform.position = baseIcon.transform.position;
            }
            else
            {
                selectedObject = null;
                selection.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (selectedObject == null)
            {
                selection.SetActive(true);
                selectedObject = tower1;
                selection.transform.position = tower1Icon.transform.position;
            }
            else
            {
                selectedObject = null;
                selection.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (selectedObject == null)
            {
                selection.SetActive(true);
                selectedObject = tower2;
                selection.transform.position = tower2Icon.transform.position;
            }
            else
            {
                selectedObject = null;
                selection.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (selectedObject == null)
            {
                selection.SetActive(true);
                selectedObject = tower3;
                selection.transform.position = tower3Icon.transform.position;
            }
            else
            {
                selectedObject = null;
                selection.SetActive(false);
            }
        }
    }


}
