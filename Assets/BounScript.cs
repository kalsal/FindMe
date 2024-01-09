using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbt;
   
    GameObject[] ObjectTouche;
   

    Vector3 lastVelocity;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
    	Vector3 randomDirection = Random.onUnitSphere;
        rb.velocity = randomDirection * 5f;

        ObjectTouche = InitTabController.initTabController.FindMes;
        foreach (GameObject item in ObjectTouche)
        {
            rbt = item.GetComponent<Rigidbody2D>();
            rbt.AddForce(transform.up * 35f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {  
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);

        if(coll.gameObject.tag == "bas")
        {
            rb.AddForce(transform.up * 60f);
        }


        speed *= 100f; 
    }
    
  


  



}
