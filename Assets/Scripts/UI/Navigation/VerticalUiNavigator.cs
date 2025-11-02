using Gameplay.Player;
using UniRx;

namespace UI.Navigation
{
    public class VerticalUiNavigator : UINavigator
    {
        public VerticalUiNavigator(PlayerInputProvider playerInputProvider) : base(playerInputProvider)
        {
        }

        protected override void SubscribeToPlayerInput()
        {
            PlayerInputProvider.OnUiDown.Subscribe(_ => UpdateSelection(+1)).AddTo(Disposables);
            PlayerInputProvider.OnUiUp.Subscribe(_ => UpdateSelection(-1)).AddTo(Disposables);
        }
    }
}