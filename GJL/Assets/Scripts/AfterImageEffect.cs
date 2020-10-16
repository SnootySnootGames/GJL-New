using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{

    [SerializeField] private SpriteRenderer afterImageSR;
    [SerializeField] private SpriteRenderer playerSR;

    private Transform playerTran;
    private GameObject player;
    private float alphaLimit = 0.9f;
    private float initialAlpha = 0.8f;
    private float timeLimit = 0.8f;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        afterImageSR = gameObject.GetComponent<SpriteRenderer>();
        playerSR = player.GetComponent<SpriteRenderer>();
        playerTran = player.transform;
        transform.position = playerTran.position;
        afterImageSR.sprite = playerSR.sprite;

        Color tmp = afterImageSR.color;
        tmp.a = initialAlpha;
        afterImageSR.color = tmp;
    }

    private void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            initialAlpha *= alphaLimit;
            Color tmp = afterImageSR.color;
            tmp.a = initialAlpha;
            afterImageSR.color = tmp;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
