using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public float rotationSpeed = 200f; // degrees per second

    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
