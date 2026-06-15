using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    private bool IsCutting;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {  // There is no KitchenObject on the Counter

            if (player.HasKitchenObject()) {    // Player is holding a KitchenObject

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {    // Player carring something that can be cut
                    // Set parent to CuttingCounter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    cuttingProgress = 0;
                    IsCutting = false;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                }

            } else {    // Player is not holding anything
                // Do nothing

            }

        } else {
            // There is a KitchenObject on the Counter
            if (player.HasKitchenObject()) {
                // Player is holding a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //  Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                    }
                }

            } else {
                // Player is not holding anything

                // Set parent to Player if cutting is not in progress
                if (!IsCutting) {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    OnProgressChanged?.Invoke(this, null);

                }

            }

        }

    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a KitchenObject here and can be cut
            cuttingProgress++;
            IsCutting = true;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);    

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {

                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();

                IsCutting = false;

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        } else {
            return null;
        }

    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}
