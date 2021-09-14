using UnityEngine;

namespace Assets.PhantomSyntax.Scripts.Vehicle {
    public class VehicleComponents : MonoBehaviour
    {
        [Header("Vehicle Settings")]
        [SerializeField] private Material highlightMaterial;
        private Material currentMaterial;

        // Add a collider to each of the child objects to use a raycast for highlighting
        public void AddPartColliders(GameObject newPart) {
            newPart.AddComponent<MeshCollider>();
            newPart.AddComponent<PointerBehavior>();
        }

        // TODO: doing the highlight swap like this could potentially introduce bugs/orphaned materials
        public void ChangeHighlightMaterial(GameObject newPart) {
            MeshRenderer partMeshRenderer = newPart.GetComponent<MeshRenderer>();
            currentMaterial = partMeshRenderer.material;
            partMeshRenderer.material = highlightMaterial;
        }

        public void ChangeOriginalMaterial(GameObject newPart) {
            MeshRenderer partMeshRenderer = newPart.GetComponent<MeshRenderer>();
            partMeshRenderer.material = currentMaterial;
        }
    }
}