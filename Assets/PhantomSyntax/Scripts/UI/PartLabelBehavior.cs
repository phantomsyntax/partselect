using Assets.PhantomSyntax.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Assets.PhantomSyntax.Scripts.UI {
    public class PartLabelBehavior : MonoBehaviour {
        [Header("Part Label Settings")]
        [SerializeField] private GameObject lookAtPoint;
        [SerializeField] private GameObject partLabelPrefab;
        [SerializeField] private DictionaryValue partNameXref;
        [SerializeField] private Vector3 partLabelOffset = Vector3.zero;
        
        [Header("Line Settings")]
        [SerializeField] private LineRenderer partLabelLine;
        [SerializeField] private Vector3 partLabelLineOffset = new Vector3(0.0f, -1.0f, 0.0f);
        [SerializeField] private Vector2 partLabelLineWidth = new Vector2(0.02f, 0.02f);
        private TextMeshProUGUI partLabelPrefabText;

        private void Awake() {
            partLabelPrefabText = partLabelPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // Hide the child components
            partLabelPrefab.SetActive(false);
            partLabelLine.startWidth = 0;
            partLabelLine.endWidth = 0;
            
            // Null checks
            if (!partNameXref) {
                Debug.LogWarning("[PartLabelBehavior] - The Part Name Xref ScriptableObject is not attached!");
            }
        }
        
        private void Update() {
            // TODO: research a better way to have this face the camera
            transform.LookAt(lookAtPoint.transform.position);
        }

        public void UpdatePartLabel(GameObject newPart) {
            if (!partLabelPrefab.activeSelf) {
                partLabelPrefab.SetActive(true);
                partLabelLine.startWidth = partLabelLineWidth.x;
                partLabelLine.endWidth = partLabelLineWidth.y;
            }
            gameObject.transform.position = (newPart.transform.position + partLabelOffset);
            partLabelPrefabText.text = partNameXref.Value[newPart.name];
            
            partLabelLine.SetPosition(0, gameObject.transform.position + partLabelLineOffset);
            partLabelLine.SetPosition(1, newPart.transform.position);
        }
    }
}
