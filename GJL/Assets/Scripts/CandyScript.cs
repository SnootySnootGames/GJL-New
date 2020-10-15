using UnityEngine;

public class CandyScript : MonoBehaviour
{

    [SerializeField] private Sprite[] candyChoices;
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
            gameObject.SetActive(false);
            StartTimer.timerUsed += 1;
        }
    }
}
