using UnityEditor;
using UnityEngine;

public class WindowCapture : Editor
{
    [MenuItem("MyTools/CaptureActiveWindow")]
    static void CaptureActiveWindow()
    {
        WindowCaptureUtility.CaptureWindow();
    }
    [MenuItem("MyTools/CaptureGameView")]
    static void CaptureGameView()
    {
        WindowCaptureUtility.CaptureGameWindow();
    }

}