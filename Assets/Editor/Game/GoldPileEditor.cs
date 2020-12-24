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
        if (EditorUI.GUIButton("获取数据", 100, 50))
        {
            GetGoldPileData();
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

    protected void GetGoldPileData()
    {
        //清空数据
        for (int i = 0; i < listGoldPile.Length; i++)
        {
            GameObject itemObj = listGoldPile[i];
            DestroyImmediate(itemObj);
        }

        List<GameLevelBean> listData = gameLevelService.QueryDataByLevel(levelForGame);
        if (!CheckUtil.ListIsNull(listData))
        {
            GameLevelBean gameLevelData = listData[0];
            string goldPileData = gameLevelData.gold_pile;
            if (CheckUtil.StringIsNull(goldPileData))
                return;
            string[] listGoldPileArray = StringUtil.SplitBySubstringForArrayStr(goldPileData, '|');

            sizeForGoldPile = listGoldPileArray.Length;
            listGoldPile = new GameObject[sizeForGoldPile];

            for (int i = 0; i < listGoldPileArray.Length; i++)
            {
                string itemGoldPileData = listGoldPileArray[i];
                string[] pileTempData = StringUtil.SplitBySubstringForArrayStr(itemGoldPileData, ':');
                pileTempData[0] = pileTempData[0].Replace("(Clone)", "");
                GameObject objModel = Resources.Load("GoldPile/" + pileTempData[0]) as GameObject;
                GameObject objItem = Instantiate(objModel);
                objItem.name = objItem.name.Replace("(Clone)", "");
                objItem.SetActive(true);
                float[] pileDataPosition = StringUtil.SplitBySubstringForArrayFloat(pileTempData[1], ',');
                objItem.transform.position = new Vector3(pileDataPosition[0], pileDataPosition[1], pileDataPosition[2]);
                listGoldPile[i] = objItem;
            }
            Resources.UnloadUnusedAssets();
        }
    }

    protected void SetGoldPileData()
    {
        goldPileData = "";
        for (int i = 0; i < listGoldPile.Length; i++)
        {
            GameObject itemObj = listGoldPile[i];
            if (itemObj == null)
                continue;
            goldPileData += (itemObj.name + ":" + itemObj.transform.position.x + "," + itemObj.transform.position.y + "," + itemObj.transform.position.z + "|");
            DestroyImmediate(itemObj);
        }
        if (CheckUtil.StringIsNull(goldPileData))
            return;
        List<GameLevelBean> listData = gameLevelService.QueryDataByLevel(levelForGame);
        if (!CheckUtil.ListIsNull(listData))
        {
            GameLevelBean gameLevelData = listData[0];
            gameLevelData.gold_pile = goldPileData;
            gameLevelService.InsertDataByLevel(gameLevelData);
        }
    }

}