using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{

    public GameObject turret;
    public Animator optionMenuAnim;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private float speed = 25;
    private bool isOptionMenuOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        SetVolumeOnStart();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turret.transform.Rotate(0, Time.deltaTime * speed, 0);
    }

    public void openOptions()
    {
        if (isOptionMenuOpen)
        {
            optionMenuAnim.SetTrigger("close");
            isOptionMenuOpen = false;
        }
        else
        {
            optionMenuAnim.SetTrigger("open");
            isOptionMenuOpen = true;
        }
        
    }

    public void closeOptions()
    {
        optionMenuAnim.SetTrigger("close");
        isOptionMenuOpen = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void volumeControl(float volume)
    {
        PlayerPrefs.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.Save();
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }


    private void SetVolumeOnStart()
    {
        float volume = .25f;
        if (PlayerPrefs.HasKey("volume"))
            volume = PlayerPrefs.GetFloat("volume");
        else
            PlayerPrefs.SetFloat("volume",volume);
        volumeSlider.value = volume;
        volumeControl(volume);
    }
}
