using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Settings
{

    // Move Settings
    public static float sensitivity = .5f;


    // Graphics Settings
    public static float renderDist = 12;
    public static bool shadows = true;


    /*
     public static void worldList()
     {
         [SerializeField]private data d = new data();

         nums.Add(new int { });
         nums.Add(new int { });
         var jsonData = JsonUtility.ToJson(nums);
         System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", jsonData);
     }

     public static void printName()
     {
         var jsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/PotionData.json");
         Debug.Log(jsonData);
         List<int> n =  JsonUtility.FromJson<List<int>>(jsonData);
         Debug.Log(n[0]);
         Debug.Log(n[1]);

     }

 }*/
}

[System.Serializable]
public class data
{
    public static List<int> nums = new List<int>();

}
