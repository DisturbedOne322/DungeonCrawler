using System.Collections.Generic;

namespace UI.PlayerBattleMenu
{
    public class MenuItemsUpdater
    {
        public void UpdateItems(List<MenuItemView> menuItems, ref int selectedIndex, int increment = 0)
        {
            selectedIndex += increment;
            
            if(selectedIndex < 0)
                selectedIndex = menuItems.Count - 1;
            
            if(selectedIndex >= menuItems.Count)
                selectedIndex = 0;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if(!menuItems[i].Data.Selectable())
                    menuItems[i].SetUnusable();
                
                menuItems[i].SetSelected(i == selectedIndex);
            }
        }
    }
}