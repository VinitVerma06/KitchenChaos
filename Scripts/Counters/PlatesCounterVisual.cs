using UnityEngine;
using System.Collections.Generic;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e) {
        
        float plateOffsetY = 0.1f;
        
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
