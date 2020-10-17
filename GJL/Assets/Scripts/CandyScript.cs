using UnityEngine;

public class CandyScript : MonoBehaviour
{

    [SerializeField] private int scorePerCandy = 10;
    [SerializeField] private float comboTimerCount;
    [SerializeField] private float comboTimerReset = 3f;
    [SerializeField] private Sprite[] candyChoices;

    private bool comboEnded = true;
    private int numberOfCandiesCollectedDuringCombo;
    private SpriteRenderer candySpriteRend;

    private void Start()
    {
        candySpriteRend = gameObject.GetComponent<SpriteRenderer>();
        int randomCandyTextInt = Random.Range(0, candyChoices.Length);
        candySpriteRend.sprite = candyChoices[randomCandyTextInt];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Score.candyCollected = true;
            gameObject.SetActive(false);
        }
    }
}
