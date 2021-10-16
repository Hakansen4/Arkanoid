using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndSceneText : MonoBehaviour
{
    public TextMeshProUGUI Score, HighScore, MainText,GoMenu;
    public AudioClip typeSound;
    public AudioSource audioS;
    public GameObject bButton;
    private bool goMenuActive = false;
    [Multiline]
    public string Maintxt;
    void Start()
    {
        Score.text = PlayerPrefs.GetFloat("Score").ToString();
        HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
        StartCoroutine("WriteMaintxt");
    }
    private void Update()
    {
        if(goMenuActive)
        {
            if(Input.GetKeyDown(KeyCode.Space)  ||  Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MM");
            }
        }
    }
    private IEnumerator WriteMaintxt()
    {
        foreach(char i in Maintxt)
        {
            MainText.text += i.ToString();
            audioS.pitch = Random.Range(0.8f, 1.2f);
            audioS.PlayOneShot(typeSound);
            if (i.ToString() == ".")
                yield return new WaitForSeconds(1);
            else
                yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(menuBT());
    }
    private IEnumerator menuBT()
    {
        yield return new WaitForSeconds(3);
        GoMenu.gameObject.SetActive(true);
        bButton.gameObject.SetActive(true);
        goMenuActive = true;
    }
    public void MobileGoMenu()
    {
        if (goMenuActive)
            SceneManager.LoadScene("MM");

    }
}
