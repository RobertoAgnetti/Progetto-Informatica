using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegui : MonoBehaviour
{
    public Transform obbiettivo;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (obbiettivo != null)
        {
            transform.position = obbiettivo.position + offset;
        }
    }
}
