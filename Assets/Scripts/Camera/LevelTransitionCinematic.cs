using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionCinematic : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;
    public double time;
    public double currentTime;

    [SerializeField]
    private string _movieFilename;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();

        StartCoroutine(PlayMovie(_movieFilename));

        //time = videoPlayer.length;
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

    /// <summary>
    /// Stream the specified video.
    /// </summary>
    /// <param name="filename">The video file.</param>
    /// <returns>Coroutine.</returns>
    private IEnumerator PlayMovie(string filename)
    {
        //VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            // It's important that the video is in /Assets/StreamingAssets
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, _movieFilename);

            //Debug.Log($"About play video: {_movieFilename}");

            videoPlayer.url = videoPath;

            videoPlayer.Play();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            videoPlayer.Stop();
        }
    }


}
