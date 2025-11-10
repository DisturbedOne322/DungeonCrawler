using System.Collections.Generic;
using AssetManagement.AssetProviders;
using AssetManagement.Configs;
using Data;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.MovementsHistory
{
    public class PlayerMovementHistoryDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RoomIconDisplay _roomIconDisplay;
        [SerializeField] [Min(2)] private int _maximumRoomsHistory;
        [SerializeField] private float _scaleUpDuration = 0.2f;
        [SerializeField] private float _scaleDownDuration = 0.15f;

        private readonly LinkedList<RoomIconDisplay> _activeDisplays = new();

        private DungeonVisualsConfig _dungeonVisualsConfig;

        [Inject]
        private void Construct(PlayerMovementHistory history, DungeonVisualsConfigProvider dungeonVisualsConfigProvider)
        {
            _dungeonVisualsConfig = dungeonVisualsConfigProvider.GetConfig();

            for (var i = 0; i < _maximumRoomsHistory; i++)
            {
                var display = Instantiate(_roomIconDisplay, _parent);
                _activeDisplays.AddLast(display);
            }

            history.RoomsHistory.ObserveAdd().Subscribe(newRoom => { ShowRoomDisplay(newRoom.Value); })
                .AddTo(gameObject);
        }

        private void ShowRoomDisplay(RoomType roomType)
        {
            var display = _activeDisplays.Last.Value;

            _activeDisplays.RemoveLast();

            display.transform.DOScale(Vector3.zero, _scaleDownDuration).OnComplete(() =>
            {
                if (_dungeonVisualsConfig.TryGetRoomIcon(roomType, out var roomIcon))
                    display.SetIcon(roomIcon);

                display.transform.localScale = Vector3.zero;
                display.transform.SetAsFirstSibling();
                display.transform.DOScale(Vector3.one, _scaleUpDuration).SetEase(Ease.OutBack).SetLink(gameObject);
            }).SetLink(gameObject);

            _activeDisplays.AddFirst(display);
        }
    }
}