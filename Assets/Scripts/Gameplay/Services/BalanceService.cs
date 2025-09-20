using Data;

namespace Gameplay.Services
{
    public class BalanceService
    {
        private readonly GameplayData _gameplayData;

        public BalanceService(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }
        
        public bool TryPurchase(int price)
        {
            if (GetBalance() < price)
                return false;
            
            _gameplayData.Coins.Value -= price;
            return true;
        }
        
        public void AddBalance(int amount) => _gameplayData.Coins.Value += amount;
        
        public int GetBalance() => _gameplayData.Coins.Value;
    }
}