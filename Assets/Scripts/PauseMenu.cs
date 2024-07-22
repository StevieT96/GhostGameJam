using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool isPaused;

    public string SceneName;

    private PlayerInputActionMap _inputActions;
    private PlayerInputActionMap _InputActions
    {
        get
        {
            if (_inputActions != null) return _inputActions;
            return _inputActions = new PlayerInputActionMap();
        }
    }

    private void OnEnable() => _InputActions.Enable();


    private void OnDisable() => _InputActions.Disable();



    // Start is called before the first frame update
    void Start()
    {
        _inputActions.Main.Move.performed += ctx => TogglePause();

        isPaused = false;
        pauseMenu.SetActive(false);
    }

    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> TogglePause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }

        return null;
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMainMenu()
    {   
         SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        
    }

    public void QuitApplication()
    {
        Debug.Log("quiting");
        Application.Quit();
    }
}
