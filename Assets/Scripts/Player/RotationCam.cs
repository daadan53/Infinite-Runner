using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCam : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private bool isRotating;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 180, 0);
        rotationSpeed = 5f;
    }

    void Update()
    {
        // RETOURNER
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            isRotating = true;
        }

        if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRotating = false;
        }

        if(isRotating)
        {
            Rotate();
        } else{transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);}
    }

    // Rotation
    private void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
