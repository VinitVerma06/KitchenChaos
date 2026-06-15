using System;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance {  get; private set; }

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public static event Action OnDayEnded;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        WaitingToEndDay,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 240f;   // Each level of 4 minutes
    private bool isGamePaused = false;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        BellCounter.Instance.OnBellInteract += BellCounter_OnBellInteract;
    }

    private void BellCounter_OnBellInteract(object sender, EventArgs e) {
        // When bell pressed for first time to start the countdown
        if (state == State.WaitingToStart) {
            state = State.CountdownToStart;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        // When bell pressed for second time to end the day 
        if (state == State.WaitingToEndDay) {
            state = State.GameOver;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.WaitingToEndDay;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.WaitingToEndDay:
                break;

            case State.GameOver:
                OnDayEnded?.Invoke();

                int currentLevel = LevelManager.GetCurrentLevelNumber();
                LevelManager.CompleteLevel(currentLevel);
                break;

        }
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public float GetPlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsWaitingToEndDay() {
        return state == State.WaitingToEndDay;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        } else {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }
    }

}