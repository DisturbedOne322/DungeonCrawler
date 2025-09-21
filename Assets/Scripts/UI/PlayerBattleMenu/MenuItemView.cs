using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerBattleMenu
{
    public class MenuItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private Image _backgroundImage;
        
        private MenuItemData _data;
        
        public MenuItemData Data => _data;

        public void SetData(MenuItemData data) => _data = data;
        public void SetText(string text) => _text.text = text;
        public void SetSelected(bool selected) => _backgroundImage.color = selected ? Color.green : Color.white;
        public void SetUnusable() => _backgroundImage.color = Color.grey;
    }
}