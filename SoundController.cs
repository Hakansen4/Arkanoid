using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip startGame,RoundLose,enlargeSound,BNB,RB,YGB,laserSound,GameOver,BreakedS,DOHhit,DOHdead;
    public void GameStartingSound()
    {
        if(this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if(rankCheck(startGame.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = startGame;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = startGame;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void GameOverSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(GameOver.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = GameOver;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if(GetComponent<AudioSource>().clip != GameOver)
            {
                this.gameObject.GetComponent<AudioSource>().clip = GameOver;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
    public void DOHdeadSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(DOHdead.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = DOHdead;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = DOHdead;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void DOHhitSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(DOHhit.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = DOHhit;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = DOHhit;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void RoundLoseSound()
    {
        if(this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
           if(rankCheck(RoundLose.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = RoundLose;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = RoundLose;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void EnlargeSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if(rankCheck(enlargeSound.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = enlargeSound;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = enlargeSound;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void BallNormalBlockSound()
    {
        if(this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if(rankCheck(BNB.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = BNB;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = BNB;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void RacketBallSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(RB.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = RB;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = RB;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void YellowGreyBlockSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(YGB.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = YGB;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = YGB;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void LaserSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(laserSound.ToString()))
            {
                this.gameObject.GetComponent<AudioSource>().clip = laserSound;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = laserSound;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void BreakedSound()
    {
        if (this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (rankCheck(BreakedS.ToString()))
            {
                Debug.Log("AADASDAS");
                this.gameObject.GetComponent<AudioSource>().clip = BreakedS;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = BreakedS;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    private bool rankCheck(string SoundName)
    {
        //get current sound fo rank
        AudioClip[] Sounds = { startGame, RoundLose, enlargeSound, BNB, RB, YGB, laserSound,BreakedS};
        string CurrentSound="";
        for(int i=0;i<Sounds.Length;i++)
        {
            if(GetComponent<AudioSource>().clip == Sounds[i])
            {
                CurrentSound = Sounds[i].ToString();
                break;
            }
            else
            {
                CurrentSound = "";
            }
        }
        //rank 0 = game start/Over;rank 1 = Pover Up; rank 2 = laser; rank 3 = ball coll;
        int Rank = 0,rankCurrentClip = 0;
        if (SoundName == startGame.ToString() || SoundName == RoundLose.ToString()  ||  SoundName == GameOver.ToString()    
            ||  SoundName == BreakedS.ToString()    ||  SoundName==DOHdead.ToString())
            Rank = 0;
        else if (SoundName == enlargeSound.ToString())
            Rank = 1;
        else if (SoundName == laserSound.ToString())
            Rank = 2;
        else if (SoundName == BNB.ToString() || SoundName == RB.ToString() || SoundName == YGB.ToString()   ||  SoundName==DOHhit.ToString())
            Rank = 3;
        /////////////////////////////////////////////////////////////////////// we ranked next sound
        if (CurrentSound == startGame.ToString() || CurrentSound == RoundLose.ToString()    ||  SoundName == GameOver.ToString()    
            ||  SoundName == BreakedS.ToString()    ||  SoundName==DOHdead.ToString())
            rankCurrentClip = 0;
        else if (CurrentSound == enlargeSound.ToString())
            rankCurrentClip = 1;
        else if (CurrentSound == laserSound.ToString())
            rankCurrentClip = 2;
        else if (CurrentSound == BNB.ToString() || CurrentSound == RB.ToString() || CurrentSound == YGB.ToString()  || SoundName == DOHhit.ToString())
            rankCurrentClip = 3;
        ///////////////////////////////////////////////////////////////////////// we ranked current sound
        if (SoundName == startGame.ToString()   ||  SoundName == GameOver.ToString())
        {
            if (Rank < rankCurrentClip)
                return true;
            else
                return false;
        }
        else if (Rank <= rankCurrentClip)        
            return true;        
        else
            return false;
    }
}