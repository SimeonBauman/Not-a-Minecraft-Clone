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
    public GameObject worlds;
    public TMP_InputField field;
    public TMP_InputField name;
    public GameObject background;

    public GameObject worldSelectionButtons;
 
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
        CreateWorldFiles.createWorld(name.text, seed.ToString());
        NoiseVars.recalc(seed);
        SceneManager.LoadScene(1);
        
    }

    public void onPlay()
    {
        main.SetActive(false);
        string[] worlds = CreateWorldFiles.getWorlds();
        for(int i = 0; i < worlds.Length; i++)
        {
            GameObject g = Instantiate(worldSelectionButtons);
            g.transform.parent = this.worlds.transform;
            g.transform.localScale = new Vector3(1,1,1);
            g.transform.localPosition = new Vector2(0, -i * 60);
            g.GetComponentInChildren<TMP_Text>().text = worlds[i];
            g.name = worlds[i];
            g.GetComponentInChildren<Button>().onClick.AddListener(delegate { CreateWorldFiles.loadWorld(g.name); });

        }
        this.worlds.SetActive(true);
    }

    public void onCreate()
    {
        this.worlds.SetActive(false );
        play.SetActive(true );
    }

    public void quit()
    {
        Application.Quit();
    }
}
