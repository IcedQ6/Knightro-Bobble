using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;


public class startGame : MonoBehaviour
{
    public void switchToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
