using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay;
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
            UniTaskCompletionSource<EquipmentSelectChoice> tcs = new();

            var popup = _uiFactory.CreatePopup<EquipmentChangeConfirmPopup>();
            popup.SetData(oldItem, newItem);

            popup.OnKeepPressed += () => tcs.TrySetResult(EquipmentSelectChoice.Keep);
            popup.OnChangePressed += () => tcs.TrySetResult(EquipmentSelectChoice.Change);

            popup.ShowPopup().Forget();

            var result = await tcs.Task;
            await popup.HidePopup();

            return result;
        }
    }
}