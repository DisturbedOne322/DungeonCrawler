using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.ShopRooms.Shop;
using Gameplay.Player;
using UI.Menus;
using UI.Menus.Data;

namespace StateMachine.Shop
{
    public class MainShopState : BaseShopState
    {
        private readonly ShopItemsProvider _shopItemsProvider;

        public MainShopState(
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            ShopItemsProvider shopItemsProvider) :
            base(playerInputProvider, menuItemsUpdater)
        {
            _shopItemsProvider = shopItemsProvider;
        }

        public override void InitMenuItems()
        {
            MenuItems.Clear();

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "EQUIPMENT",
                    () => _shopItemsProvider.AnyEquipmentSold(),
                    () => StateMachine.GoToState<EquipmentShopState>().Forget())
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "SKILLS",
                    () => _shopItemsProvider.AnySkillsSold(),
                    () => StateMachine.GoToState<SkillsShopState>().Forget())
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "STATUS EFFECTS",
                    () => _shopItemsProvider.AnyStatusEffectsSold(),
                    () => StateMachine.GoToState<StatusEffectsShopState>().Forget())
            );

            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "CONSUMABLES",
                    () => _shopItemsProvider.AnyConsumablesSold(),
                    () => StateMachine.GoToState<ConsumablesShopState>().Forget())
            );
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
            StateMachine.InvokeShopExit();
        }
    }
}