using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;



    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipeDelivered;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        UpdateOrderList();
    }

    private void UpdateOrderList() {
        //  Check if the Recipe list is empty or not 
        if (GameHandler.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax) {
            spawnRecipeTimer += Time.deltaTime;     // Start the spawn timer 

            if (spawnRecipeTimer >= spawnRecipeTimerMax) {
                spawnRecipeTimer = 0f;      // Reset spawn timer

                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            // Check if the recipe has the same number of ingredients as on the plate
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                bool plateContentsMatchesRecipe = true;

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {    // Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {     // Cycling through all ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }

                }

                if (plateContentsMatchesRecipe) {
                    // Player delivered the correct recipe!
                    successfulRecipeDelivered++;

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No match found!
        // Player did not delivered the correct recipe!
        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipeDelivered() {
        return successfulRecipeDelivered;
    }

}
