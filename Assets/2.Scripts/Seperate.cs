using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperate : MonoBehaviour {


    GameObject targetG;
    Vector2 target;
    public float speed;
    public float rotateSpeed;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetG = Character.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        target = targetG.transform.position;
        Vector2 dir = target - rb.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;

        //플레이어와 근접시 분리
        if (Vector2.Distance((Vector2)transform.position, target) < 2f)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Manager.Instance.SFX(0);
            for (int i = 1; i < 5; i++)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
                transform.parent.GetChild(i).GetComponent<Seperated>().SeperateLaunch(i, target);
            }
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.tag == "Player")
    //    {
    //        Manager.Instance.GameOver();
    //    }
    //    else
    //    {
    //        //폭발 FX
    //        Manager.Instance.SFX(3);
    //        gameObject.SetActive(false);
    //    }
    //}

}
