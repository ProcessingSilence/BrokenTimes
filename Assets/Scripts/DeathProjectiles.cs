using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProjectiles : MonoBehaviour
{
    public float rotationAmount;

    public Vector2 force;
    

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.AddRelativeForce(force);
        rb.AddTorque(rotationAmount, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
