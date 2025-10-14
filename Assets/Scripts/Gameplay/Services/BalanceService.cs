using Gameplay.Units;

namespace Gameplay.Services
{
    public class BalanceService
    {
        private readonly PlayerUnit _playerUnit;

        public BalanceService(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }
        
        public bool TryPurchase(int price)
        {
            if (GetBalance() < price)
                return false;
            
            _playerUnit.UnitInventoryData.Coins.Value -= price;
            return true;
        }
        
        public void AddBalance(int amount) => _playerUnit.UnitInventoryData.Coins.Value += amount;
        
        public int GetBalance() => _playerUnit.UnitInventoryData.Coins.Value;
    }
}