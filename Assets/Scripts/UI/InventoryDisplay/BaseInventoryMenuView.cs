using UI.Menus;
using UnityEngine;

namespace UI.InventoryDisplay
{
    public abstract class BaseInventoryMenuView : MonoBehaviour
    {
        [SerializeField] protected MenuPageView MenuPageView;

        protected BaseInventoryDisplayMenu DisplayMenu;
        
        private void Start()
        {
            Initialize();
        }

        protected abstract void Initialize();

        public void Select()
        {
            DisplayMenu.TakeControls();
        }

        public void Deselect()
        {
            DisplayMenu.RemoveControls();
        }
    }
}