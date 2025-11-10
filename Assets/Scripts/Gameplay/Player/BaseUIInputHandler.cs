namespace Gameplay.Player
{
    public class BaseUIInputHandler : IUiInputHandler
    {
        public virtual void OnUISubmit() { }
        public virtual void OnUIBack() { }
        public virtual void OnUIUp() { }
        public virtual void OnUIDown() { }
        public virtual void OnUILeft() { }
        public virtual void OnUIRight() { }
    }
}