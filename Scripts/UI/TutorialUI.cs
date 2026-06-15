using System;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    public static event Action OnMovementTutorialRequested;
    public static event Action OnStartDayTutorialRequested;
    public static event Action OnPickupTutorialRequested;
    public static event Action OnCuttingTutorialRequested;
    public static event Action OnFryingTutorialRequested;
    public static event Action OnDeliverTutorialRequested;
    public static event Action OnEndDayTutorialRequested;



    public static event Action OnPlayerMoved;
    public static event Action OnPlayerAltInteracted;
    public static event Action OnPlayerPickedUp;
    public static event Action OnPlayerCut;
    public static event Action OnPlayerCooked;
    public static event Action OnPlayerDelivered;
    public static event Action OnDayEnded;

    private enum TutorialState {
        Move,
        BeginDay,
        PickUp,
        Cutting,
        Frying,
        Deliver,
        EndDay,
        Complete
    }

    private TutorialState state;
    private float showHintTimer = 3f;
    private float currentTimer;

    private bool hasRequested = false;
    private bool HasPlayerMoved = false;
    private bool HasPlayerAltInteracted = false;
    private bool HasPlayerPickedUp = false;
    private bool HasPlayerCut = false;
    private bool HasPlayerCooked = false;
    private bool HasPlayerDelivered = false;
    private bool HasDayEnded = false;

    private void Start() {
        state = TutorialState.Move;
        currentTimer = 0f;

        Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;
        BellCounter.Instance.OnBellInteract += BellCounter_OnBellInteract;
        Player.Instance.OnItemPicked += Player_OnItemPicked;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        StoveCounter.OnItemPlaced += StoveCounter_OnItemPlaced;
        DeliveryCounter.OnItemDelivered += DeliveryCounter_OnItemDelivered;
        GameHandler.OnDayEnded += GameHandler_OnDayEnded;
    }

    private void GameHandler_OnDayEnded() {
        HasDayEnded = true;
        GameHandler.OnDayEnded -= GameHandler_OnDayEnded;
    }

    private void DeliveryCounter_OnItemDelivered() {
        HasPlayerDelivered = true;
        DeliveryCounter.OnItemDelivered -= DeliveryCounter_OnItemDelivered;
    }

    private void StoveCounter_OnItemPlaced() {
        HasPlayerCooked = true;
        StoveCounter.OnItemPlaced -= StoveCounter_OnItemPlaced;
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        HasPlayerCut = true;
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
    }

    private void Player_OnItemPicked(object sender, EventArgs e) {
        HasPlayerPickedUp = true;
        Player.Instance.OnItemPicked -= Player_OnItemPicked;
    }

    private void BellCounter_OnBellInteract(object sender, EventArgs e) {
        HasPlayerAltInteracted = true;
        BellCounter.Instance.OnBellInteract -= BellCounter_OnBellInteract;
    }

    private void Player_OnPlayerMoved() {
        HasPlayerMoved = true;
        Player.Instance.OnPlayerMoved -= Player_OnPlayerMoved;
    }

    private void Update() {
        ShowTutorial();
    }

    private void ShowTutorial() {

        switch (state) {

            default:

            case TutorialState.Move:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestMovementTutorial();
                        Debug.Log("Movement Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerMoved) {
                    PlayerMoved();
                    currentTimer = 0f;
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.BeginDay:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestStartDayTutorial();
                        Debug.Log("Begin Day Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerAltInteracted) {
                    PlayerAltInteracted();
                    currentTimer = 0f;
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.PickUp:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestPickupTutorial();
                        Debug.Log("Pickup Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerPickedUp) {
                    PlayerPickedUp();
                    currentTimer = 0f;
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.Cutting:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestCuttingTutorial();
                        Debug.Log("Cutting Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerCut) {
                    PlayerCut();
                    currentTimer = 0f;
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.Frying:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestFryingTutorial();
                        Debug.Log("Frying Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerCooked) {
                    PlayerCooked();
                    currentTimer = 0f;
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.Deliver:
                currentTimer += Time.deltaTime;
                if (currentTimer >= showHintTimer) {
                    if (!hasRequested) {
                        RequestDeliverTutorial();
                        Debug.Log("Delivery Tutorial Requested");
                        hasRequested = true;
                    }
                }
                if (HasPlayerDelivered) {
                    PlayerDelivered();
                    currentTimer = 0f;
                }
                if (GameHandler.Instance.IsWaitingToEndDay()) {
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.EndDay:
                if (!hasRequested) {
                    RequestEndDayTutorial();
                    Debug.Log("End Day Tutorial Requested");
                    hasRequested = true;
                }
                if (HasDayEnded) {
                    DayEnded();
                    AdvanceToNextTutorial();
                }
                break;

            case TutorialState.Complete:
                break;
        }
    }

    private void AdvanceToNextTutorial() {
        state++;
        hasRequested = false;
        if (state != TutorialState.Complete) {
            ShowTutorial();
        } else {
            Debug.Log("Tutorial completed!");
        }
    }

    private void RequestMovementTutorial() => OnMovementTutorialRequested?.Invoke();
    private void RequestStartDayTutorial() => OnStartDayTutorialRequested?.Invoke();
    private void RequestPickupTutorial() => OnPickupTutorialRequested?.Invoke();
    private void RequestCuttingTutorial() => OnCuttingTutorialRequested?.Invoke();
    private void RequestFryingTutorial() => OnFryingTutorialRequested?.Invoke();
    private void RequestDeliverTutorial() => OnDeliverTutorialRequested?.Invoke();
    private void RequestEndDayTutorial() => OnEndDayTutorialRequested?.Invoke();
    
    
    public static void PlayerMoved() => OnPlayerMoved?.Invoke();
    public static void PlayerAltInteracted() => OnPlayerAltInteracted?.Invoke();
    public static void PlayerPickedUp() => OnPlayerPickedUp?.Invoke();
    public static void PlayerCut() => OnPlayerCut?.Invoke();
    public static void PlayerCooked() => OnPlayerCooked?.Invoke();
    public static void PlayerDelivered() => OnPlayerDelivered?.Invoke();
    public static void DayEnded() => OnDayEnded?.Invoke();

}
