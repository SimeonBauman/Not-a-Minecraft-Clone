using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    public GameObject main;
    public GameObject play;
    public TMP_InputField field;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(true);
        play.SetActive(false);
    }

   

    public void createWorld()
    {
        main.SetActive(false);
        play.SetActive(false);
        background.SetActive(false);
        int seed = Random.Range(0, int.MaxValue);
        if(field.text.Length > 0)
        {
            seed = int.Parse(field.text);
            Debug.Log(seed);
        }
        NoiseVars.recalc(seed);
        SceneManager.LoadScene(1);
    }

    public void onPlay()
    {
        main.SetActive(false);
        play.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }
}
