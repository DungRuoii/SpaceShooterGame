using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOver;
    public GameObject scoreUIText;
    public GameObject TimeCounter;
    public GameObject GameTitle;
    public enum GameControllerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameControllerState GCState;
    // Start is called before the first frame update
    void Start()
    {
        GCState = GameControllerState.Opening;
    }

    void UpdateGameControllerState()
    {
        switch (GCState)
        {
            case GameControllerState.Opening:
                GameOver.SetActive(false);
                playButton.SetActive(true);
                GameTitle.SetActive(true);
                break;

            case GameControllerState.Gameplay:
                GameTitle.SetActive(false);
                scoreUIText.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                playerShip.GetComponent<PlayerController>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                TimeCounter.GetComponent<TimeCounter>().StartTimeCounter();
                break;

            case GameControllerState.GameOver:
                TimeCounter.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                GameOver.SetActive(true);
                Invoke("ChangeToOpeningState", 5f);
                break;
        }
    }

    public void SetGameControllerState(GameControllerState state)
    {
        GCState = state;
        UpdateGameControllerState();
    }

    public void StartGamePlay()
    {
        GCState = GameControllerState.Gameplay;
        UpdateGameControllerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameControllerState(GameControllerState.Opening);
    }
}
