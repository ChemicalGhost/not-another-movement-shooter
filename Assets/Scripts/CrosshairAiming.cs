using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CrosshairAiming : MonoBehaviour
{
    // [SerializeField] Sprite crosshairSprite;
    [SerializeField] GameObject rayOrigin;
    [SerializeField] Camera cameraa;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();
        // Ensure the Line Renderer has 2 positions (start and end)
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetUpRay();
    }

    void SetUpRay()
    {

        Vector3 rayOriginPos = rayOrigin.transform.position;
        Vector3 rayDirection = cameraa.transform.forward;
        Ray cameraRay = cameraa.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, 100.0f))

        {
            // RenderLine(cameraRay.origin, hitInfo.point);

            // Calculate direction for the second ray
            Vector3 secondRayDirection = (hitInfo.point - rayOriginPos).normalized;

            // Cast the second ray from the gun towards the first hit point
            if (Physics.Raycast(rayOriginPos, secondRayDirection, out RaycastHit weaponHitInfo, hitInfo.distance))
            {

                Debug.Log("Hit SOMETHING" + hitInfo.point + hitInfo.distance);
                RenderLine(rayOriginPos, weaponHitInfo.point);

            }
            else
            {
                Debug.Log("NOTHING");

            }

        }
        else
        {
            // Optional: Extend the line to a maximum distance if no hit
            Vector3 maxDistancePoint = rayOriginPos + rayDirection * 100.0f;
            RenderLine(rayOriginPos, maxDistancePoint);
        }
    }

    void RenderLine(Vector3 lineOrign, Vector3 lineDestination)
    {
        lineRenderer.SetPosition(0, lineOrign);
        lineRenderer.SetPosition(1, lineDestination);
        // lineRenderer.sharedMaterial.SetColor("_Color", Color.green);
    }


    void HitByBullet()
    {
        // if (health > 0)
        // {
        //     // Debug.Log("");
        // }
        // else
        // {

        // }
    }
}
