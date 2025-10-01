using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay.Combat;
using UI;
using UI.Gameplay;

namespace Controllers
{
    public class EquipmentChangeController
    {
        private readonly UIFactory _uiFactory;

        public EquipmentChangeController(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public async UniTask<EquipmentSelectChoice> MakeEquipmentChoice(BaseGameItem oldItem, BaseGameItem newItem)
        {
            UniTaskCompletionSource<EquipmentSelectChoice> tcs = new ();

            var popup = _uiFactory.CreatePopup<EquipmentChangeConfirmPopup>();
            popup.ShowPopup();
            popup.SetData(oldItem, newItem);

            popup.OnKeepPressed += () => tcs.TrySetResult(EquipmentSelectChoice.Keep);
            popup.OnChangePressed += () => tcs.TrySetResult(EquipmentSelectChoice.Change);
            
            var result =  await tcs.Task;

            return result;
        }
    }
}