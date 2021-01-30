using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : GenericSingleton<MenuManager>
{
    [SerializeField]
    private GameObject menuPanel, controlPanel, creditPanel;

    private void Start()
    {
        menuPanel.SetActive(true);
        controlPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void OnPlayClick()
    {
        GameStateMachine.Instance.CurrentState = GameStates.Intro;
    }

    public void OnControlClick()
    {
        menuPanel.SetActive(false);
        controlPanel.SetActive(true);
        creditPanel.SetActive(false);
    }

    public void OnCreditClick()
    {
        menuPanel.SetActive(false);
        controlPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnReturnButton()
    {
        menuPanel.SetActive(true);
        controlPanel.SetActive(false);
        creditPanel.SetActive(false);
    }
}
