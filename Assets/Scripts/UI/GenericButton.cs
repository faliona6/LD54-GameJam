using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class GenericButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float scaleAnimationTime = 0.2f;
        [SerializeField] private float scaleSize = 0.9f;

        private RectTransform rectTransform;
        private Vector3 initialScale;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            initialScale = rectTransform.localScale;
        }

        private void OnDestroy()
        {
            KillTweens();
        }

        private void KillTweens()
        {
            DOTween.Kill(this);
        }

        public void OnPointerDown(PointerEventData eventData) =>
            PlayScaleTween(new Vector3(scaleSize * initialScale.x, scaleSize * initialScale.y, 1));
        public void OnPointerUp(PointerEventData eventData) => 
            PlayScaleTween(initialScale);

        private void PlayScaleTween(Vector3 endScale)
        {
            rectTransform.DOScale(endScale, scaleAnimationTime)
                .SetEase(Ease.InOutCubic)
                .SetId(this);
        }
    }
}
