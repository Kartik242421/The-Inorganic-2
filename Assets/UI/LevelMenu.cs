using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private void Start()
    {
        LevelUI.SetActive(false);
        MainUI.SetActive(true);
        CharacterSelection.SetActive(false);
        OptionUI.SetActive(false);


    }
    [SerializeField] GameObject LevelUI, MainUI,CharacterSelection,OptionUI;

    public void OpenLevel(int level)
    {
        // Load the scene by index
        SceneManager.LoadScene(level);

        // Disable the LevelUI and enable the MainUI

        LevelUI.SetActive(false);
        CharacterSelection.SetActive(false);
        OptionUI.SetActive(false);


        MainUI.SetActive(true);
    }
    public void OpenLevelSelectionMenu()
    {
        LevelUI.SetActive(true);
        MainUI.SetActive(false);
        CharacterSelection.SetActive(false);
        OptionUI.SetActive(false);

    }
    public void OpenCharacterSelectionMenu()
    {
        LevelUI.SetActive(false);
        MainUI.SetActive(false);
        CharacterSelection.SetActive(true);
        OptionUI.SetActive(false);


    }

    public void OpenOptionMenu()
    {
        OptionUI.SetActive(true);

        LevelUI.SetActive(false);
        MainUI.SetActive(false);
        CharacterSelection.SetActive(false);

    }
    public void QuitButton()
    {
        Application.Quit();
    }



}
