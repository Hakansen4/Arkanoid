using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMcontroller : MonoBehaviour
{
    private bool Firstpos,Secpos;
    private float AnimTimer = 0;
    public RectTransform Cursor;
    public TextMeshProUGUI HS, S;
    public GameObject PlayBt, QuitBt;
    private void Awake()
    {
        scoreControl();
        Firstpos = true;
        Secpos = false;
    }
    private void Update()
    {
        MoveCursor();
        Onclick();
        checkAnim();
    }
    private void MoveCursor()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Firstpos)
            {
                Cursor.position = new Vector3(Cursor.position.x, QuitBt.transform.position.y, 0);
                Firstpos = false;
                Secpos = true;
                PlayBt.gameObject.SetActive(true);
            }
            else if(Secpos)
            {
                Cursor.position = new Vector3(Cursor.position.x, PlayBt.transform.position.y, 0);
                Firstpos = true;
                Secpos = false;
                QuitBt.gameObject.SetActive(true);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(Firstpos)
            {
                Cursor.position = new Vector3(Cursor.position.x, QuitBt.transform.position.y, 0);
                Firstpos = false;
                Secpos = true;
                PlayBt.gameObject.SetActive(true);
            }
            else if(Secpos)
            {
                Cursor.position = new Vector3(Cursor.position.x, PlayBt.transform.position.y, 0);
                Firstpos = true;
                Secpos = false;
                QuitBt.gameObject.SetActive(true);
            }
        }
    }
    private void Onclick()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if(Firstpos)
            {
                PlayerPrefs.SetFloat("Score", 0);
                SceneManager.LoadScene("1");
            }
            else if(Secpos)
            {
                Application.Quit();
            }
        }
    }
    private void scoreControl()
    {
        HS.text = PlayerPrefs.GetFloat("HighScore").ToString();
        S.text = PlayerPrefs.GetFloat("Score").ToString();
    }
    private void checkAnim()
    {
        if (Time.time - AnimTimer > 0.4f)
            ChooseAnim();
    }
    private void ChooseAnim()
    {
        AnimTimer = Time.time;
        if (Cursor.gameObject.active)
        {
            Cursor.gameObject.SetActive(false);
            if (Firstpos)
                PlayBt.gameObject.SetActive(false);
            else if (Secpos)
                QuitBt.gameObject.SetActive(false);
        }
        else if (!Cursor.gameObject.active)
        {
            Cursor.gameObject.SetActive(true);
            if (Firstpos)
                PlayBt.gameObject.SetActive(true);
            else if (Secpos)
                QuitBt.gameObject.SetActive(true);
        }
    }
    //Mobile///////////////////////////////////////////////
    public void MobileMoveDownButton()
    {
        if (Firstpos)
        {
            Cursor.position = new Vector3(Cursor.position.x, QuitBt.transform.position.y, 0);
            Firstpos = false;
            Secpos = true;
            PlayBt.gameObject.SetActive(true);
        }
        else if (Secpos)
        {
            Cursor.position = new Vector3(Cursor.position.x, PlayBt.transform.position.y, 0);
            Firstpos = true;
            Secpos = false;
            QuitBt.gameObject.SetActive(true);
        }
    }
    public void MobileMoveUpButton()
    {
        if (Firstpos)
        {
            Cursor.position = new Vector3(Cursor.position.x, QuitBt.transform.position.y, 0);
            Firstpos = false;
            Secpos = true;
            PlayBt.gameObject.SetActive(true);
        }
        else if (Secpos)
        {
            Cursor.position = new Vector3(Cursor.position.x, PlayBt.transform.position.y, 0);
            Firstpos = true;
            Secpos = false;
            QuitBt.gameObject.SetActive(true);
        }
    }
    public void MobileGoButton()
    {
        if (Firstpos)
        {
            PlayerPrefs.SetFloat("Score", 0);
            SceneManager.LoadScene("1");
        }
        else if (Secpos)
        {
            Application.Quit();
        }
    }
}
