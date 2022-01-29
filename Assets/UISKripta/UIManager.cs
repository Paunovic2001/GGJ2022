using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Animations")]
    public Animator HTPanim;
    public Animator optionsAnim;
    public Animator creditsAnim;

    public AudioSource[] glazbaZaCjelokupnuIgru;
    public AudioSource[] soundEfektiUglazbi;
    public Slider slid;
    public GameObject pausePanel;

    [Header("paneli")]
    public GameObject menuPanel;
    public GameObject imageObjekt;

    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;
    public GameObject[] gameObjekti;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private int qualityLevel;
    private float brightnessLevel;
    bool isFullcreen;
    
    
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {

        PausePanel();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PausePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            PauseGame();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("izgubio si.");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public  void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }
   
   public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void ChangeSliderValueOne()
    {
        for (int i = 0; i < glazbaZaCjelokupnuIgru.Length; i++)
        {
            glazbaZaCjelokupnuIgru[i].volume = slid.value;
        }
    }

    public void ChangeSliderValueTwo()
    {
        for (int i = 0; i < soundEfektiUglazbi.Length; i++)
        {
            soundEfektiUglazbi[i].volume = slid.value;
        }
    }


   

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void StartGame()
    {
        for(int i = 0; i < gameObjekti.Length; i++)
        {
            gameObjekti[i].SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void HTPanimOnPress()
    {
        HTPanim.enabled = true;
        HTPanim.Play("HTPanim", 0, 0);
        imageObjekt.SetActive(false);
        
    }

    

    public void OptionsAnim()
    {
        optionsAnim.enabled = true;
        optionsAnim.Play("OptionsAnim", 0, 0);
    }

    public void CreditsAnim()
    {
        creditsAnim.Play("CreditsAnim", 0, 0);
        creditsAnim.enabled = true;
    }

    public void HTPbackAnim()
    {
        HTPanim.Play("HTPbackAnim", 0, 0);
    }

    public void OptionBckAnim()
    {
        optionsAnim.Play("OptionBackAnim", 0, 0);
    }

    public void CreditsBackAnim()
    {
        creditsAnim.Play("CreditsBackAnim", 0, 0);
    }
}
