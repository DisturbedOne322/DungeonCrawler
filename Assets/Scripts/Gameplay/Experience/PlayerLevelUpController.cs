using Cysharp.Threading.Tasks;

namespace Gameplay.Experience
{
    public class PlayerLevelUpController
    {
        private readonly ExperienceData _experienceData;
        private readonly PlayerStatDistributionController _playerStatDistributionController;
        private readonly PlayerLevelUpBonusApplier _playerLevelUpBonusApplier;
        
        public PlayerLevelUpController(ExperienceData experienceData, 
            PlayerStatDistributionController playerStatDistributionController,
            PlayerLevelUpBonusApplier playerLevelUpBonusApplier)
        {
            _experienceData = experienceData;
            _playerStatDistributionController = playerStatDistributionController;
            _playerLevelUpBonusApplier = playerLevelUpBonusApplier;
        }

        public async UniTask ProcessLevelUp()
        {
            _experienceData.IncrementLevel();
            _playerLevelUpBonusApplier.ApplyLevelUpBonus();
            await _playerStatDistributionController.DistributeStatPoints();
        }
    }
}