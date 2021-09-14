using System.Collections.Generic;
using UnityEngine;

namespace Assets.PhantomSyntax.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Dictionary<string, string>", order = 3)]
    public class DictionaryValue : ScriptableObject {
        public Dictionary<string, string> Value = new Dictionary<string, string>();
    }
}