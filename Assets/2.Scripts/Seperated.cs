using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperated : MonoBehaviour {

    Character character;
    Vector2 dir;
    Rigidbody2D rb;
    bool fly = false;

    public AudioClip SeperatedAudio;
    public AudioClip ExplosionAudio;
    public AudioSource audio;

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
            character.GameOver();
        }
        else if ((col.tag == "SeperateMissile" || col.tag == "SeperatedMissile") && fly)
        {
            //폭발 둘다 모션 두번 폭발음
            StartCoroutine(Explosion());
        }
        else
        {
            StartCoroutine(Explosion());
        }
    }



    public void SeperateLaunch(int i, Vector2 target)
    {
        rb = GetComponent<Rigidbody2D>();
        dir = target.normalized;

        //StartCoroutine(Flight());
        StartCoroutine(flyCheck());

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

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        audio.PlayOneShot(SeperatedAudio);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator Flight()
    {
        while(true)
        {

            yield return null;
        }
    }

    IEnumerator flyCheck()
    {
        yield return new WaitForSeconds(0.5f);
        fly = true;
    }
}
