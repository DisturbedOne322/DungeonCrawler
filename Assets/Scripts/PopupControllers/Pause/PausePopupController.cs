using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Services.Stats;
using UI;
using UI.Popups;

namespace PopupControllers.Pause
{
    public class PausePopupController : BaseUIInputHandler
    {
        private readonly UIFactory _uiFactory;
        private readonly PlayerStatTableBuilder  _playerStatTableBuilder;

        private PausePopup _activePopup;
        
        public PausePopupController(UIFactory uiFactory, PlayerStatTableBuilder playerStatTableBuilder)
        {
            _uiFactory = uiFactory;
            _playerStatTableBuilder = playerStatTableBuilder;
        }

        public void OpenPopup()
        {
            _activePopup = _uiFactory.CreatePopup<PausePopup>();
            _activePopup.SetStats(_playerStatTableBuilder.GetStatsTable(_playerStatTableBuilder.CreateMenuItems()));
        }

        public void ClosePopup()
        {
            _activePopup.HidePopup().Forget();
        }
    }
}