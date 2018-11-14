using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    private static Manager _instance = null;
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(Manager)) as Manager;
            }
            return _instance;
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public GameObject overPanel;
    public GameObject Char;

    public GameObject Normals;
    public GameObject Seperates;
    public GameObject Rapids;
    public GameObject Bombs;
    public GameObject Missiles;
    //public GameObject missiles;

    // 0 Seperate  1 Seperated 2 Char 3 Explosion(1) 4 Explosion(2) 
    public AudioClip[] Audio;
    public AudioSource audio;

    public Text Score;
    public Text Level;
    int destroy;
    float time;
    int level = 1;
    float x, y;
    bool playing = false;




    private void Update()
    {
        Camera.main.transform.position = new Vector3(Char.transform.position.x, Char.transform.position.y, Camera.main.transform.position.z);
    }

    private void Start()
    {
        GameStart();
    }



    public void SFX(int i)
    {
        switch (i)
        {
            case 0:
                audio.PlayOneShot(Audio[0]);
                break;
            case 1:
                audio.PlayOneShot(Audio[1]);
                destroy += 5;
                break;
            case 2:
                audio.PlayOneShot(Audio[2]);
                break;
            case 3:
            case 4:
                audio.PlayOneShot(Audio[Random.Range(2, 4)]);
                destroy += 10;
                break;
        }
    }


    public void GameOver()
    {
        SFX(0);
        playing = false;
        destroy = 0;
        level = 1;
        overPanel.SetActive(true);
    }

    public void GameStart()
    {
        playing = true;
        StartCoroutine(ScoreCounter());
        StartCoroutine(SpawnTimer());
    }

    public void Retry()
    {
        for (int i = 0; i < Missiles.transform.childCount; i++)
        {
            for(int o = 0; o < Missiles.transform.GetChild(i).childCount; o++)
            {
                Missiles.transform.GetChild(i).GetChild(o).gameObject.SetActive(false);
            }
        }
        Char.transform.position = Vector3.zero;


        Score.text = "0";
        //초기화

        overPanel.SetActive(false);
        GameStart();
    }

    IEnumerator ScoreCounter()
    {
        while (playing)
        {
            time += Time.deltaTime;
            if (destroy > 0)
            {
                Score.text = "" + Mathf.Floor(time * 10) + destroy;
                destroy = 0;
            }
            else Score.text = "" + Mathf.Floor(time * 10);

            if (int.Parse(Score.text) > 150) { level = 2; Level.text = "2"; }
            else if (int.Parse(Score.text) > 350) { level = 3; Level.text = "3"; }
            else if (int.Parse(Score.text) > 400) { level = 4; Level.text = "4"; }
            else if (int.Parse(Score.text) > 550) { level = 5; Level.text = "5"; }
            else if (int.Parse(Score.text) > 700) {level = 6; Level.text = "6"; }

            yield return null;
        }
    }

    IEnumerator SpawnTimer()
    {
        while(playing)
        {
            switch (level)
            {
                case 1:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(6);
                    break;
                case 2:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(4);
                    break;
                case 3:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(2);
                    break;
                case 4:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(1);
                    break;
                case 5:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(0.7f);
                    break;
                case 6:
                    Spawner(Random.Range(0, 3));
                    //StartCoroutine(Spawner(Random.Range(0, 3)));
                    yield return new WaitForSeconds(0.4f);
                    break;
            }
        }

    }

    // 0 normal 1 Seperate 2 Rapid 3 Bomb
    void Spawner(int num)
    {
        if ( 0 <= num && num <= 2)
        {
            // 위치 선정
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                float x = Random.Range(-2.8f, 2.8f);
                int random1 = Random.Range(0, 2);
                if (random1 == 0) y = 5.2f;
                else y = -5.2f;
            }
            else
            {
                int random2 = Random.Range(0, 2);
                if (random2 == 0) x = -3.0f;
                else x = 3.0f;
            }


            // 위치 경고 UI

            //yield return new WaitForSeconds(2);

            switch (num)
            {
                case 0:
                    Normals.transform.GetChild(0).gameObject.SetActive(true);
                    Normals.transform.GetChild(0).transform.position = new Vector3(Char.transform.position.x + x, Char.transform.position.y + y, 0);
                    Normals.transform.GetChild(0).transform.SetAsLastSibling();
                    break;
                case 1:
                    Seperates.transform.GetChild(0).gameObject.SetActive(true);
                    Seperates.transform.GetChild(0).transform.position = new Vector3(Char.transform.position.x + x, Char.transform.position.y + y, 0);
                    Seperates.transform.GetChild(0).transform.SetAsLastSibling();
                    break;
                case 2:
                    Rapids.transform.GetChild(0).gameObject.SetActive(true);
                    Rapids.transform.GetChild(0).transform.position = new Vector3(Char.transform.position.x + x, Char.transform.position.y + y, 0);
                    Rapids.transform.GetChild(0).transform.SetAsLastSibling();
                    break;
            }
        }
    }
}
