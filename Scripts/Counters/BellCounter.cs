using System;
using UnityEngine;

public class BellCounter : BaseCounter {

    public static BellCounter Instance { get; private set; }

    public event EventHandler OnBellInteract;

    private void Awake() {
        Instance = this;
    }

    public override void Interact(Player player) {
        // Do Nothing
    }

    public override void InteractAlternate(Player player) {
        OnBellInteract?.Invoke(this, EventArgs.Empty);
    }
}
