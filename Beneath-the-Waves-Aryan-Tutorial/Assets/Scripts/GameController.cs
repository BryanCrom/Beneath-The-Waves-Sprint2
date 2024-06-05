using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void ReloadScene()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        MovePlayerToPosition(new Vector3(22f, 3f, -40f));

    }

    private void MovePlayerToPosition(Vector3 newPosition)
    {
        // Find the player GameObject in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If player GameObject exists, move it to the specified position
        if (player != null)
        {
            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found in the scene.");
        }
    }
}
