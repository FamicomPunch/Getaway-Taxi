using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    bool loadingScreen = false;
    public Text titleText, endlessText, carmageddonText;
    
    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("Title Text").GetComponent<Text>();
        endlessText = GameObject.Find("Endless Text").GetComponent<Text>();
        carmageddonText = GameObject.Find("Carmageddon Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        loadingScreen = true;
        titleText.text = "Loading...";
        titleText.color = Color.green;
        titleText.fontSize = 50;
        SceneManager.LoadSceneAsync("EndlessRoadTest", LoadSceneMode.Single);
    }

    public void StartEndless()
    {
        //if (endlessText.text != "???")
        {
            //loadingScreen = true;
            titleText.text = "Loading...";
            titleText.color = Color.blue;
            titleText.fontSize = 50;
            //SceneManager.LoadScene("EndlessRoad", LoadSceneMode.Single);}
        }
    }

    public void Carmageddon()
    {
        //if (carmageddonText.text != "???")
        {
            //loadingScreen = true;
            titleText.text = "Loading...";
            titleText.color = Color.red;
            titleText.fontSize = 50;
            //SceneManager.LoadScene("Carmageddon", LoadSceneMode.Single);
        }

    }

    public void QuitGame()
    {
        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
            Debug.Log("Quit Game");
        else
            QuitGame();
    }

}
