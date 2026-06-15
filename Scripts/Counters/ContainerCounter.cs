using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {   // Player is not holding a KitchenObject

            // Spawns KitchenObject at Player's Hold 
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            // Triggers open n' close animation of ContainerCounter when Player interacts
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);

        }

    }

    public override void InteractAlternate(Player player) {
        // Do Nothing
    }

}
