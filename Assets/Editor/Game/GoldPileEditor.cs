using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldPileEditor : EditorWindow
{

    public GameObject[] listGoldPile = new GameObject[0];
    public int sizeForGoldPile = 1;
    public int levelForGame = 1;
    public string goldPileData = "";
    public GameLevelService gameLevelService;

    [MenuItem("工具/金币堆设置")]
    static void CreateWindows()
    {
        EditorWindow.GetWindow(typeof(GoldPileEditor));
    }

    public GoldPileEditor()
    {
        this.titleContent = new GUIContent("金币堆设置");
    }

    private void OnEnable()
    {
        gameLevelService = new GameLevelService();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorUI.GUIText("应用场景等级");
        levelForGame = EditorUI.GUIEditorText(levelForGame, 50);
        if (EditorUI.GUIButton("确认设置", 100, 50))
        {
            SetGoldPileData();
        }
        EditorGUILayout.EndHorizontal();

        EditorUI.GUIText("金币堆数量");
        sizeForGoldPile = EditorUI.GUIEditorText(sizeForGoldPile, 50);
        if (listGoldPile.Length != sizeForGoldPile)
        {
            listGoldPile = new GameObject[sizeForGoldPile];
        }
        for (int i = 0; i < listGoldPile.Length; i++)
        {
            GameObject itemObj = listGoldPile[i];
            itemObj = EditorUI.GUIObj<GameObject>("金币容器", itemObj);
            listGoldPile[i] = itemObj;
        }
    }

    protected void SetGoldPileData()
    {
;       goldPileData = "";
        for (int i = 0; i < listGoldPile.Length; i++)
        {
            GameObject itemObj = listGoldPile[i];
            goldPileData += (itemObj.name + ":" + itemObj.transform.position.x + "," + itemObj.transform.position.y + "," + itemObj.transform.position.z + "|");
        }
        List<GameLevelBean> listData= gameLevelService.QueryDataByLevel(levelForGame);
        if (!CheckUtil.ListIsNull(listData))
        {
            GameLevelBean gameLevelData = listData[0];
            gameLevelData.gold_pile = goldPileData;
            gameLevelService.InsertDataByLevel(gameLevelData);
        }
    }

}