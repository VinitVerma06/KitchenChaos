using System;
using UnityEngine;

public class StoveCounter : BaseCounter {

    public static event Action OnItemPlaced;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }



    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;
    private bool IsCooking;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,

    }

    private State state;

    private void Start() {

        IsCooking = false;
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch(state) {
                
                case State.Idle:
                break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.FryingTimerMax
                    });
                    
                    if (fryingTimer > fryingRecipeSO.FryingTimerMax) {
                        // Fried
                        GetKitchenObject().DestorySelf();
                        
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        IsCooking = false;

                        state = State.Fried;
                        burningTimer = 0f;
                        
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                            progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                        });

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });
                    }
                break;
            
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        // Fried
                        GetKitchenObject().DestorySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;

                        OnProgressChanged?.Invoke(this, null);

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });
                    }
                break;
            
                case State.Burned:
                break;
            }
            
        }

    }

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {    // There is no KitchenObject on the Counter
        
            if (player.HasKitchenObject()) {    // Player is holding a KitchenObject
            
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {    // Player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    OnItemPlaced?.Invoke();

                    IsCooking = true;

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    // Set state to Frying and reset timer to zero
                    state = State.Frying;
                    fryingTimer = 0f;
                    
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.FryingTimerMax
                    });

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                }

            } else {    // Player is not holding anything
                // Do nothing
            }

        } else {
            // There is a KitchenObject on the Counter
            if (player.HasKitchenObject()) {    // Player is holding a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //  Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            } else {
                // Player is not holding anything
                // KitchenObject parent is set to Player if not cooking
                if(!IsCooking) {
                    GetKitchenObject().SetKitchenObjectParent(player);
                
                    // After Player picks up the object set state to Idle  
                    state = State.Idle;

                    OnProgressChanged?.Invoke(this, null);

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = state
                    });
                }
            }
        }
    }

    public override void InteractAlternate(Player player) {
        // Do Nothing
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }

    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
