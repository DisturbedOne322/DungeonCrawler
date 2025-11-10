using Gameplay.Player;

namespace UI.Navigation
{
    public class HorizontalUINavigator : UINavigator
    {
        public HorizontalUINavigator(PlayerInputProvider playerInputProvider) : base(playerInputProvider)
        {
        }

        public override void OnUILeft()
        {
            UpdateSelection(-1);
        }

        public override void OnUIRight()
        {
            UpdateSelection(+1);
        }
    }
}