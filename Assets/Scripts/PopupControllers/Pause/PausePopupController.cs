using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UI;
using UI.Popups;

namespace PopupControllers.Pause
{
    public class PausePopupController : BaseUIInputHandler
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly UIFactory _uiFactory;

        private PausePopup _activePopup;

        public PausePopupController(UIFactory uiFactory, PlayerInputProvider playerInputProvider)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
        }

        public void OpenPopup()
        {
            _playerInputProvider.AddUiInputOwner(this);
            _activePopup = _uiFactory.CreatePopup<PausePopup>();
        }

        public void ClosePopup()
        {
            _activePopup.HidePopup().Forget();
            _playerInputProvider.TrimInputStackTo(this);
        }
    }
}