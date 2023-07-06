using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // update gm state

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                HandleGameScene(true);
                GameOverGO.SetActive(false);
                break;

            case GameManagerState.Gameplay:
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                HandleGameScene(false);

                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();


                break;

            case GameManagerState.Gameover:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                GameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);

                break;

        }
    }
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        SetGameManagerState(GameManagerState.Gameplay);
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    private void HandleGameScene(bool setActive)
    {
        playButton.SetActive(setActive);
        quitButton.SetActive(setActive);
        GameTitleGO.SetActive(setActive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
