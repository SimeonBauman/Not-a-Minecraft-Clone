using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ControlsMenu : MonoBehaviour
{
    public TMP_InputField forword;
    public TMP_InputField backword;
    public TMP_InputField left;
    public TMP_InputField right;
    public TMP_InputField jump;



    private void Awake()
    {
        string[] values = Settings.outputData();
        forword.text = values[3];
        backword.text = values[4];
        left.text = values[5];
        right.text = values[6];
        jump.text = values[7];

    }

    public void changeForword()
    {
        string value = forword.text;
        Settings.editData(3, value);

    }
    public void changeBackword()
    {
        string value = forword.text;
        Settings.editData(4, value);

    }
    public void changeLeft()
    {
        string value = forword.text;
        Settings.editData(5, value);

    }
    public void changeRight()
    {
        string value = forword.text;
        Settings.editData(6, value);

    }
    public void changeJump()
    {
        string value = forword.text;
        Settings.editData(7, value);

    }


}
