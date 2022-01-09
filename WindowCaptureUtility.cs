using UnityEngine;
using UnityEditor;
using System.IO;


public class WindowCaptureUtility
{
    //Unityプロジェクトのパス
    private static string ProjectPath = Application.dataPath.Replace("Assets", "");
    //キャプチャーした画像の保存先
    private static string ExportPath = ProjectPath + "Capture/";
    //取得したウィンドウの縦幅がなぜか小さいので微調整用の変数
    private static int Offset_Height = 22;

    //保存先のフォルダ作成を作成する
    private static void CreateFolder()
    {
        if (!Directory.Exists(ExportPath))
        {
            Directory.CreateDirectory(ExportPath);
        }
    }

    //アクティブなウィンドウをキャプチャする
    public static void CaptureWindow()
    {
        string fileName = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
        CreateFolder();

        //選択されたウィンドウを取得する
        EditorWindow activeWindow = EditorWindow.focusedWindow;
        Vector2 position = activeWindow.position.position;
        Vector2Int size = new Vector2Int((int)activeWindow.position.width, (int)activeWindow.position.height + Offset_Height);
        //ウィンドウから色情報を取得してTexture2D→PNGに変換する
        Color[] colors = UnityEditorInternal.InternalEditorUtility.ReadScreenPixel(position, size.x, size.y);
        Texture2D texture2D = new Texture2D(size.x, size.y);
        texture2D.SetPixels(colors);
        byte[] bytes = texture2D.EncodeToPNG();
        File.WriteAllBytes(ExportPath + fileName, bytes);

        Debug.Log("Capture! : " + ExportPath + fileName);


    }

    //ゲームウィンドウをキャプチャする
    public static void CaptureGameWindow()
    {
        string fileName = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
        CreateFolder();

        // キャプチャを撮る
#if UNITY_2017_1_OR_NEWER
        ScreenCapture.CaptureScreenshot(ExportPath + fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#else
            Application.CaptureScreenshot(ExportPath + fileName); // ← GameViewにフォーカスがない場合、この時点では撮られない
#endif
        // GameViewを取得してくる
        var assembly = typeof(EditorWindow).Assembly;
        var type = assembly.GetType("UnityEditor.GameView");
        var gameview = EditorWindow.GetWindow(type);
        // GameViewを再描画
        gameview.Repaint();

        Debug.Log("ScreenShot: " + ExportPath + fileName);


    }
}