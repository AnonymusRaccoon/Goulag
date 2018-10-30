using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour {
    [SerializeField]
    public float Speed = 1;
 
    private Renderer rend;

    // Update is called once per frame

    void Update()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
    }
}
 
    



