using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task10_2 : MonoBehaviour
{
    private Renderer renderer;
    private Color savedColor;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            savedColor = renderer.material.color;
        }
    }

    public void OnHit()
    {
        renderer.material.color = Color.red;
    }

    public void OffHit()
    {
        renderer.material.color = savedColor;
    }
}
