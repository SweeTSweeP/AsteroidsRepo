using UnityEngine;
using UnityEngine.UI;

namespace UI.Curtain
{
    [RequireComponent(typeof(Image))]
    public class Curtain : MonoBehaviour
    {
        private CanvasGroup _curtain;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void HideCurtain() => 
            ChangeCurtainState(true);

        public void ShowCurtain() =>
            ChangeCurtainState(false);

        private void ChangeCurtainState(bool isTransparent)
        {
            if (_curtain == null) _curtain = GetComponent<CanvasGroup>();

            _curtain.alpha = isTransparent ? 0 : 1;
        }
    }
}
