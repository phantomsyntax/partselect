using UnityEngine;

namespace Assets.PhantomSyntax.Scripts.Camera {
    public class CameraManager : MonoBehaviour {
        [Header("Camera Settings")]
        [SerializeField] private float cameraRotateSpeed = 1.0f;
        [Range(-0.6f, 0.0f)]
        [SerializeField] private float cameraRotateHeight = -0.2f;

        [Header("Default Camera LookAt Points")]
        [SerializeField] private GameObject rightPoint;
        [SerializeField] private GameObject leftPoint;
        public GameObject CurrentPoint { get; private set; }

        private void Start() {
            // Null checks
            if (!rightPoint) {
                Debug.LogWarning("[CameraManager] - the rightPoint GameObject is not attached!");
            }
            
            // Starts the CameraPivot location looking at the rightPoint;
            CurrentPoint = rightPoint;
        }

        private void Update() {
            // TODO: check if already in position
            RotateCameraToCurrentPoint();
        }

        public void UpdateCurrentPoint(GameObject newPart) {
            CurrentPoint = newPart;
        }

        public void RotateCameraToCurrentPoint() {
            Vector3 currentPointVector = CurrentPoint.transform.position - transform.position;

            // To avoid passing zero vector to rotation
            if (currentPointVector != Vector3.zero) {
                //Quaternion newPointToLocate = Quaternion.LookRotation(currentPointVector);
                // Redone to clamp rotation so it won't move directly above the vehicle
                Quaternion newPointToLocate = Quaternion.LookRotation(currentPointVector);
                Quaternion newTransformRotation = new Quaternion(Mathf.Clamp(newPointToLocate.x, cameraRotateHeight, 0.0f), newPointToLocate.y, newPointToLocate.z, newPointToLocate.w);
                transform.rotation = Quaternion.Slerp(transform.rotation, newTransformRotation, (cameraRotateSpeed * Time.deltaTime));
            }
        }
    }
}