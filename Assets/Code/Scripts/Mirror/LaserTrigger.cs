using System;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event Action Triggered;
    public void Trigger() {
        Triggered.Invoke();
    }
}
