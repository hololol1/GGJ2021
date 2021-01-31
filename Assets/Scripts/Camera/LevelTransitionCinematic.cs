using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionCinematic : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;
    public double time;
    public double currentTime;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        time = videoPlayer.length;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = videoPlayer.time;
        if(currentTime >= time-0.1f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
