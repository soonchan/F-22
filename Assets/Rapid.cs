using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapid : MonoBehaviour {

    Vector2 target;
    Rigidbody2D rb;
    Character character;
    GameObject targetG;

    public AudioClip[] ExplosionAudio;
    public AudioSource audio;

    public float speed;

    void Start () {

        rb = GetComponent<Rigidbody2D>();

        targetG = GameObject.FindWithTag("Player");
        character = targetG.GetComponent<Character>();

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
            character.GameOver();
        }
        else
        {
            //폭발 둘다 모션 두번 폭발음
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        audio.PlayOneShot(ExplosionAudio[Random.Range(0, 2)]);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
