using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health = 16;
    public GameObject[] hearts;

    private void Update()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if((i+1) * 2 > health)
            {
                if((i + 1) * 2 - health == 1)
                {
                    hearts[i].GetComponent<Image>().fillAmount = .5f;
                }
                else
                {
                    hearts[i].GetComponent<Image>().fillAmount = 0;
                }
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
