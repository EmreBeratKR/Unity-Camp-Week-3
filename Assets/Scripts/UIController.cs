using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
