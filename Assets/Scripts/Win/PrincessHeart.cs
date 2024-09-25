using UnityEngine;
using DG.Tweening;

public class PrincessHeart : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip princessSound;

    [Header("Position Parameters")]
    [SerializeField] private float newYPosition;
    private Vector3 originalScale;
    private Vector3 scaleTo;

    void Start()
    {
        originalScale = transform.localScale;
        scaleTo = originalScale * 12;
        gameObject.SetActive(false);
    }

    public void TriggerHeartAnimation()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        SoundManager.Instance.PlaySound(winSound);

        Sequence heartAnimation = DOTween.Sequence();
        heartAnimation.Append(transform.DOScale(scaleTo, 0.8f).SetEase(Ease.InOutSine));
        heartAnimation.Join(transform.DOMoveY(newYPosition, 0.8f)
         .SetEase(Ease.InOutSine)).OnComplete(() =>
         SoundManager.Instance.PlaySound(princessSound)
        );
    }
}
