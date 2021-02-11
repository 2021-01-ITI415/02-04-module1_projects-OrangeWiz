using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S;

    static public GameObject POI;
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 destination;

        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }
}
