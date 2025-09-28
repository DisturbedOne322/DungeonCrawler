using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StateMachine.BattleMenu;
using UI.BattleMenu;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuItemsScroller : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private float _visibilityCheckOffset = 5;

        private RectTransform _area;
        private RectTransform _content;
        
        private List<BaseMenuItemView> _menuItems;
        private MenuItemsUpdater _menuItemsUpdater;

        private readonly Vector3[] _parentCorners = new Vector3[4];
        private readonly Vector3[] _itemCorners = new Vector3[4];

        private float _parentYPos;
        private float _areaHeight;
        private float _itemHeight;

        private int _prevItemIndex;

        private IEnumerator Start()
        {
            yield return null;

            _content = _layoutGroup.GetComponent<RectTransform>();
            _area = _content.parent.GetComponent<RectTransform>();
            
            _areaHeight = _area.rect.height;
            _area.GetWorldCorners(_parentCorners);
        }

        public void SetData(List<BaseMenuItemView> menuItems, MenuItemsUpdater menuItemsUpdater)
        {
            _menuItems = menuItems;
            _menuItemsUpdater = menuItemsUpdater;

            _menuItemsUpdater.OnViewChanged.Subscribe(_ => UpdateView()).AddTo(gameObject);
        }

        private void UpdateView()
        {
            if(!_menuItemsUpdater.IsSelectionValid())
                return;
            
            var activeItemId = _menuItemsUpdater.GetSelectedIndex();
            var item = _menuItems[activeItemId];

            if (!IsVisible(item))
            {
                if (_itemHeight == 0)
                    _itemHeight = ((RectTransform)item.transform).rect.height;

                var localPos = _area.InverseTransformPoint(item.transform.position);

                _parentYPos = GetTargetPos(activeItemId, localPos.y);

                _content.DOKill();
                _content.DOAnchorPosY(_parentYPos, 0.25f).SetEase(Ease.OutCubic);
            }
            
            _prevItemIndex = activeItemId;
        }

        private bool IsVisible(BaseMenuItemView item)
        {
            var itemRect = item.transform as RectTransform;

            itemRect.GetWorldCorners(_itemCorners);

            var itemTop = _itemCorners[1].y;
            var itemBottom = _itemCorners[0].y;

            var parentTop = _parentCorners[1].y;
            var parentBottom = _parentCorners[0].y;

            return itemTop <= parentTop + _visibilityCheckOffset && itemBottom >= parentBottom - _visibilityCheckOffset;
        }

        private float GetTargetPos(int currentIndex, float localY)
        {
            if (currentIndex == 0 && _prevItemIndex == _menuItems.Count - 1)
                return 0;

            if (currentIndex == _menuItems.Count - 1 && _prevItemIndex == 0)
            {
                float offset = 0;
                while (offset < _areaHeight) 
                    offset += GetStep();
                
                if(offset != 0)
                    offset -= GetStep();
                
                return _content.rect.height - offset;
            }

            int indexIncrease = Mathf.Abs(currentIndex - _prevItemIndex);
            if (indexIncrease != 1)
            {
                if (currentIndex > _prevItemIndex)
                    return _parentYPos + indexIncrease * GetStep();
                
                return _parentYPos - indexIncrease * GetStep();
            }
            
            if (localY - _itemHeight < -_areaHeight / 2f)
                _parentYPos += GetStep();
            
            if (localY + _itemHeight > _areaHeight / 2f)
                _parentYPos -= GetStep();
            
            return _parentYPos;
        }
        
        private float GetStep() => _itemHeight + _layoutGroup.spacing;
    }
}