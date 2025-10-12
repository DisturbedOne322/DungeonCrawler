using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using Gameplay.Consumables;
using Gameplay.Player;
using Gameplay.Units;
using UI.BattleMenu;
using UniRx;

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

        public override void LoadMenuItems()
        {
            MenuItems.Clear();

            var inventory = Player.UnitInventoryData;

            Dictionary<BaseConsumable, int> itemsDict = new ();

            foreach (var item in inventory.Consumables)
            {
                if (!itemsDict.TryAdd(item, 1)) 
                    itemsDict[item]++;
            }
            
            foreach (var itemQuantityKv in itemsDict)
            {
                var item = itemQuantityKv.Key;
                var quantity = itemQuantityKv.Value;
                
                MenuItems.Add(
                    MenuItemData.ForItem(
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
            
            MenuItemsUpdater.SetMenuItems(MenuItems);
        }

        protected override void SubscribeToInputEvents()
        {
            Disposables = new();
            
            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));            
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
            Disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ => StateMachine.GoToPrevState().Forget()));
        }
    }
}