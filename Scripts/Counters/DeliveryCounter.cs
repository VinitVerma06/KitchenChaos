using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    public static event Action OnItemDelivered;

    public void Awake() {
        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {     // Only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                OnItemDelivered?.Invoke();
                player.GetKitchenObject().DestorySelf();
            }
        }
    }

    public override void InteractAlternate(Player player) {
        //  Do nothing
    }
}
