using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse");
            RaycastHit hit;
            if(Physics.Raycast(Camera.allCameras[0].ScreenPointToRay(Input.mousePosition), out hit)) {
                Debug.Log(hit.point);
                goTo(hit.point);
            }
        }
    }

    public void goTo(Vector3 pos) {
        navMeshAgent.SetDestination(pos);
    }
}
