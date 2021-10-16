using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private int currentHealth = 1;
    public int MaxHealth;
    public GameObject FirstHealth, SecondHealth, ThirdHealth, FourthHealth;
    public Sprite Health;
    //----------------------------------------------------------------------------
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("HC").Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        currentHealth = 0;
    }
    public void AddHealth()
    {
        currentHealth++;
        if(currentHealth == 1)
        {
            SecondHealth.GetComponent<SpriteRenderer>().sprite = Health;
        }
        else if (currentHealth == 2)
        {
            ThirdHealth.GetComponent<SpriteRenderer>().sprite = Health;
        }
        else if (currentHealth == 3)
        {
            FourthHealth.GetComponent<SpriteRenderer>().sprite = Health;
        }
    }
    public void incraseHealth()
    {
        currentHealth--;
        if(currentHealth == 2)
        {
            FourthHealth.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (currentHealth == 1)
        {
            ThirdHealth.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (currentHealth == 0)
        {
            SecondHealth.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (currentHealth == -1)
        {
            FirstHealth.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    public int GetCurrentHeal()
    {
        return currentHealth;
    }    
}
