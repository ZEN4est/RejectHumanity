using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    LineRenderer lr;

    public float nm, rm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.green;
        lr.endColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        laser();
    }

    private void laser() {
        
        List<Vector3> points = new();
        points.Add(transform.position);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward); 
        Physics.Raycast(ray, out hit);
        while(hit.collider is not null && hit.collider.GetComponent<Mirror>()) {
            points.Add(hit.point);
            ray.origin = hit.point;
            ray.direction = Vector3.Reflect(ray.direction, hit.normal);
            Physics.Raycast(ray, out hit);
        }
        points.Add(hit.collider is null ? points.Last() + ray.direction * 10 : hit.point);
        
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }
}
