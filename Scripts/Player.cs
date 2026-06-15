using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler OnItemPicked;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public event Action OnPlayerMoved;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir = Vector3.zero;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private float deadzone = 0.1f;      // Deadzone of gamepad left stick 

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("ERROR : MULTIPLE PLAYER INSTANCE.");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameHandler.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        PlayerMovement();
        HandleInteraction();

    }

    #region Counter Interactions
    private void HandleInteraction() {

        float playerInteractDistance = 2f;

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        // Detect Interactable Counter
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, playerInteractDistance)) {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if(baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    #endregion


    #region Player Movement
    private void PlayerMovement() {

        Vector2 inputVector = gameInput.GetMovementVector();  // Get Input from GameInput

        float inputMagnitude = inputVector.magnitude;   // Magnitude for analog speed
        
        if (inputMagnitude < deadzone) {
            inputMagnitude = 0;
        } else {
            inputMagnitude = (inputMagnitude - deadzone) / (1f - deadzone);
        }


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * inputMagnitude * Time.deltaTime;

        // Check collision  
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove) {
            // if can not move 
            // Check movement in X direction

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            } else {
                // if can not move in X direction 
                // Check movement in Z direction

                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                } else {
                    // Can not move anywhere
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = inputMagnitude > 0f;

        if (isWalking) {
            OnPlayerMoved?.Invoke();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    #endregion


    public bool IsWalking() {
        return isWalking;
    }


    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnItemPicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}