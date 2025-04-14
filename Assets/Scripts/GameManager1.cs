using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instance;
    private List<GameObject> players; // List of active players
    public GameObject gameOverUI; // UI for game over screen
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        FindAllPlayers();
    }
    // Find all active players in the scene
    private void FindAllPlayers()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    // Remove player from list when they are eliminated
    public void PlayerEliminated(GameObject player)
    {
        players.Remove(player);
        if (players.Count == 1) // Only one player left
        {
            EndGame();
        }
    }
    // End the game when only one player is left
    private void EndGame()
    {
        Debug.Log("Game Over! Winner: " + players[0].name);
        gameOverUI.SetActive(true); // Show game over screen
    }
    // Restart game method
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Quit game method
    public void QuitGame()
    {
        Application.Quit();
    }
}
