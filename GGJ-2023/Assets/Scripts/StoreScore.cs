using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreScore : MonoBehaviour
{
    public float finalScore;
    public TextMeshProUGUI finalScoreText;

    public static StoreScore storeScoreInstance;
    private void Awake()
    {
        storeScoreInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        finalScoreText.text = "Final Score: " + finalScore;
    }


    public void AddScore(float _scoreToAdd)
    {
        finalScore += _scoreToAdd;
    }
}
