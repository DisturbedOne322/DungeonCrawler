using Gameplay.Player;
using UniRx;

namespace UI.PlayerBattleMenu
{
    public class SelectableMenuState : BaseMenuState
    {
        private readonly bool _canGoBack;

        public SelectableMenuState(PlayerInputProvider input, BattleMenuController controller, bool canGoBack = false)
            : base(input, controller)
        {
            _canGoBack = canGoBack;
        }

        protected override void SubscribeInput(CompositeDisposable d)
        {
            d.Add(Input.OnUiUp.Subscribe(_ => Navigator.UpdateSelection(Items, -1)));
            d.Add(Input.OnUiDown.Subscribe(_ => Navigator.UpdateSelection(Items, +1)));
            d.Add(Input.OnUiSubmit.Subscribe(_ => Navigator.ExecuteSelection(Items)));

            if (_canGoBack)
                d.Add(Input.OnUiBack.Subscribe(_ => Controller.ReturnToMainMenu()));
        }
    }
}