using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.Shop;
using Gameplay.Player;
using UI.BattleMenu;
using UniRx;

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
            
            MenuItems.Add(
                MenuItemData.ForSubmenu(
                    "LEAVE",
                    () => true,
                    () => StateMachine.InvokeShopExit())
            );
        }

        protected override void SubscribeToInputEvents()
        {
            Disposables = new ();

            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
        }
    }
}