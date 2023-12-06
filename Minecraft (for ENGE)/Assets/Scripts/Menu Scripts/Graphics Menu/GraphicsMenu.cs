using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GraphicsMenu : MonoBehaviour
{

    public TMP_Dropdown shadows;
    public TMP_Dropdown fullscreen;
    public TMP_InputField renderDistance;
    public Light sun;


    private void Awake()
    {
        string[] values = Settings.outputData();
        shadows.value = int.Parse(values[0]);
        fullscreen.value = int.Parse(values[1]);
        renderDistance.text = values[2];
    }
    public void changeShadows()
    {
        int value  = shadows.value;
        Settings.editData(0, value.ToString());

        
        
    }

    public void changeWindowed()
    {
        int value = fullscreen.value;
        Settings.editData(1, value.ToString());

    }

    public void changeRenderDist()
    {
        string value = renderDistance.text;
        
        Settings.editData(2,value );

    }

    
}
