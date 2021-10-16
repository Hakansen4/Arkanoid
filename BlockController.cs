using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
    int health;
    private GameController GC;
    //-----------------------------------------------------------------------------------------
    private void Awake()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().sprite.name =="YellowBrick")
        {
            health = -1;
        }
        else if(this.gameObject.GetComponent<SpriteRenderer>().sprite.name=="GreyBrick1")
        {
            if (int.Parse(SceneManager.GetActiveScene().name.ToString()) < 11)
                health = 2;
            else if (int.Parse(SceneManager.GetActiveScene().name.ToString()) < 17)
                health = 3;
            else if (int.Parse(SceneManager.GetActiveScene().name.ToString()) < 25)
                health = 4;
            else
                health = 5;
        }
        else
        {
            health = 1;
        }
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void Start()
    {
        for(int i= 0;i<=GC.blocks.Length;i++)
        {
            if (GC.blocks[i] == null)
            {
                if (this.gameObject.GetComponent<SpriteRenderer>().name == "YellowBrick")
                    break;
                GC.blocks[i] = this.gameObject;
                break;
            }
        }
    }
    public void GetScore()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "WhiteBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 50);
            GC.Point += 50;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if(PlayerPrefs.GetFloat("Score")>PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "BlueBrick1")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 110);
            GC.Point += 110;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "OrangeBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 60);
            GC.Point += 60;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "GreenBrick1")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 90);
            GC.Point += 90;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "RedBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 100);
            GC.Point += 100;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "WhiteBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 50);
            GC.Point += 50;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "PinkBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 120);
            GC.Point += 120;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "BlueBrick0")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 70);
            GC.Point += 70;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "BrownBrick")
        {
            PlayerPrefs.SetFloat("Score", GC.Point + 50);
            GC.Point += 50;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "GreyBrick1")
        {
            int puan = int.Parse(SceneManager.GetActiveScene().name) * 50; 
            PlayerPrefs.SetFloat("Score", GC.Point + puan);
            GC.Point += puan;
            GC.Score.text = PlayerPrefs.GetFloat("Score").ToString();
            if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
                GC.HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
            }
        }
    }
    private void Sound()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "YellowBrick" ||
            this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "GreyBrick1")
        {
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().YellowGreyBlockSound();
        }
        else
        {
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().BallNormalBlockSound();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sound();
        health--;
        if (health == 0)
        {
            GC.InstantiateBills(this.transform);
            GetScore();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
        if (health == 0)
        {
            GC.InstantiateBills(this.transform);
            GetScore();
            Destroy(gameObject);
        }
    }
}
