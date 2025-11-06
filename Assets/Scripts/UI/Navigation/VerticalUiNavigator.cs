using Gameplay.Player;

namespace UI.Navigation
{
    public class VerticalUiNavigator : UINavigator
    {
        public VerticalUiNavigator(PlayerInputProvider playerInputProvider) : base(playerInputProvider)
        {
        }
        
        public override void OnUIDown() => UpdateSelection(+1);
        public override void OnUIUp() => UpdateSelection(-1);
    }
}