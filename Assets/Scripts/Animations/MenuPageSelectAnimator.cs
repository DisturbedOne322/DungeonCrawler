using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
    public class MenuPageSelectAnimator : MonoBehaviour
    {
        [SerializeField] private float _animTime = 0.2f;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _deselectColor;

        public void Select(Image image)
        {
            ChangeColor(image, _selectColor);
        }

        public void Deselect(Image image)
        {
            ChangeColor(image, _deselectColor);
        }

        private void ChangeColor(Image image, Color color)
        {
            image.DOKill();
            image.DOColor(color, _animTime).SetLink(gameObject).SetUpdate(true);
        }
    }
}