using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public GameObject pauseScreen;
	public AudioMixer audioMixer;

	private void Start()
	{
		audioMixer.SetFloat("musicVolume", 0);
		audioMixer.SetFloat("gameVolume", 0);
	}

	public void ReturnToMenu()
	{
		audioMixer.SetFloat("gameVolume", 0);
		Time.timeScale = 1.0f;
	}

	public void Quit()
	{
		Application.Quit();
		print("Quit");
	}

	public void Play()
	{
		SceneManager.LoadScene("SampleScene");
		print("Play");
	}
}
