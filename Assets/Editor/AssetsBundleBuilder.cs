using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AssetsBundleBuilder : Editor
{   
    // Version File
    private static string _VersionFile = "xversion";
    private static string _ABFilePath = Path.Combine(Application.dataPath, "AB");

    [MenuItem("Assets/Config AssetBundles")]
    static void ConfigAllAssetBundles()
    {
        UnityEngine.Object[] objs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets);
        foreach (var item in objs)
        {
            Debug.Log(item.name);
            string path = AssetDatabase.GetAssetPath(item);
            Debug.Log(path);
            AssetImporter ai = AssetImporter.GetAtPath(path);
            ai.assetBundleName = item.name;
            ai.assetBundleVariant = "ab";
        }
    }


    [MenuItem("Assets/Build AssetBundles")]
    public static void OnBuildAB()
    {
        //EditorUtility.DisplayDialog("Success", "开始打包~~", "OK");

        if (Directory.Exists(_ABFilePath))
        {
            Directory.Delete(_ABFilePath, true);
        }
        Directory.CreateDirectory(_ABFilePath);

        BuildPipeline.BuildAssetBundles("Assets/AB", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        PackageAllAssetBundles();
    }

    [MenuItem("Assets/Build Android AssetBundles")]
    public static void OnBuildAB_Android()
    {
        //EditorUtility.DisplayDialog("Success", "开始打包~~", "OK");

        if (Directory.Exists(_ABFilePath))
        {
            Directory.Delete(_ABFilePath, true);
        }
        Directory.CreateDirectory(_ABFilePath);

        BuildPipeline.BuildAssetBundles("Assets/AB", BuildAssetBundleOptions.None, BuildTarget.Android);

        PackageAllAssetBundles();
    }

    [MenuItem("Assets/Build iOS AssetBundles")]
    public static void OnBuildAB_iOS()
    {
        //EditorUtility.DisplayDialog("Success", "开始打包~~", "OK");

        if (Directory.Exists(_ABFilePath))
        {
            Directory.Delete(_ABFilePath, true);
        }
        Directory.CreateDirectory(_ABFilePath);

        BuildPipeline.BuildAssetBundles("Assets/AB", BuildAssetBundleOptions.None, BuildTarget.iOS);

        PackageAllAssetBundles();
    }

    [MenuItem("Assets/Generate Version File")]
    static void PackageAllAssetBundles()
    {
        using (FileStream fs = File.Open(Application.dataPath + "/AB/" + _VersionFile, FileMode.OpenOrCreate))
        {
            string line = "";
            string[] files = Directory.GetFiles(_ABFilePath, "*.ab", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                // 文件名
                string file_name = Path.GetFileNameWithoutExtension(file);

                // 文件MD5
                //string md5 = utils.GetMD5FromFile(file);

                // 文件大小
                FileInfo f = new FileInfo(file);
                long size = f.Length;

                // 生成
                //line = line + file_name + "|" + md5 + "|" + size + "\r\n";
            }
            byte[] bits = Encoding.UTF8.GetBytes(line);
            fs.Write(bits, 0, bits.Length);
        }

        EditorUtility.DisplayDialog("Success", "资源打包成功~~", "OK");
    }
}