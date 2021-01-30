using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Menu,
    Intro,
    GameStart,
    CleAMoletteFound,
    LampFound,
    FinalPieceFound
}

public class GameStateMachine : GenericSingleton<GameStateMachine>
{
    [SerializeField]
    private GameStates currentState;
    public GameStates CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    private GameStates previousState;

    private bool canRepair = false;
    public bool CanRepair
    {
        get { return canRepair; }
    }

    private bool lampFound = false;
    public bool LampFound
    {
        get { return lampFound; }
    }

    // Start is called before the first frame update
    void Start()
    {
        previousState = currentState;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != previousState)
        {
            switch (currentState)
            {
                case GameStates.Menu:
                    OnMenuEnterState();
                    break;
                case GameStates.Intro:
                    OnIntroEnterState();
                    break;
                case GameStates.GameStart:
                    OnGameStartEnterState();
                    break;
                case GameStates.CleAMoletteFound:
                    OnCleAMoletteFoundEnterState();
                    break;
                case GameStates.LampFound:
                    OnLampFoundEnterState();
                    break;
                case GameStates.FinalPieceFound:
                    OnFinalPieceFoundEnterState();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMenuEnterState()
    {
        //TODO: load Menu Scene
    }

    private void OnIntroEnterState()
    {
        ResetGame();
        //TODO: load intro scene (or game scene and launch intro)
    }

    private void OnGameStartEnterState()
    {
        //TODO: load game 1st scene (or allow the player to move if same szcene as intro)
    }

    private void OnCleAMoletteFoundEnterState()
    {
        canRepair = true;
    }

    private void OnLampFoundEnterState()
    {
        lampFound = true;
    }

    private void OnFinalPieceFoundEnterState()
    {
        //TODO: launch ending scene
    }


    private void ResetGame()
    {
        canRepair = false;
        lampFound = false;
    }
}
