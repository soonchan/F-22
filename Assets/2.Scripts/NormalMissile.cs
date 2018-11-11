using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NormalMissile : MonoBehaviour {

    Character character;

    GameObject targetG;
    Vector2 target;
    public float speed;
    public float rotateSpeed;

    Rigidbody2D rb;
    public AudioClip[] ExplosionAudio;
    public AudioSource audio;


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
