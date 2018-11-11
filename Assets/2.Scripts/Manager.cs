using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public GameObject overPanel;
    public GameObject Char;
    //public GameObject missiles;

    private void Update()
    {
        Camera.main.transform.position = new Vector3(Char.transform.position.x, Char.transform.position.y, Camera.main.transform.position.z);
    }

    public void GameOver()
    {
        overPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
