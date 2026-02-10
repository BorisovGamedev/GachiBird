using UnityEngine;

namespace Flappy.Game
{
    public class BaseWindow : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false); 
        }
    }
}