using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ClearCounter : BaseCounter {

    //[SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player) {
        // Place kitchenObject on ClearCounter
        
        if (!HasKitchenObject()) {  // There is no KitchenObject 
            
            if (player.HasKitchenObject()) {    // Player is holding a KitchenObject
                
                // Set parent to ClearCounter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            
            } else {    // Player is not holding anything
                // Do nothing

            }

        } else {
            // Player is holding something
            if (player.HasKitchenObject()) {    
                // Player is holding a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //  Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                    }
                } else {    // Player is not holding a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter has a plate 
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestorySelf();
                        }
                    }
                }
            } else {    // Player is not holding anything

                // Set parent to Player
                GetKitchenObject().SetKitchenObjectParent(player);

            }

        }

    }

    public override void InteractAlternate(Player player) {
        // Do Nothing
    }

}
