using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenuEvents : MonoBehaviour
{

/* 
PAUSE MENU (INCLUDING SETTINGS MENU) - To work with the Unity UI Toolkit.

This script was written as a way to play around with the UI Toolkit and to see how efficient it is to create a menu that is used frequently. 
After having a play around with it, the flow of it proved to be really useful and feels just as good as the UI Canvas.

*/

    // UIDocument that will be used throughout.
    private UIDocument _pauseDocument;

    // Button variables that will be used to line up with the UIDocument.
    private Button _continueButton;
    private Button _settingsButton;
    private Button _mainMenuButton;
    private Button _closeGameButton;
    private Button _closeSettingsButton;

    // Toggles for the Audio. 
    private Toggle _musicToggle;
    private Toggle _sfxToggle;

    // Each VisualElement that is included in the UIDocument.
    private VisualElement _pauseBGElement;
    private VisualElement _settingsBGElement;

    // The list for the buttons that will be checked.
    private List<Button> _pauseMenuButtons = new List<Button>();

    // AudioSources that will be used in the Settings.
    public AudioSource _musicSource;
    public AudioSource _sfxSource;

    // Bools that will be checked on certain functions.
    private bool gameIsPaused;
    private bool settingsOpened;
    private bool musicToggled;
    private bool sfxToggled;

    //Awake function to trigger on launch.
    void Awake()
    {
        //Ensuring that the Bools are set to what's expected.
        gameIsPaused = false;
        settingsOpened = false;
        musicToggled = true;
        sfxToggled = true;

        //Getting the required components for this script.
        _pauseDocument = GetComponent<UIDocument>();
        _musicSource = GetComponent<AudioSource>();

        //Declaring what each button is within the UIDocument.
        _continueButton = _pauseDocument.rootVisualElement.Q("ContinueButton") as Button;
        _settingsButton = _pauseDocument.rootVisualElement.Q("SettingsButton") as Button;
        _mainMenuButton = _pauseDocument.rootVisualElement.Q("MainMenuButton") as Button;
        _closeGameButton = _pauseDocument.rootVisualElement.Q("CloseGameButton") as Button;
        _closeSettingsButton = _pauseDocument.rootVisualElement.Q("CloseSettingsButton") as Button;

        //Declaring each VisualElement that will be used.
        _pauseBGElement = _pauseDocument.rootVisualElement.Q("PauseBG");
        _settingsBGElement = _pauseDocument.rootVisualElement.Q("SettingsMenu");

        //Declaring the Toggles within the UIDocument.
        _musicToggle = _pauseDocument.rootVisualElement.Q("MusicToggle") as Toggle;
        _sfxToggle = _pauseDocument.rootVisualElement.Q("SFXToggle") as Toggle;

        //Setting up each button to what function it will use. 
        _continueButton.RegisterCallback<ClickEvent>(ResumeGame);
        _settingsButton.RegisterCallback<ClickEvent>(SettingsMenuOpenClose);
        _mainMenuButton.RegisterCallback<ClickEvent>(ReturnToMM);
        _closeGameButton.RegisterCallback<ClickEvent>(CloseGame);
        _closeSettingsButton.RegisterCallback<ClickEvent>(SettingsMenuOpenClose);

        //Setting up each Toggle to what function it will use.
        _musicToggle.RegisterCallback<ClickEvent>(ToggleMusic);
        _sfxToggle.RegisterCallback<ClickEvent>(ToggleSFX);

        //Listing the buttons that are within in the UIDocument
        _pauseMenuButtons = _pauseDocument.rootVisualElement.Query<Button>().ToList();

        // This counts the list of buttons and then calls the function that will trigger when any button is pressed within the UIDocument.
        for (int i = 0; i < _pauseMenuButtons.Count; i++)
        {
            _pauseMenuButtons[i].RegisterCallback<ClickEvent>(OnAllButtons);
        }

        _settingsBGElement.style.display = DisplayStyle.None;

    }

    void Update()
    {
        //Inputs to call the function to Pause or unpause the game.
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        //Input to close the Settings VisualElements.
        if (Input.GetMouseButtonDown((int)MouseButton.RightMouse))
        {
            if (_settingsBGElement.style.display == DisplayStyle.Flex)
            {
                _settingsBGElement.style.display = DisplayStyle.None;
                _pauseBGElement.style.display = DisplayStyle.Flex;
            }
        }

        //Checks if the toggle is true or false and then changes the volume to match. 
        if (musicToggled)
        {
            _musicToggle.value = true;
            _musicSource.volume = 1;

        }
        else if (!musicToggled)
        {
            _musicToggle.value = false;
            _musicSource.volume = 0;

        }

        //Checks if the toggle is true or false and then changes the volume to match. 
        if (sfxToggled)
        {
            _sfxToggle.value = true;
            _sfxSource.volume = 1;

        }
        else if (!sfxToggled)
        {
            _sfxToggle.value = false;
            _sfxSource.volume = 0;
        }

        //print(Time.deltaTime);
      
    }

    //Function to change the Tool bool for the music.
    void ToggleMusic(ClickEvent evt)
    {
        if (musicToggled)
        {
            musicToggled = false;
        }
        else if (!musicToggled)
        {
            musicToggled = true;
        }
    }

    //Function to change the Tool bool for the SFX.
    void ToggleSFX(ClickEvent evt)
    {
     if (sfxToggled)
        {
            sfxToggled = false;
        }
        else if (!sfxToggled)
        {
            sfxToggled = true;
        }
    }

    //Function to close the Pause VisualElement - This is used with the UIDocument button.
    void ResumeGame(ClickEvent evt)
    {
        _pauseBGElement.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    //Function to open and close the Puase VisualElement - This is used with the keyboard Input.
    void PauseResumeGame()
    {
        if (gameIsPaused)
        {
            _pauseBGElement.style.display = DisplayStyle.None;
            Time.timeScale = 1;
            gameIsPaused = false;
        }
        else if (!gameIsPaused)
        {
            _pauseBGElement.style.display = DisplayStyle.Flex;
            gameIsPaused = true;
            Time.timeScale = 0;
        }
    }

    //Function to open and close the Settings VisualElement - This is used with the UIDocument button.
    void SettingsMenuOpenClose(ClickEvent evt)
    {
        if (!settingsOpened)
        {
            _settingsBGElement.style.display = DisplayStyle.Flex;
            _pauseBGElement.style.display = DisplayStyle.None;
            settingsOpened = true;
        }
        else if (settingsOpened)
        {
            _settingsBGElement.style.display = DisplayStyle.None;
            _pauseBGElement.style.display = DisplayStyle.Flex;
            settingsOpened = false;
        }  
    }

    //Function to return to the Main Menu - This is used with the UIDocument button.
    void ReturnToMM(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Function to close the game - This is used with the UIDocument button.
    void CloseGame(ClickEvent evt)
    {
        Application.Quit();
    }

    //Function to play a sound when a button is pressed - This is used with the UIDocument button.
    void OnAllButtons(ClickEvent evt)
    {
        _musicSource.Play();
    }
}



