using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Speed = 100f;
    public float NormalSpeed = 100f;
    private GameController GC;
    public bool isCatched = false;
    bool isFirstBall = false, isSecondBall = false, isThirdBall = false;
    RacketController racket;
    //---------------------------------------------------------------------------------------
    private void Awake()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        RegisterBall();
    }
    private void Start()
    {
        racket = GameObject.FindGameObjectWithTag("Racket").GetComponent<RacketController>();
        CheckBallStatue();
        StartMove();
    }
    void RegisterBall()
    {
        for(int i=0;i<3;i++)
        {
            if(GC.Balls[i]==null)
            {
                GC.Balls[i] = this.gameObject;
                break;
            }
        }
    }
    void CheckBallStatue()
    {
        if (GC.Balls[0] == this.gameObject)
            isFirstBall = true;
        else if (GC.Balls[1] == this.gameObject)
            isSecondBall = true;
        else if (GC.Balls[2] == this.gameObject)
            isThirdBall = true;
    }
    void StartMove()
    {
        if (isFirstBall)
            GetComponent<Rigidbody2D>().velocity = Vector2.up * Speed;
        else if (isSecondBall)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1) * Speed;
        else if (isThirdBall)
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1) * Speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Racket"))
        {
            if (!racket.isCatchMode)
            {
                Vector2 Dir;
                float HitLoc = HitFactor(this.gameObject.transform, collision.transform, collision.collider.bounds.size.x) * 2;
                if (HitLoc > 0.75f)
                    Dir = new Vector2(1, 1);
                else if (HitLoc < -0.75f)
                    Dir = new Vector2(-1, 1);
                else if (HitLoc > 1)
                    Dir = new Vector2(1, 0.5f);
                else if (HitLoc < -1)
                    Dir = new Vector2(-1, 0.5f);
                else
                    Dir = new Vector2(HitLoc, 1);
                GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().RacketBallSound();
                GetComponent<Rigidbody2D>().velocity = Dir * Speed;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isCatched = true;
            }
        }
        else if(collision.gameObject.CompareTag("BackWall"))
        {
            GC.GetComponent<GameController>().canSpeed = true;
        }
        else if(collision.gameObject.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
        //Duvarlara carpma da speed ve velocity arttirimi
            if(gameObject.GetComponent<Rigidbody2D>().velocity.y>-300   && gameObject.GetComponent<Rigidbody2D>().velocity.y < 300
                &&  GC.canSpeed)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity *= 1.02f;
                Speed *= 1.008f;
                if (!GC.getisSlowed())
                    NormalSpeed *= 1.02f;
            }
    }
    public float HitFactor(Transform Ball,Transform Racket,float RacketSize)
    {
        //-1  -0.5  0  0.5   1  <- x value depending on where it was hit
        //===================  <- this is the racket
        return (Ball.position.x - Racket.position.x) / RacketSize;
    }
}