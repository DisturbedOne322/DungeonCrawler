using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class AspectRatioLayoutElement : MonoBehaviour, ILayoutElement
    {
        [SerializeField] private float _aspectRatio = 1f;
        [SerializeField] private RectTransform _rectTransform;
        
        public void CalculateLayoutInputHorizontal() { }
        public void CalculateLayoutInputVertical() { }

        public float minWidth => -1;
        public float preferredWidth => -1;
        public float flexibleWidth => -1;

        public float minHeight => -1;
        public float preferredHeight
        {
            get
            {
                float width = _rectTransform.rect.width;
                return width / _aspectRatio;
            }
        }

        public float flexibleHeight => -1;
        public int layoutPriority => 1;
    }
}