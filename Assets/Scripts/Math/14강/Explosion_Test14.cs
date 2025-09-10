using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion14 : MonoBehaviour
{
    public float force = 300f;
    public float radius = 5f;

    public float delay = 1.5f;
    public float upwardsModifier = 1f;

    void OnEnable()
    {
        //Invoke("RunExplode", 2f);
        Invoke(nameof(ManualExplode), delay);
    }

    void RunExplode()  //유니티에서 제공하는 폭발함수 사용 rb.AddExplosionForce
    {
        Vector3 explosionPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (var col in hitColliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(force, explosionPos, radius);
            }
        }
        Destroy(gameObject);  //폭발 제거
    }

    void ManualExplode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (var col in colliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if(rb == null) continue;

            float rbMass = rb.mass;

            Vector3 toTarget = rb.position - explosionPos;
            float distance = toTarget.magnitude;
            Vector3 dir = toTarget.normalized;
            float attenuation = 1f - Mathf.Clamp01(distance / radius);
            dir += Vector3.up * upwardsModifier;
            dir = dir.normalized;
            Vector3 impulse = dir * force / rbMass + dir * force * 0.3f * attenuation / rbMass;
            rb.AddForce(impulse, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
