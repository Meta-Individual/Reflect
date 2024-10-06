using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakScreenController : MonoBehaviour
{
    public float explosionMinForce = 5f;
    public float explosionMaxForce = 100f;
    public float explosionForceRadius = 10f;

    public Rigidbody[] rigidbodies;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    void Explode()
    {
        StartCoroutine(ExplodeCoroutine());
         //this.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), this.transform.position, explosionForceRadius);
    }

    IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), this.transform.position, explosionForceRadius);
        }
    }
}
