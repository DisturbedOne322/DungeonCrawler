using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UI;
using UI.Popups;

namespace PopupControllers.Pause
{
    public class PausePopupController : BaseUIInputHandler
    {
        private readonly UIFactory _uiFactory;

        private PausePopup _activePopup;
        
        public PausePopupController(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void OpenPopup()
        {
            _activePopup = _uiFactory.CreatePopup<PausePopup>();
        }

        public void ClosePopup()
        {
            _activePopup.HidePopup().Forget();
        }
    }
}