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
    void Update()
    {
        SetUpRay();
    }

    void SetUpRay()
    {

        Vector3 rayOriginPos = rayOrigin.transform.position;
        Vector3 rayDirection = cameraa.transform.forward;
        Ray cameraRay = cameraa.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, 100.0f))
        // if (Physics.Raycast(rayOrigin.GetComponent<Transform>().position, rayOrigin.GetComponent<Transform>().forward, out RaycastHit hitInfo, 20.0f))
        {
            if (Physics.Raycast(rayOriginPos, hitInfo.point, out RaycastHit weaponHitInfo, 100.0f))
            {

                RenderLine(rayOriginPos, weaponHitInfo.point);

            }
            Debug.Log("Hit SOMETHING" + hitInfo.point + hitInfo.distance);
            // Debug.DrawLine(rayOrigin.GetComponent<Transform>().position, hitInfo.point, Color.red, 1.5f);
            RenderLine(cameraRay.origin, hitInfo.point);

        }
        else
        {
            Debug.Log("NOTHING");
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
}
