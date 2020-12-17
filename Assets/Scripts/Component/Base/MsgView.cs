using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MsgView : BaseMonoBehaviour
{
    public Text ui_TvContent;

    private void Awake()
    {
        AutoLinkUI();
    }

    private void Start()
    {
        AnimForMoveY();
    }

    /// <summary>
    /// 设置正文
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(string content)
    {
        if (ui_TvContent != null)
            ui_TvContent.text = content;
    }

    public void AnimForMoveY()
    {
        RectTransform rtf = (RectTransform)transform;
        rtf.DOAnchorPosY(rtf.anchoredPosition.y + 100, 3);
        CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1).SetDelay(2).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}