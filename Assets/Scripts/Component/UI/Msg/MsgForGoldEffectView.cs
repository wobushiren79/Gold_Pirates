using UnityEditor;
using UnityEngine;
using DG.Tweening;
public class MsgForGoldEffectView : MsgView
{
    public Vector2 targetPostion;
    public void SetTargetPosition(Vector2 targetPostion)
    {
        this.targetPostion = targetPostion;
    }

    public override void AnimForMove()
    {
        RectTransform rtf = (RectTransform)transform;
        rtf.DOAnchorPos(targetPostion, 1).SetEase(Ease.Linear);
        CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1).SetDelay(0.8f).OnComplete(() =>
        {
            Destroy(gameObject);
        });

    }
}