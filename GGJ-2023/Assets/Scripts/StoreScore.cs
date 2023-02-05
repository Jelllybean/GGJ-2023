using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScore : MonoBehaviour
{
    public float finalScore;

    public static StoreScore storeScoreInstance;
    private void Awake()
    {
        storeScoreInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddScore(float _scoreToAdd)
    {
        finalScore += _scoreToAdd;
    }
}
