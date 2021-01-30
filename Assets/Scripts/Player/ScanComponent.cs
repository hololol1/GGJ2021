using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanComponent : MonoBehaviour
{
    public LayerMask reflectionLayer;
    private LineRenderer laserLine;
    private float distance = 5.0f;
    
    public float maxRange = 20.0f;
    public int maxReflectionCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        laserLine = GetComponent<LineRenderer>();
        //laserLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Fire();
        }
        else
        {
            //laserLine.enabled = false;
        }
    }

    private void Fire()
    {
        laserLine.enabled = true;
        Vector3 startPos = transform.position;
        laserLine.SetPosition(0, startPos);
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(startPos, (mouseRay.GetPoint(distance) - startPos) * maxRange, Color.green);
        Ray laserRay = new Ray(startPos, mouseRay.GetPoint(distance) - startPos);
        RaycastHit hit;

        if (Physics.Raycast(laserRay, out hit, maxRange))
        {
            Vector3 endPos = hit.point;
            laserLine.SetPosition(1, endPos);
            if(hit.collider.gameObject.tag == "Reflection")
            {
                DrawReflectionPattern(endPos, Vector3.Reflect(endPos, hit.normal), 2);
            }
        }
        else
        {
            laserLine.positionCount = 2;
            laserLine.SetPosition(1, (mouseRay.GetPoint(distance) - startPos) * maxRange);
        }
    }

    private void DrawReflectionPattern(Vector3 position, Vector3 direction, int reflectionNumber)
    {
        if (reflectionNumber == maxReflectionCount)
        {
            return;
        }

        laserLine.positionCount = reflectionNumber + 1;
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRange, reflectionLayer))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
            laserLine.SetPosition(reflectionNumber, position);
            
            if (hit.collider.gameObject.tag == "Reflection")
            {
                DrawReflectionPattern(position, direction, reflectionNumber + 1);
            }
        }
        else
        {
            position += direction * maxRange;
            laserLine.SetPosition(reflectionNumber, position);
        }

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(startingPosition, position);

        Debug.DrawLine(startingPosition, position, Color.blue);

        


    }
}
