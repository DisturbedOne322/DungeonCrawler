using System;

namespace UI.PlayerBattleMenu
{
    public class MenuItemData
    {
        public Func<bool> Selectable;
        public Action OnSelected;
    }
}