using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    void Awake()
    {
        AddColliderOnCamera();
    }


    void AddColliderOnCamera()
    {
        if(Camera.main == null)
        {
            //Debug.LogError("No cam");
            return;
        }

        Camera cam = Camera.main;
        if(!cam.orthographic)
        {
            Debug.LogError("cam on ortho?");
            return;
        }

        var edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        var LB = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var LT = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        var RT = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        var RB = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

        var edgePoints = new[] { LB, LT , RT, RB };

        edgeCollider.points = edgePoints;
    }
}
