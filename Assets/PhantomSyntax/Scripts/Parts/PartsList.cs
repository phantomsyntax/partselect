using Assets.PhantomSyntax.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PhantomSyntax.Scripts.Parts {
    public class PartsList : MonoBehaviour {
        [Header("Vehicle Settings")]
        [SerializeField] private GameObject CurrentVehicle;

        [Header("Parts UI Settings")]
        [SerializeField] private GameObject partsContent;
        [SerializeField] private GameObject partButtonPrefab;
        [SerializeField] private GameObject preloader;
        [SerializeField] private DictionaryValue partNameXref;

        [Header("Description UI Settings")]
        [SerializeField] private GameObject descriptionPrefab;
        [SerializeField] private GameObject partLabelPrefab;
        
        [Header("Event/Observer Settings")]
        [SerializeField] private GameEvent handleChangeCurrentPart;
        [SerializeField] private GameEvent handleChangeRotatePoint;
        [SerializeField] private GameEvent handlePopulate;
        
        private void Awake() {
            descriptionPrefab.SetActive(false);
        }

        private void Start() {
            // Null checks
            if (!CurrentVehicle) {
                CurrentVehicle = GameObject.FindWithTag("Vehicle");
            }

            if (!preloader) {
                Debug.LogWarning("[PartsList] - The Preloader GameObject is not attached!");
            }

            if (partNameXref == null) {
                Debug.LogWarning("[PartsList] - The Part Name Xref ScriptableObject is not attached!");
            }
            
            if (handleChangeCurrentPart == null) {
                Debug.LogWarning("[PartsList] - The Handle Change Current Part ScriptableObject is not attached!");
            }
            
            if (handleChangeRotatePoint == null) {
                Debug.LogWarning("[PartsList] - The Handle Change Rotate Point ScriptableObject is not attached!");
            }

            if (handlePopulate == null) {
                Debug.LogWarning("[PartsList] - The Handle Populate ScriptableObject is not attached!");
            }
            
            CurrentVehicle.SetActive(true);
            Populate();
        }

        public void Populate() {
            
            for (int childIndex = 0; childIndex < CurrentVehicle.transform.childCount; childIndex++) {
                // Instantiate and label button for each part listed in parent vehicle
                GameObject newPart = CurrentVehicle.transform.GetChild(childIndex).gameObject;
                GameObject newButton = Instantiate(partButtonPrefab);
                newButton.transform.SetParent(partsContent.transform);
                // Grab the cross reference Name from the Part Label
                newButton.GetComponentInChildren<Text>().text = PartToName(newPart.name);
                newButton.GetComponent<Button>().onClick.AddListener(() => ChangeCurrentPart(newPart));
                
                // Fire off event to handlePopulate listeners
                handlePopulate.ConditionalGameObject(newPart);
            }
        }
        
        public string PartToName(string partLabel) {
            return partNameXref.Value[partLabel];
        }

        public void ChangeCurrentPart(GameObject newPart) {
            // Changes the PartDescription name
            // This relies on the first child object being the text header in the prefab
            descriptionPrefab.transform.GetChild(0).GetComponent<Text>().text = PartToName(newPart.name);
            // Enable the Part Description canvas
            if (!descriptionPrefab.activeSelf) {
                descriptionPrefab.SetActive(true);
            }
            
            // Fire off event to handleChangeCurrentPoint listeners
            handleChangeCurrentPart.ConditionalGameObject(newPart);
        }

        public void ChangeRotatePoint(GameObject newPart) {
            // Disable the Part Description canvas
            if (descriptionPrefab.activeSelf) {
                descriptionPrefab.SetActive(false);
            }
            // Disable the Part Label canvas child
            if (partLabelPrefab.activeSelf) {
                partLabelPrefab.SetActive(false);
            }
            
            // Fire off event to handleChangeCurrentPoint listeners
            handleChangeRotatePoint.ConditionalGameObject(newPart);
        }

        #region Test Functions
        public void ClearAll() {
            // TODO: placeholder to test easily adding new vehicles
            // Disable the Part Label canvas child
            if (partLabelPrefab.activeSelf) {
                partLabelPrefab.SetActive(false);
            }
            // Clear out the button prefabs from the Parts List
            foreach (Transform childObject in partsContent.transform) {
                Destroy(childObject.gameObject);
            }
            // Deactivates the current Active vehicle from the scene
            GameObject currentCar = GameObject.FindWithTag("Vehicle");
            currentCar.SetActive(false);
        }

        public void ChangeCurrentVehicle(GameObject newCar) {
            // TODO: placeholder to test easily adding new vehicles
            newCar.SetActive(true);
            CurrentVehicle = newCar;
        }
        #endregion
    }
}