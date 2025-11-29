using Cysharp.Threading.Tasks;
using Gameplay.Experience;
using Gameplay.Player;
using UI;
using UI.Popups;

namespace Gameplay.Pause
{
    public class PausePopupController : BaseUIInputHandler
    {
        private readonly UIFactory _uiFactory;
        private readonly PlayerStatTableBuilder  _playerStatTableBuilder;
        private readonly PlayerInputProvider _playerInputProvider;

        private PausePopup _activePopup;
        
        public PausePopupController(UIFactory uiFactory, PlayerStatTableBuilder playerStatTableBuilder, 
            PlayerInputProvider playerInputProvider)
        {
            _uiFactory = uiFactory;
            _playerStatTableBuilder = playerStatTableBuilder;
            _playerInputProvider = playerInputProvider;
        }

        public void OpenPopup()
        {
            _playerInputProvider.AddUiInputOwner(this);
            _activePopup = _uiFactory.CreatePopup<PausePopup>();
            _activePopup.SetStats(_playerStatTableBuilder.GetStatsTable(_playerStatTableBuilder.CreateMenuItems()));
        }

        public void ClosePopup()
        {
            _activePopup.HidePopup().Forget();
            _playerInputProvider.RemoveUiInputOwner(this);
        }
    }
}