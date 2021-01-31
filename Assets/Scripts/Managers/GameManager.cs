using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private bool paused = false;
    public bool IsPaused { get { return paused; } }

    private void Start()
    {
    }

    public void Reset()
    {
    }

    private void Update()
    {
        if(Input.GetButtonUp("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        if(paused)
        {
            
        }
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    public void OnApplicationPause(bool pause)
    {
        
    }

    public void ChangeLevel(int level, float transitionTime)
    {
        Debug.LogWarning("Change level");
        Camera.main.GetComponent<CameraLevelSwitch>().GoToLevel(level, transitionTime);
    }
}
