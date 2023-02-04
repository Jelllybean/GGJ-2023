using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class DiggingMiniGame : MonoBehaviour
{
    [SerializeField] private DrawingSettings drawingSettings;

    [SerializeField] private Vector2 mouseInput;

    [SerializeField] private SnifferAbility staminaManagement;

    void Start()
    {
        drawingSettings.SetEraser();
    }

    void Update()
    {
        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");

        if ((Mathf.Abs(mouseInput.x) + 0.5f) > Mathf.Abs(mouseInput.y))
        {
            drawingSettings.SetMarkerWidth(10);
            staminaManagement.stamina -= 0.25f * Time.deltaTime;
        }
        else if(Mathf.Abs(mouseInput.y) > Mathf.Abs(mouseInput.x))
        {
            drawingSettings.SetMarkerWidth(25);
            staminaManagement.stamina -= 0.75f * Time.deltaTime;
        }
    }
}
