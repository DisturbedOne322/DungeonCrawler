using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class RoomIconDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void SetIcon(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.gameObject.SetActive(true);
        }
    }
}