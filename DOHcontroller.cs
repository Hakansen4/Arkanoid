using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOHcontroller : MonoBehaviour
{
    private int health = 15;
    private SpriteRenderer spriteRenderer;
    public bool isDead = false;
    public bool onFire = true;
    public float FireCooldown;
    public Sprite Classic, whenFire, Hitoff,Hitonn, Dead;
    public GameObject Bullet;
    public Transform BulletPoss;
    public GameObject[] diamonds = new GameObject[5];
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(reload());
    }
    private void Update()
    {
        if (!onFire &&  !isDead)
            StartCoroutine(Fire());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().DohHitPoint();
            health--;
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().DOHhitSound();
            StartCoroutine(hitAnim());
            checkDead();
        }
    }
    private void checkDead()
    {
        if(health<=0)
        {
            StopAllCoroutines();
            spriteRenderer.sprite = Dead;
            GameObject.FindGameObjectWithTag("AudioCT").GetComponent<SoundController>().DOHdeadSound();
            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isDead = true;
        }
    }
    public void DeleteDiamonds()
    {
        for(int i=0;i<diamonds.Length;i++)
        {
            if (diamonds[i] != null)
                Destroy(diamonds[i]);
        }
    }
    private IEnumerator hitAnim()
    {
        if (onFire)
            spriteRenderer.sprite = Hitonn;
        else
            spriteRenderer.sprite = Hitoff;
        yield return new WaitForSeconds(0.1f);
        if (onFire)
            spriteRenderer.sprite = whenFire;
        else
            spriteRenderer.sprite = Classic;
    }
    private IEnumerator Fire()
    {
        onFire = true;
        spriteRenderer.sprite = whenFire;
        Instantiate(Bullet,BulletPoss.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Bullet, BulletPoss.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Bullet, BulletPoss.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Bullet, BulletPoss.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Bullet, BulletPoss.position, Quaternion.identity);
        spriteRenderer.sprite = Classic;
        StartCoroutine(reload());
    }
    public IEnumerator reload()
    {
        spriteRenderer.sprite = Classic;
        yield return new WaitForSeconds(FireCooldown);
        onFire = false;
    }
}