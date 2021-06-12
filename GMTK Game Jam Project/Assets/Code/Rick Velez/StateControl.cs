using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControl : MonoBehaviour
{
    public enum GameState {TitleState, LearnState, CreditState};
    public GameState currentGameState;

    public GameObject title;
    public GameObject learn;
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.TitleState;
        ShowScreen(title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this function loads the instructions when players click on the instructions button on the title screen
    public void HowToPlay()
    {
        currentGameState = GameState.LearnState;
        ShowScreen(learn);
    }

    // this function loads the credits when players click on the credits button on the title screen
    public void EndCredits()
    {
        currentGameState = GameState.CreditState;
        ShowScreen(credit);
    }

    // this function goes back to the title screen when players click on the back button in either the instructions or credits screen
    public void BackToTitle()
    {
        currentGameState = GameState.TitleState;
        ShowScreen(title);
    }

    // this function is used to determine which screen to show on the main menu
    private void ShowScreen(GameObject gameObjectToShow)
    {
        title.SetActive(false);
        learn.SetActive(false);
        credit.SetActive(false);

        gameObjectToShow.SetActive(true);
    }
}
