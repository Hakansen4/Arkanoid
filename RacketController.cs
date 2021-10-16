using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    Animator animator;
    static float baseScaleX = 1, baseScaleY = 0.7875f;
    public Sprite ClassicSprite, LaserSprite;
    public float Speed = 200f;
    public bool isLazerMode = false, isEnlargeMode = false, isCatchMode = false,
        isDistruptorMode = false, isGetBreakBill = false, isSlowMode = false, isSlowed = false,
        isGetHeal = false, isDeleteBalls = false, isBreaked = false, checkSpeed = false,
        isClearBreak = false, isStopSlow = false, isGoNext = false, isDead = false;
    //----------------------------------------------------------------------------------------------------------
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Break")
        {
            BallSpeedChecker();
            isBreaked = true;
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().BreakedSound();
            animator.applyRootMotion = false;
            animator.SetTrigger("Breaking");
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (collision.gameObject.tag == "L")
        {
            BallSpeedChecker();
            ResetRacket();
            animator.SetBool("LaserMode", true);
            isLazerMode = true;
        }
        else if (collision.gameObject.tag == "E")
        {
            BallSpeedChecker();
            ResetRacket();
            isEnlargeMode = true;
        }
        else if (collision.gameObject.tag == "C")
        {
            BallSpeedChecker();
            ResetRacket();
            isCatchMode = true;
        }
        else if (collision.gameObject.tag == "D")
        {
            BallSpeedChecker();
            isDistruptorMode = true;
            ResetRacket();
        }
        else if (collision.gameObject.tag == "B")
        {
            BallSpeedChecker();
            ResetRacket();
            isGetBreakBill = true;
        }
        else if (collision.gameObject.tag == "S")
        {
            ResetRacket();
            isSlowed = true;
            isSlowMode = true;
        }
        else if (collision.gameObject.tag == "P")
        {
            BallSpeedChecker();
            ResetRacket();
            isGetHeal = true;
        }
        else if (collision.gameObject.tag == "BreakBorder")
        {
            isGoNext = true;
        }
        else if (collision.gameObject.tag == "Diamond")
        {
            isDead = true;
        }
    }
    public void ResetRacket()
    {
        this.transform.localScale = new Vector3(baseScaleX, baseScaleY, 1);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ClassicSprite;
        animator.SetBool("LaserMode", false);
        isLazerMode = false;isEnlargeMode = false;isCatchMode = false;
        if (!isDistruptorMode)
            isDeleteBalls = true;
    }
    private void BallSpeedChecker()
    {
        if(isSlowMode)
        {
            checkSpeed = true;
            isSlowMode = false;
        }
    }
}