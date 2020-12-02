using UnityEditor;
using UnityEngine;

public class UIViewForGoldProgress : BaseUIView
{
    public ProgressView ui_GoldPro;
  
    public void SetData(int maxGold,int currentGold)
    {
        ui_GoldPro.SetData(maxGold, currentGold);
    }


}