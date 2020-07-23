using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public int currentSceneIndex = 0; //Holds the number for whichever scene we are on.

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        //Turns the gamemanager into a singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Debug.LogError("Gamemanager attempted to create second instance.");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void LoadLevel (int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// This method is called every time a scene finishes loading.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene finished loading");
        currentSceneIndex = scene.buildIndex;
    }

    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }
}
