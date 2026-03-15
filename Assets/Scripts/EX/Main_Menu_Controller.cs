using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Escena_01");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}