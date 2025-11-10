using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay;
using Gameplay.Player;
using UI;
using UI.Gameplay.Experience;

namespace PopupControllers
{
    public class EquipmentChangeController
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly UIFactory _uiFactory;

        public EquipmentChangeController(UIFactory uiFactory, PlayerInputProvider playerInputProvider)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
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