namespace Gameplay.Player
{
    public interface IUiInputHandler
    {
        void OnUISubmit();
        void OnUIBack();
        void OnUIUp();
        void OnUIDown();
        void OnUILeft();
        void OnUIRight();
    }
}