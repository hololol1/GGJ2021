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
       

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Intro.mp4");
        videoPlayer.url = filePath;

        //videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        //videoPlayer.targetCameraAlpha = 1.0f;
       videoPlayer.Play();

        time = videoPlayer.length;
    }

    // Update is called once per frame
    void Update()
    {
       /* currentTime = videoPlayer.time;
        //if(currentTime >= time-0.1f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }*/
    }
}
