using TMPro;
using UnityEditor;
using UnityEngine;

public class MsgForGoldView : MsgView
{
    public TextMeshProUGUI ui_TvGold;

    public void SetGold(long gold)
    {
        if (ui_TvGold != null)
            ui_TvGold.text = gold + "";
    }

}