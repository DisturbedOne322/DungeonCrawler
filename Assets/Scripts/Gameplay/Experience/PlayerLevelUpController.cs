using Cysharp.Threading.Tasks;

namespace Gameplay.Experience
{
    public class PlayerLevelUpController
    {
        private readonly ExperienceData _experienceData;
        private readonly PlayerLevelUpBonusApplier _playerLevelUpBonusApplier;
        private readonly PlayerStatDistributionController _playerStatDistributionController;

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
            await _playerStatDistributionController.DistributeStatPoints();
            _playerLevelUpBonusApplier.ApplyLevelUpBonus();
            _experienceData.IncrementLevel();
        }
    }
}