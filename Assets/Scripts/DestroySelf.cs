using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float Delay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
