using TMPro;
using UnityEngine;

public class coinRotation : MonoBehaviour
{
    public float rotationSpeed = 120f;
   
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime,0);
    }
}
