using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestHover : MonoBehaviour
{
    // Reference to the Hover Label text
    public TextMeshProUGUI hoverLabel;
    // Distance within which the label appears
    public float hoverDistance = 3f; 
    private Transform playerTransform;
    private Camera mainCamera;

    private void Start()
    {
        // Ensure player has the "Player" tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        // Ensure the label is initially inactive
        hoverLabel.gameObject.SetActive(false); 
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= hoverDistance)
        {
            // Raycast from the center of the screen
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    // Calculate the position for the hover label relative to the chest
                    Vector3 labelPosition = transform.position + Vector3.up * 0.8f;
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