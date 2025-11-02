using Constants;
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
            if (!HasEnoughBalance(price))
                return false;

            _playerUnit.UnitInventoryData.Coins.Value -= price;
            return true;
        }
        
        public bool HasEnoughBalance(int price) => GetBalance() >= price;

        public void AddBalance(int amount)
        {
            int balance = GetBalance();
            balance += amount;

            if (balance > GameplayConstants.MaxBalance)
                balance = GameplayConstants.MaxBalance;
            
            _playerUnit.UnitInventoryData.Coins.Value = balance;
        }

        public int GetBalance() => _playerUnit.UnitInventoryData.Coins.Value;
    }
}