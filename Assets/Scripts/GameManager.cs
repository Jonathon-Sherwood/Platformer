using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Allows all scripts to call this.
    public int currentSceneIndex = 0; //Holds the number for whichever scene we are on.
    public Player player; //Allows the gamemanager to hold onto the player within the scene.
    public GameObject spawnPoint; //Allows the gamemanager to spawnt he player in a defined location.
    public GameObject playerPrefab; //Sets a prefab that can be instantiated in the level.
    [HideInInspector] public int playerLives; //Holds onto the value of the players current lives;
    public int MaxPlayerLives; //Allows the designer to choose how many times the player can die.


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
            Destroy(this.gameObject);
        }
        playerLives = MaxPlayerLives;
    }

    private void Start()
    {
        AudioManager.instance.Play("Music"); //Starts playing music on start.
    }

    private void Update()
    {
        //Switches to the lose screen on player death 4 times, otherwise respawns player.
        if (player == null && (currentSceneIndex != (4) && currentSceneIndex != (0) && currentSceneIndex != (3)) && playerLives < 0)
        {
            LoadLevel(4);
            AudioManager.instance.Play("Die");
            AudioManager.instance.Stop("Music");
            AudioManager.instance.Play("Lose");
        } else if (player == null && (currentSceneIndex != (4) && currentSceneIndex != (0) && currentSceneIndex != (3)) && playerLives >= 0)
        {
            AudioManager.instance.Play("Die");
            Instantiate(playerPrefab, spawnPoint.transform);
            playerLives--;
        }
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
        currentSceneIndex = scene.buildIndex;

        //Replaces the player on the spawn point in the scene if the player has lives.
        spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null && playerLives >= 0)
        {
            Instantiate(playerPrefab, spawnPoint.transform);
            playerLives--;
        }

        if(currentSceneIndex == 1)
        {
            AudioManager.instance.Play("Music"); //Starts music after lose screen.
        }
    }

    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }
}
