using UnityEngine;
using UnityEngine.Events;

namespace Assets.PhantomSyntax.ScriptableObjects {
    public class GameEventListener : MonoBehaviour {
        [Header("GameEvent Observer Settings")]
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent unityEvent;
        [SerializeField] private UnityEvent<bool> boolEvent;
        [SerializeField] private UnityEvent<GameObject> gameObjectEvent;
        
        private void OnEnable() {
            if (gameEvent != null) {
                gameEvent.Register(this);
            }
        }

        public void EventTriggered() {
            unityEvent.Invoke();
        }

        public void EventBool(bool value) {
            boolEvent.Invoke(value);
        }

        public void EventGameObject(GameObject value) {
            gameObjectEvent.Invoke(value);
        }
        
        private void OnDisable() {
            if (gameEvent != null) {
                gameEvent.Unregister(this);
            }
        }
    }
}