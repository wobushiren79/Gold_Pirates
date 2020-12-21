using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIViewForBattle : BaseUIView
{
    public RectTransform rtfContainer;
    public RectTransform rtfEnemy;
    public RectTransform rtfPlayer;

    public TextMeshProUGUI ui_TvEnemyGoldNumber;
    public TextMeshProUGUI ui_TvPlayerGoldNumber;

    public int playerNumber;
    public int enemyNumber;

    protected float minRate = 0.2f;
    protected float maxRate = 0.8f;

    public Color colorForPlayerOutline;
    public Color colorForEnemyOutline;
    public void SetData(int playerNumber, int enemyNumber)
    {
        this.playerNumber = playerNumber;
        this.enemyNumber = enemyNumber;
        RefreshUI();
    }

    public void RefreshUI()
    {
        float widthContainer = rtfContainer.rect.width;
        float heightContainer = rtfContainer.rect.height;
        if ((playerNumber + enemyNumber) == 0)
        {
            rtfPlayer.sizeDelta = new Vector2(widthContainer * 0.5f, heightContainer);
            rtfEnemy.sizeDelta = new Vector2(widthContainer * 0.5f, heightContainer);
        }
        else
        {
            float playerRate = ((float)playerNumber / (playerNumber + enemyNumber));
            float enemyRate = ((float)enemyNumber / (playerNumber + enemyNumber));
            if (playerRate < minRate)
            {
                playerRate = minRate;
                enemyRate = maxRate;
            }
            else if (enemyRate < minRate)
            {
                enemyRate = minRate;
                playerRate = maxRate;
            }
            rtfPlayer.DOKill();
            rtfPlayer.DOSizeDelta(new Vector2(widthContainer * playerRate , heightContainer), 0.5f);
            rtfEnemy.DOKill();
            rtfEnemy.DOSizeDelta(new Vector2(widthContainer * enemyRate , heightContainer), 0.5f);
        }
        SetGoldNumber(CharacterTypeEnum.Enemy);
        SetGoldNumber(CharacterTypeEnum.Player);
    }

    public void SetGoldNumber(CharacterTypeEnum characterType)
    {
        if (characterType == CharacterTypeEnum.Player)
        {
            ui_TvPlayerGoldNumber.text = playerNumber + "";
            ui_TvPlayerGoldNumber.outlineColor = colorForPlayerOutline;
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            ui_TvEnemyGoldNumber.text = enemyNumber + "";
            ui_TvEnemyGoldNumber.outlineColor = colorForEnemyOutline;
        }
    }
}