using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PouseMenu : MonoBehaviour
{
    public GameObject pouseMenu;

    public bool isPoused;

    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        isPoused = false;
        pouseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pouse();
    }

    void pouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPoused == false)
        {
            pouseMenu.SetActive(true);
            Time.timeScale = 0;
            isPoused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPoused == true)
        {
            pouseMenu.SetActive(false);
            Time.timeScale = 1;
            isPoused = false;
        }
    }

    public void Resume()
    {
        pouseMenu.SetActive(false);
        Time.timeScale = 1;
        isPoused = false;
    }

    public void GoToMainMenu()
    {   
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("quiting");
    }
}
