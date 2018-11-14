using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperated : MonoBehaviour {

    Character character;
    Vector2 dir;
    Rigidbody2D rb;


    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Manager.Instance.GameOver();
        }
        else
        {
            //폭발 둘다 모션 두번 폭발음
            Manager.Instance.SFX(1);
            gameObject.SetActive(false);
        }
    }



    public void SeperateLaunch(int i, Vector2 target)
    {
        dir = target.normalized;

        switch (i)
        {
            case 1:
                transform.Rotate(0, 0, -20);
                break;
            case 2:
                transform.Rotate(0, 0, -10);
                break;
            case 3:
                transform.Rotate(0, 0, 10);
                break;
            case 4:
                transform.Rotate(0, 0, 20);
                break;
        }

        rb.velocity = transform.up * speed;
    }


    IEnumerator Flight()
    {
        yield return new WaitForSeconds(5);
        Manager.Instance.SFX(1);
        gameObject.SetActive(false);
    }
}
