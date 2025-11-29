using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Core
{
    public class UITable : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _namesParent;
        [SerializeField] private VerticalLayoutGroup _statsParent;

        public void SetTable<TType1, TType2>(Dictionary<TType1, TType2> table) where TType1 : Component where TType2 : Component
        {
            foreach (var kv in table)
            {
                kv.Key.transform.SetParent(_namesParent.transform, false);
                kv.Value.transform.SetParent(_statsParent.transform, false);
            }
        }
    }
}