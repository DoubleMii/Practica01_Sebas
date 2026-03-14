using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private InputSystem_Actions inputActions;
    private bool isPaused = false;

    public static GameManager GetInstance() => instance;

    void Awake()
    {
        // Singleton correcto para MonoBehaviour
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.Game.ReStart.performed += ctx => OnRperformed();
        inputActions.Game.Pause.performed += ctx => PausePerformed();
    }

    void OnDestroy()
    {
        inputActions.Game.ReStart.performed -= ctx => OnRperformed();
        inputActions.Game.Pause.performed -= ctx => PausePerformed();
        inputActions.Disable();
        inputActions.Dispose();
    }

    void PausePerformed()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    void OnRperformed()
    {
        isPaused = false;
        Time.timeScale = 1; // por si acaso estaba pausado al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}