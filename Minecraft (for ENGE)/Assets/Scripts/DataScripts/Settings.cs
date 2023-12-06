using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Settings
{
    public static PlaceChunks controller;
    
    
    // Move Settings Vars
    public static float sensitivity = .5f;

    
    // Graphics Settings
    public static float renderDist = 2;
 

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
            
            PlayerControlls.Forword = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[3]));
            PlayerControlls.Backword = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[4]));
            PlayerControlls.Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[5]));
            PlayerControlls.Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[6]));
            PlayerControlls.Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[7]));
            PlayerControlls.OpenSettings = (KeyCode)System.Enum.Parse(typeof(KeyCode), Settings.makeKeyCode(data[8]));
            PlayerControlls.sensitivity = float.Parse(data[9]);


            if (Settings.inGame)
            {
                
                controller.refreshRenderDist();
            }
        }
    }

    public static string makeKeyCode(string s)
    {
        string tempString = "";
        for(int i = 0; i < s.Length; i++)
        {
            if(i == 0)
            {
                tempString += s[i].ToString().ToUpper();
            }
            else
            {
                tempString += s[i].ToString().ToLower();
            }
        }
        return tempString;
    }
    
}

