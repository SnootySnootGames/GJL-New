using UnityEngine;

public class CandyScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            StartTimer.timerUsed += 1;
        }
    }
}
