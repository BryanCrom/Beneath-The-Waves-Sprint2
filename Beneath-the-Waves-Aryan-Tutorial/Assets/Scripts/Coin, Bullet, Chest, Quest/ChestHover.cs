using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestHover : MonoBehaviour
{
    public TextMeshProUGUI hoverLabel; // Reference to the Hover Label text
    public float hoverDistance = 3f; // Distance within which the label appears
    private Transform playerTransform;
    private Camera mainCamera;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Ensure your player has the "Player" tag
        mainCamera = Camera.main;
        hoverLabel.gameObject.SetActive(false); // Ensure the label is initially inactive
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= hoverDistance)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Raycast from the center of the screen
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    // Calculate the position for the hover label relative to the chest
                    Vector3 labelPosition = transform.position + Vector3.up * 0.8f; // Adjust the height as needed
                    hoverLabel.transform.position = labelPosition;

                    // Ensure the label faces the camera
                    hoverLabel.transform.LookAt(mainCamera.transform);
                    hoverLabel.transform.Rotate(0, 180, 0); // Adjust the rotation if the text appears backward

                    hoverLabel.gameObject.SetActive(true);
                    return;
                }
            }
        }

        hoverLabel.gameObject.SetActive(false);
    }
}