using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour {

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private GameObject waitingForOrderText;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;

        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }

        waitingForOrderText.SetActive(IsWaitingOrderListEmpty());       // Shows waitingForOrderText if order list is empty
    } 

    private bool IsWaitingOrderListEmpty() {
        return DeliveryManager.Instance.GetWaitingRecipeSOList().Count == 0;
    }
}
