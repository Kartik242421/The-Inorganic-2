using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Button ResumeBtn;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button MainMenuBtn;
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        ResumeBtn.onClick.AddListener(ResumeGame);
        RestartBtn.onClick.AddListener(RestartLevel);
        MainMenuBtn.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        Debug.Log("Back To Main Menu");
    }

    private void RestartLevel()
    {
        Debug.Log("Restart Level");

    }

    private void ResumeGame()
    {
       // uiManager.SwitchToGameplayUI();
    }
}
