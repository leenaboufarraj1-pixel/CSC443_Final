using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Loads the next scene in the build index (the actual game)
        SceneManager.LoadScene(1);
    }
}