using System;
using Assets.PhantomSyntax.ScriptableObjects;
using UnityEngine;

namespace Assets.PhantomSyntax.Scripts.Utility {
// TODO: turn into Editor function to not rely on runtime to build xref
    public class LabelConverter : MonoBehaviour {
        [SerializeField] private string fileName = String.Empty;
        [SerializeField] private DictionaryValue partNameXref;

        private void Awake() {
            ImportJsonData();
        }

        private void ImportJsonData() {
            TextAsset jsonString = Resources.Load<TextAsset>(fileName);
            ListOfLabelNames jsonList = JsonUtility.FromJson<ListOfLabelNames>(jsonString.text);
            
            // Clear and then populate the PartLabelXref scriptable object
            partNameXref.Value.Clear();
            for (int i = 0; i < jsonList.LabelNames.Length; i++) {
                partNameXref.Value.Add(jsonList.LabelNames[i].label, jsonList.LabelNames[i].name);
            }
        }

        [System.Serializable]
        public class LabelName {
            public string label;
            public string name;
        }
        [System.Serializable]
        public class ListOfLabelNames {
            public LabelName[] LabelNames;
        }
    }
}