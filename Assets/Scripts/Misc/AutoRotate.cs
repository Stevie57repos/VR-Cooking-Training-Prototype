using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed * Time.fixedDeltaTime);
    }
}
