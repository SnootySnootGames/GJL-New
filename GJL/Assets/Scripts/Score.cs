using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private int scorePerCandy = 10;
    [SerializeField] private float comboTimerCount = 0f;
    [SerializeField] private float comboTimerReset = 3f;
    [SerializeField] private TMP_Text scoreBoard;
    [SerializeField] private TMP_Text multiplierBoard;
    [SerializeField] private TMP_Text xBoard;
    public int currentScore = 0;
    public int numberOfCandiesCollectedDuringCombo = 0;
    public bool comboEnded = true;
    public int scoreMultiplier = 1;

    public static bool candyCollected = false;

    private void Start()
    {
        scoreBoard.text = currentScore.ToString();
    }

    private void Update()
    {
        ComboMethod();
        comboTimer();
    }

    private void ComboMethod()
    {
        if (candyCollected == true)
        {
            candyCollected = false;
            if (comboEnded == true)
            {
                xBoard.gameObject.SetActive(true);
                comboEnded = false;
                numberOfCandiesCollectedDuringCombo = 1;
                comboTimerCount = comboTimerReset;
                currentScore += 100;
                scoreBoard.text = currentScore.ToString();
            }
            else
            {
                comboTimerCount = comboTimerReset;
                numberOfCandiesCollectedDuringCombo++;
                MultiplierMethod();
                ScoreMethod();
                scoreBoard.text = currentScore.ToString();
            }
        }
    }

    private void comboTimer()
    {
        if (comboTimerCount > 0)
        {
            comboTimerCount -= Time.deltaTime;
            Color tmp = xBoard.color;
            tmp.a = comboTimerCount / comboTimerReset;
            xBoard.color = tmp;
            multiplierBoard.color = tmp;
        }
        else
        {
            comboEnded = true;
            xBoard.gameObject.SetActive(false);
            numberOfCandiesCollectedDuringCombo = 0;
            scoreMultiplier = 1;
        }
    }

    private void MultiplierMethod()
    {
        if (numberOfCandiesCollectedDuringCombo % 3 == 0)
        {
            scoreMultiplier++;
            multiplierBoard.text = scoreMultiplier.ToString();
            Color randomColorSwap = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            xBoard.color = randomColorSwap;
            multiplierBoard.color = randomColorSwap;
        }
    }

    private void ScoreMethod()
    {
        currentScore += 100 * scoreMultiplier;

        if (GameMasterScript.currentLevel == 1)
        {
            GameMasterScript.level1Score = currentScore;
        }
    }


    //candy collected
    //combo gets started-timer resets and starts counting down to zero
    //combo is ongiong unless timer hits zero
    //count the number of candies collected during combo
    //after 3 candies, multiplier increase by 1
    //if timer hits zero, combo ends
}
