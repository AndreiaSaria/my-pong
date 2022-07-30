using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(426,240,false);
        
        //https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html
        Application.targetFrameRate = 30;

        Application.runInBackground = true;
    }
}
