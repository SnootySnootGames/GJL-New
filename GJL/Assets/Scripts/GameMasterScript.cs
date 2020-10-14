using UnityEngine;

public class GameMasterScript : MonoBehaviour
{

    [SerializeField] private bool paused = false;

    private void Update()
    {
        PauseGameInput();
        PauseGameFunction();
    }

    private void PauseGameInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
