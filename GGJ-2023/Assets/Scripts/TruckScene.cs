using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScene : MonoBehaviour
{

    [SerializeField] private GameObject puzzleMiniGameObject;
    void Start()
    {
        Invoke("EnablePuzzle", 4f);
    }

    public void EnablePuzzle()
    {
        puzzleMiniGameObject.SetActive(true);
    }
}
