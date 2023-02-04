using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class DiggingMiniGame : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private DrawingSettings drawingSettings;

    [SerializeField] private Vector2 mouseInput;

    [SerializeField] private SnifferAbility staminaManagement;
    [SerializeField] private CheckTransparency transparencyChecker;

    [SerializeField] private Transform pigSprite;

    [Header("Audio")]
    [SerializeField] private List<AudioClip> diggingNoises = new List<AudioClip>();
    [SerializeField] private AudioSource diggingPlayer;

    void Start()
    {
        drawingSettings.SetEraser();
        transparencyChecker.StartMiniGame();
    }

    void Update()
    {
        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");

        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = camera.transform.position.z + camera.nearClipPlane;
        pigSprite.transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            diggingPlayer.clip = diggingNoises[Random.Range(0, diggingNoises.Count)];
            diggingPlayer.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            transparencyChecker.Test();
            diggingPlayer.Stop();
        }

        if (Input.GetMouseButton(0))
        {
            if ((Mathf.Abs(mouseInput.x) + 0.5f) > Mathf.Abs(mouseInput.y))
            {
                drawingSettings.SetMarkerWidth(10);
                staminaManagement.stamina -= 0.25f * Time.deltaTime;
            }
            else if (Mathf.Abs(mouseInput.y) > Mathf.Abs(mouseInput.x))
            {
                drawingSettings.SetMarkerWidth(25);
                staminaManagement.stamina -= 0.75f * Time.deltaTime;
            }
        }
    }
}
