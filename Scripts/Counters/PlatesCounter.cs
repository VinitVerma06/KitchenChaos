using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnTimer = 4f;
    private int plateAmount;
    private int plateAmountMax = 4;

    private void Update() {
        Spawnplate();
    }

    // Spawn Plates
    private void Spawnplate() {

        if (plateAmount < plateAmountMax) {
            spawnPlateTimer += Time.deltaTime;

            if (GameHandler.Instance.IsGamePlaying() && spawnPlateTimer >= spawnTimer) {
                spawnPlateTimer = 0f;
                plateAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {   // Player is not holding anything
            if (plateAmount > 0) {    // There's at least one plate at the counter
                plateAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        //  Do nothing
    }
}
