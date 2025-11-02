using Gameplay.Player;
using UniRx;

namespace UI.Navigation
{
    public class HorizontalUINavigator : UINavigator
    {
        public HorizontalUINavigator(PlayerInputProvider playerInputProvider) : base(playerInputProvider)
        {
            
        }

        protected override void SubscribeToPlayerInput()
        {
            PlayerInputProvider.OnUiLeft.Subscribe(_ => UpdateSelection(-1)).AddTo(Disposables);
            PlayerInputProvider.OnUiRight.Subscribe(_ => UpdateSelection(+1)).AddTo(Disposables);
        }
    }
}