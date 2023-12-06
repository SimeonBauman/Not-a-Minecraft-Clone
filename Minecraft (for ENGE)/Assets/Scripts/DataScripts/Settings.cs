using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Settings
{

    // Move Settings
    public static float sensitivity = .5f;

    public static PlaceChunks controller;
    // Graphics Settings
    public static float renderDist = 2;
    public static bool shadows = true;

    public static bool inGame = false;

    public static string dataSettingsPath = "Assets/Scripts/DataScripts/Data.txt";

    public static string[] outputData()
    {
        string[] arrLine = File.ReadAllLines(dataSettingsPath);

        return arrLine;
    }

    public static void editData(int line, string value)
    {
        string[] arrLine = File.ReadAllLines(dataSettingsPath);
        arrLine[line] = value;
        File.WriteAllLines(dataSettingsPath, arrLine);
        Settings.updateSettings();
    }

    public static void updateSettings()
    {
        if (controller != null)
        {
            string[] data = Settings.outputData();

            controller.changeRenderDist(float.Parse(data[2]));

            switch (int.Parse(data[0])) {
                case 0:
                    controller.sun.shadows = LightShadows.Soft;
                    break;
                case 1:
                    controller.sun.shadows = LightShadows.Hard;
                    break;
                case 2:
                    controller.sun.shadows = LightShadows.None;
                    break;
               
            }

            switch (int.Parse(data[1]))
            {
                case 0:
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 2:
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }
                

            


            if (Settings.inGame)
            {
                
                controller.refreshRenderDist();
            }
        }
    }
    
}

