using Assets.PhantomSyntax.Scripts.Parts;
using UnityEngine;

namespace Assets.PhantomSyntax.Scripts.Vehicle {
    public class PointerBehavior : MonoBehaviour
    {
        private GameObject partsManager;
        private GameObject vehicleManager;
        private VehicleComponents vehicleComponents;
        private PartsList partsList;

        private void Start() {
            // Null checks
            if (!vehicleManager) {
                vehicleManager = GameObject.FindWithTag("VehicleManager");
            }

            if (!partsManager) {
                partsManager = GameObject.FindWithTag("PartsManager");
            }
            
            vehicleComponents = vehicleManager.GetComponent<VehicleComponents>();
            partsList = partsManager.GetComponent<PartsList>();
        }

        private void OnMouseEnter() {
            vehicleComponents.ChangeHighlightMaterial(gameObject);
        }

        private void OnMouseExit() {
            vehicleComponents.ChangeOriginalMaterial(gameObject);
        }

        private void OnMouseDown() {
            partsList.ChangeCurrentPart(gameObject);
        }
    }
}
