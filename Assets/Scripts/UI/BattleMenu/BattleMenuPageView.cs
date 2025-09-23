using System.Collections.Generic;
using UnityEngine;

namespace UI.BattleMenu
{
    public class BattleMenuPageView : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;

        public void SetItems<T>(List<T> items) where T : Component
        {
            foreach (var item in items) 
                item.transform.SetParent(_content, false);
        }
    }
}