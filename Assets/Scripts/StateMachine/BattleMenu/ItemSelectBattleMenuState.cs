using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Consumables;
using Gameplay.Player;
using Gameplay.Units;
using UI.BattleMenu;
using UI.Menus;
using UI.Menus.Data;

namespace StateMachine.BattleMenu
{
    public class ItemSelectBattleMenuState : BattleMenuState
    {
        public ItemSelectBattleMenuState(PlayerUnit player,
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            CombatService combatService) :
            base(player,
                playerInputProvider,
                menuItemsUpdater,
                combatService)
        {
        }

        public override void InitMenuItems()
        {
            var inventory = Player.UnitInventoryData;

            Dictionary<BaseConsumable, int> itemsDict = new();

            foreach (var item in inventory.Consumables)
                if (!itemsDict.TryAdd(item, 1))
                    itemsDict[item]++;

            foreach (var itemQuantityKv in itemsDict)
            {
                var item = itemQuantityKv.Key;
                var quantity = itemQuantityKv.Value;

                MenuItems.Add(
                    MenuItemData.ForConsumableItem(
                        item,
                        () => item.CanUse(CombatService),
                        () =>
                        {
                            Player.UnitInventoryData.RemoveItem(item);
                            StateMachine.SelectAction(item);
                        },
                        quantity
                    ));
            }
        }

        public override void OnUISubmit()
        {
            MenuItemsUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            MenuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            MenuItemsUpdater.UpdateSelection(+1);
        }

        public override void OnUIBack()
        {
            StateMachine.GoToPrevState().Forget();
        }
    }
}