using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NormalMissile : MonoBehaviour {


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
            Manager.Instance.SFX(3);
            gameObject.SetActive(false);
        }
    }

}
