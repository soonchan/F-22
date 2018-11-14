using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapid : MonoBehaviour {

    Vector2 target;
    Rigidbody2D rb;
    GameObject targetG;

    public float speed;

    void Start () {

        rb = GetComponent<Rigidbody2D>();

        targetG = Character.Instance.gameObject;

        target = targetG.transform.position;
        Vector2 dir = target - rb.position;
        dir.Normalize();

        //
        Vector2 v = dir - (Vector2)transform.up;
        float rotate = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        //

        transform.Rotate(0, 0, rotate);
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
