using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private Vector3 RacketPoss;
    public float speed;
    private DOHcontroller DOHct;
    private void Awake()
    {
        DOHct = GameObject.FindGameObjectWithTag("DOH").GetComponent<DOHcontroller>();
    }
    private void Start()
    {
        registerDiamond();
        RacketPoss = GameObject.FindGameObjectWithTag("Racket").GetComponent<Transform>().position;
        RacketPoss = new Vector3(RacketPoss.x, RacketPoss.y - 30, 0);
    }
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1));
        movetoRacket();
        DestroyGameobject();
    }
    private void movetoRacket()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, RacketPoss, speed * Time.deltaTime);
    }
    private void DestroyGameobject()
    {
        if (gameObject.transform.position == RacketPoss)
            Destroy(gameObject);
    }
    private void registerDiamond()
    {
        for(int i=0;i<DOHct.diamonds.Length;i++)
        {
            if(DOHct.diamonds[i]==null)
            {
                DOHct.diamonds[i] = this.gameObject;
                break;
            }
        }
    }
}