using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    [Header("Set Dynamically")]
    public GameObject launchpoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    public float velocityMult = 18f;
    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    void Awake()
    {
        S = this;

        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchpoint = launchPointTrans.gameObject;
        launchpoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void OnMouseEnter()
    {
        launchpoint.SetActive(true);
    }
    void OnMouseExit()
    {
        launchpoint.SetActive(false);
    }
    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos; 

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }      
    }
  
}
