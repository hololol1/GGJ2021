using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanComponent : MonoBehaviour
{
    public LayerMask scanLayer;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(new Ray(transform.position, Input.mousePosition - transform.position));
        Debug.DrawRay(transform.position, (ray.GetPoint(distance) - transform.position) * maxRange, Color.green);
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, (ray.GetPoint(distance) - transform.position) * maxRange);
    }

    private void DrawReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            return;
        }

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRange))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxRange;
        }

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(startingPosition, position);

        Debug.DrawLine(startingPosition, position, Color.blue);

        DrawReflectionPattern(position, direction, reflectionsRemaining - 1);


    }
}
