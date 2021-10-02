using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public GameObject turret;
    public Animator optionMenuAnim;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public TMP_InputField roomCode, usernameField;


    private float speed = 25;
    private bool isOptionMenuOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        SetVolumeOnStart();
        roomCode.characterLimit = 4;
        usernameField.characterLimit = 16;
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
        float decibleRange = Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("turrisVolume", decibleRange);
        PlayerPrefs.Save();
        audioMixer.SetFloat("turrisVolume", decibleRange);
        volumeText.text = "Volume: ";
    }


    private void SetVolumeOnStart()
    {
        float volume = .25f;
        if (PlayerPrefs.HasKey("turrisVolume"))
            volume = PlayerPrefs.GetFloat("turrisVolume");
        else
            PlayerPrefs.SetFloat("turrisVolume", volume);
    }

    public void CreateGameButton()
    {

        if (!string.IsNullOrWhiteSpace(usernameField.text))
        {
            PlayerPrefs.SetString("name", usernameField.text);
            PlayerPrefs.SetInt("shouldCreateGame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    public void JoinGameButton()
    {
        if (int.TryParse(roomCode.text, out int joinCode) && joinCode > 999)
        {
            PlayerPrefs.SetInt("joinCode", joinCode);
            PlayerPrefs.SetInt("shouldCreateGame", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
