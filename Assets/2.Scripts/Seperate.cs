using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperate : MonoBehaviour {

    Character character;

    GameObject targetG;
    Vector2 target;
    public float speed;
    public float rotateSpeed;
    bool first = false;
    public AudioClip[] ExplosionAudio;
    public AudioClip SeperateAudio;
    public AudioSource audio;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetG = GameObject.FindWithTag("Player");
        character = targetG.GetComponent<Character>();
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
        if (Vector2.Distance((Vector2)transform.position, target) < 1f && !first)
        {
            audio.PlayOneShot(SeperateAudio);
            for(int i = 1; i < 5; i++)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
                transform.parent.GetChild(i).position = transform.position;
                transform.parent.GetChild(i).GetComponent<Seperated>().SeperateLaunch(i, target);
            }
            first = true;
            StartCoroutine(Disappear());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            character.GameOver();
        }
        else if (col.tag != "SeperatedMissile")
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
        Destroy(transform.parent.gameObject);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}
