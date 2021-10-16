using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Racket, Ball,Laser,Break;
    float timer = 0;
    RacketController racketControl;BallController ballControl;
    public Transform LeftLaserTransform, RightLaserTransform;
    public TextMeshProUGUI Round, Ready,HighScore,Score,RoundNumber;
    private GameObject MainBall;
    public GameObject HealtC;
    public GameObject[] bills;
    private float h = 0;
    int BillCooldown = 1;int BillExRandom = 9;//9 default number.
    float enlargeSpeed = 35f;
    public float Point;
    bool RestartIE = false;
    private bool getbreakPoint = true;
    private bool DistruperTwice = false, SlowTwice = false, isFinalScene = false;
    public bool canSpeed = false;public bool isDistruped = false, isGameStarted = false, GameReloading = false, GameOver = false;
    public GameObject[] Balls = new GameObject[3];
    public GameObject[] blocks;
    public GameObject GameOverScreen;
    //----------------------------------------------------------------------------
    void Awake()
    {
        PlayerPrefs.SetFloat("Score", 0);
        PlayerPrefs.SetFloat("HighScore", 0);
        if (SceneManager.GetActiveScene().name == "Final")
            isFinalScene = true;
        Score.text = PlayerPrefs.GetFloat("Score").ToString();
        HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
        Point = PlayerPrefs.GetFloat("Score");
        RoundNumber.text = SceneManager.GetActiveScene().name;
        Round.text = "Round     " + SceneManager.GetActiveScene().name;
        racketControl = Racket.GetComponent<RacketController>();
        ballControl = Ball.GetComponent<BallController>();
        blocks = new GameObject[HowManyBlock()];
        HealtC = GameObject.FindGameObjectWithTag("HC");
    }
    void Update()
    {
        if (!isGameStarted)
            startingGame(); 
        else if(isGameStarted)
        {
            SetMainBall();
            PoverUpControl();           
            for (int i = 0; i < Balls.Length; i++)
            {
                if (Balls[i] != null && Balls[i].GetComponent<BallController>().isCatched)
                    ThrowBall(Balls[i]);
            }
            if (CheckBallsDead()  &&  !GameReloading  &&  !GameOver)
            {
                if(isFinalScene)
                {
                    GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().onFire = true;
                    GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().DeleteDiamonds();
                    GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().StopAllCoroutines();
                }
                HealtC.GetComponent<HealthController>().incraseHealth();
                Racket.GetComponent<Animator>().SetTrigger("Dead");
                if (HealtC.GetComponent<HealthController>().GetCurrentHeal() < 0)
                    GameOver = true;
                else
                {
                    GameReloading = true;
                    if (isFinalScene)
                        GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().StartCoroutine("reload");
                }
            }
            else if(racketControl.isDead)
            {
                GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().onFire = true;
                GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().DeleteDiamonds();
                GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().StopAllCoroutines();
                Destroy(MainBall);
                HealtC.GetComponent<HealthController>().incraseHealth();
                Racket.GetComponent<Animator>().SetTrigger("Dead");
                if (HealtC.GetComponent<HealthController>().GetCurrentHeal() < 0)
                    GameOver = true;
                else
                { 
                    GameReloading = true;
                    GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().StartCoroutine("reload");
                } 
                racketControl.isDead = false;
            }
            if (Racket.GetComponent<RacketController>().isDeleteBalls)
                Deleteballs();
            if (Racket.GetComponent<RacketController>().checkSpeed)
            {
                CheckSpeed();
            }
            if (isDistruped)
                SetBallSpeedsAfterDistrup();
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted)
            RacketMove();
        if (GameReloading)
        {
            RestartIE = false;
            restartLevel();
            GameReloading = false;
        }
        if (GameOver)
            StartCoroutine(Game_Over());
        //////////////////////////////////////////////
        if (isGamePassed()  &&  !isFinalScene)
        {
            if (Racket.GetComponent<RacketController>().isGoNext)
                Invoke("GoNextRound", 2.0f);
            else
                GoNextRound();
        }
        if(isFinalScene)
        {
            if(GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>().isDead)
            {
                StartCoroutine("FinalScenePass");
            }
        }
        ///////////////////////////////////////////////
        if (racketControl.isBreaked &&  getbreakPoint)
        {
            Point += 10000;
            PlayerPrefs.SetFloat("Score", Point);
            Score.text = Point.ToString();
            if (int.Parse(HighScore.text) < Point)
            {
                HighScore.text = Point.ToString();
                PlayerPrefs.SetFloat("HighScore", Point);
            }
            for(int i=0;i<Balls.Length;i++)
            {
                if (Balls[i] != null)
                    Balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            racketControl.isBreaked = false;
            getbreakPoint = false;
        }
    }
    private void startingGame()
    {
        timer = Time.timeSinceLevelLoad;
        if (0.5f <timer && timer < 1)
        { 
            Round.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().GameStartingSound();
        }
        else if (timer < 1.5f && timer > 1)
            Ready.gameObject.SetActive(true);
        else if(timer>2f &&  timer<3)
        {
            Round.gameObject.SetActive(false);
            Ready.gameObject.SetActive(false);
            isGameStarted = true;
            Instantiate(Ball, new Vector3(35.55f, -85.41f, 1), Quaternion.identity);
            Balls[0].GetComponent<BallController>().isCatched = true;
        }
    }
    private void restartLevel()
    {
        if (!RestartIE)
            StartCoroutine(RestartLvl());
    }
    IEnumerator RestartLvl()
    {
        //Time.timeScale = 0;
        Round.gameObject.SetActive(true);
        Ready.gameObject.SetActive(true);
        Racket.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isGameStarted = false;
        RestartIE = true;
        DistruperTwice = false;
        SlowTwice = false;
        Racket.GetComponent<RacketController>().ResetRacket();
        GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().RoundLoseSound();
        yield return new WaitForSeconds(3);
        //Time.timeScale = 1;
        Racket.transform.position = new Vector3(36, -91.8f, 90);
        Round.gameObject.SetActive(false);
        Ready.gameObject.SetActive(false);
        isGameStarted = true;
        Instantiate(Ball, new Vector3(35.55f, -85.41f, 1), Quaternion.identity);
        Balls[0].GetComponent<BallController>().isCatched = true;
        GameReloading = false;
    }
    IEnumerator Game_Over()
    {
        Racket.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().GameOverSound();
        yield return new WaitForSeconds(3);
        GameOverScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        GameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text="";
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("MM");
    }
    private void RacketMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (Racket.transform.position.x < -53.1f)
        {
            if (h == -1)
            {
                h = 0;
            }
        }
        else if (Racket.transform.position.x > 120.8f)
        {
            if (h == 1)
                h = 0;
        }
        Racket.GetComponent<Rigidbody2D>().velocity = Vector2.right * h * racketControl.Speed;
        for (int i = 0; i < Balls.Length; i++)
        {
            if (Balls[i] != null)
            {
                if (Balls[i].GetComponent<BallController>().isCatched)
                    Balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.right * h * racketControl.Speed;
            }
        }
        /////////////////////////////////////Mobile Movement//////////////////
    }
    private void SetMainBall()
    {
        if (Balls[0] != null)
            MainBall = Balls[0];
        else if (Balls[1] != null)
            MainBall = Balls[1];
        else if (Balls[2] != null)
            MainBall = Balls[2];
    }
    void PoverUpControl()
    {
        if(racketControl.isLazerMode)
        {
            LaserShoot();
            Racket.GetComponent<SpriteRenderer>().sprite = racketControl.LaserSprite;
        }
        else if(racketControl.isEnlargeMode)
        {
            if (Racket.transform.localScale.x < 2)
            {
                Racket.transform.localScale += new Vector3(0.2f, 0, 0) * Time.deltaTime * enlargeSpeed;
                GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().EnlargeSound(); 
            }
        }
        else if(racketControl.isDistruptorMode)
        {
            MainBall.GetComponent<Rigidbody2D>().velocity = Vector2.up * ballControl.Speed;
            Instantiate(Ball, MainBall.transform.position, Quaternion.identity);
            Instantiate(Ball, MainBall.transform.position, Quaternion.identity);
            racketControl.isDistruptorMode = false;
            isDistruped = true;
        }
        else if(racketControl.isGetBreakBill)
        {
            PowerBreak();
        }
        else if(racketControl.isSlowMode)
        {
            slowBall();
        }
        else if(racketControl.isGetHeal)
        {
            GameObject.FindGameObjectWithTag("HC").GetComponent<HealthController>().AddHealth();
            racketControl.isGetHeal = false;
        }
    }
    void LaserShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().LaserSound();
            Instantiate(Laser, LeftLaserTransform.position, Quaternion.identity);
            Instantiate(Laser, RightLaserTransform.position, Quaternion.identity);
        }
    }
    void ThrowBall(GameObject Ball)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 Dir;
            float HitLoc = Ball.GetComponent<BallController>().HitFactor(Ball.transform, Racket.transform, Racket.GetComponent<Collider2D>().bounds.size.x) * 2;
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
            Ball.GetComponent<Rigidbody2D>().velocity = Dir * ballControl.Speed;
            Ball.GetComponent<BallController>().isCatched = false;
        }
    }
    public void InstantiateBills(Transform Position)
    {
        BillCooldown--;
        if (BillCooldown <= 0 && billInstanitateChange())
        {
            int number = RandomBill();
            Instantiate(bills[number], Position.position, Quaternion.identity);
            BillCooldown = 4;
            BillExRandom = number;
        }
    }
    bool billInstanitateChange()
    {
        int rndm = Random.Range(0, 8);
        if (rndm == 0)
            return true;
        else
            return false;
    }
    int RandomBill()
    {
        if (BillExRandom == 9)
        {
            int random1 = Random.Range(0, bills.Length);
            return random1;
        }
        else if(BillExRandom == 0   &&  !DistruperTwice)
        {
            int random = Random.Range(0, bills.Length);
            if (random == BillExRandom)
                DistruperTwice = true;
            return random;
        }
        else if(BillExRandom == 6   &&  !SlowTwice)
        {
            int random = Random.Range(0, bills.Length);
            if (random == BillExRandom)
                SlowTwice = true;
            return random;
        }
        else
        {
            int random2 = BillExRandom;
            while(BillExRandom==random2)
            {
                random2 = Random.Range(0, bills.Length);
            }
            DistruperTwice = false;
            SlowTwice = false;
            return random2;
        }
    }
    private void PowerBreak()
    {
        Instantiate(Break, new Vector3(140.36f, -88.6f, 1), Quaternion.identity);
        racketControl.isGetBreakBill = false;
    }
    private void slowBall()
    {
        for(int i=0;i<Balls.Length;i++)
        {
            if (Balls[i] != null    &&  racketControl.isSlowed)
            {
                Balls[i].GetComponent<Rigidbody2D>().velocity = Balls[i].GetComponent<Rigidbody2D>().velocity / 2;
                Balls[i].GetComponent<BallController>().Speed /= 2;
                racketControl.isSlowed = false;
            }
        }
    }
    private void Deleteballs()
    {
        for(int i=0;i<Balls.Length;i++)
        {
            if(Balls[i]!=null)
            {
                if (Balls[i] != MainBall)
                    Destroy(Balls[i]);
            }
        }
        Racket.GetComponent<RacketController>().isDeleteBalls = false;
    }
    public bool getisSlowed()
    {
        return Racket.GetComponent<RacketController>().isSlowMode;
    }
    private void CheckSpeed()
    {
        if(MainBall.GetComponent<BallController>().Speed    <=   MainBall.GetComponent<BallController>().NormalSpeed)
        {
            Vector2 Yon = MainBall.GetComponent<Rigidbody2D>().velocity.normalized;
            MainBall.GetComponent<BallController>().Speed = MainBall.GetComponent<BallController>().NormalSpeed;
            MainBall.GetComponent<Rigidbody2D>().velocity = MainBall.GetComponent<BallController>().NormalSpeed * Yon;
        }
        Racket.GetComponent<RacketController>().checkSpeed = false;
    }
    private void SetBallSpeedsAfterDistrup()
    {
        for(int i=0;i<Balls.Length;i++)
        {
            if (Balls[i] != null)
                Balls[i].GetComponent<BallController>().Speed = MainBall.GetComponent<BallController>().Speed;
        }
        isDistruped = false;
    }
    private int HowManyBlock()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length;
    }
    public bool isGamePassed()
    {
        if (racketControl.isGoNext)
            return true;
        for(int i=0;i<blocks.Length;i++)
        {
            if (blocks[i] != null)
                return false;
        }
        return true;
    }
    private bool CheckBallsDead()
    {
        for(int i=0;i<Balls.Length;i++)
        {
            if (Balls[i] != null)
                return false;
        }
        return true;
    }
    private void GoNextRound()
    {
        if (SceneManager.GetActiveScene().name==32.ToString())
        {
            SceneManager.LoadScene("Final");
        }
        else
        {
            int nextScene = int.Parse(SceneManager.GetActiveScene().name);
            nextScene++;
            SceneManager.LoadScene(nextScene.ToString());
        }
    }  
    public void DohHitPoint()
    {
        Point += 1000;
        PlayerPrefs.SetFloat("Score", Point);
        Score.text = Point.ToString();
        if(PlayerPrefs.GetFloat("Score")>PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", Point);
            HighScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
        }
    }
    private IEnumerator FinalScenePass()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("EndScene");
    }

    //Mobile Controller/////////////////////////////////////////////////////
    //Movement
    public void MobileRacketMoveR()
    {
        h = 1;
    }
    public void MobileRacketMoveL()
    {
        h = -1;
    }
    public void MobileRacketMoveS()
    {
        h = 0;
    }
    //Laser Shoot and Throw Ball
    public void MobilelaserShoot()
    {
        if(racketControl.isLazerMode)
        {
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().LaserSound();
            Instantiate(Laser, LeftLaserTransform.position, Quaternion.identity);
            Instantiate(Laser, RightLaserTransform.position, Quaternion.identity);
        }
    }
    public void MobileThrowBall()
    {
        for(int i=0;i<3;i++)
        {
            if (Balls[i] != null && Balls[i].GetComponent<BallController>().isCatched)
            {
                Vector2 Dir;
                float HitLoc = Balls[i].GetComponent<BallController>().HitFactor(Balls[i].transform, Racket.transform, Racket.GetComponent<Collider2D>().bounds.size.x) * 2;
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
                Balls[i].GetComponent<Rigidbody2D>().velocity = Dir * ballControl.Speed;
                Balls[i].GetComponent<BallController>().isCatched = false;
            }
        }
    }
}