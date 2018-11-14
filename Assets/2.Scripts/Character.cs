using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {

    private static Character _instance = null;
    public static Character Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(Character)) as Character;
            }
            return _instance;
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public float maxSpeed;
    public float minSpeed;
    public float rotateSpeed;
    float speed = 0;
    int a = 1;
    [HideInInspector] public Vector2 flyAngle;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float rotateAmount = Vector3.Cross(flyAngle, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }


    public IEnumerator ToMin()
    {
        while (minSpeed < speed)
        {
            speed -= 0.1f * a;
            a++;

            yield return null;
        }
        speed = minSpeed;
        a = 0;
    }

    public IEnumerator ToMax()
    {
        while (maxSpeed > speed)
        {
            speed += 0.1f * a;
            a++;

            yield return null;
        }
        speed = maxSpeed;
        a = 0;
    }
}
