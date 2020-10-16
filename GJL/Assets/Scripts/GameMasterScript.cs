using UnityEngine;

public class GameMasterScript : MonoBehaviour
{

    [SerializeField] private bool paused = false;

    public static int characterSelection = 0; //0 = girl, 1 = boy

    private void Update()
    {
        PauseGameInput();
        PauseGameFunction();
    }

    private void PauseGameInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
    }

    private void PauseGameFunction()
    {
        if (paused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
