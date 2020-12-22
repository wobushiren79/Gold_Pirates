using UnityEditor;
using UnityEngine;
using DG.Tweening;
public class MsgForGoldEffectView : MsgView
{
    public Vector2 targetPostion;
    public float timeForAnim = 1f;
    public void SetTargetPosition(Vector2 targetPostion)
    {
        this.targetPostion = targetPostion;
    }

    public void SetAnimTime(float timeForAnim)
    {
        this.timeForAnim = timeForAnim;
    }

    public override void AnimForMove()
    {
        RectTransform rtf = (RectTransform)transform;
        float delayTime = Random.Range(0f, 0.3f);
        rtf.DOAnchorPos(targetPostion, timeForAnim).SetEase(Ease.Linear).SetDelay(delayTime);
        CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, timeForAnim * 0.1f).SetDelay(timeForAnim * 0.9f + delayTime).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}