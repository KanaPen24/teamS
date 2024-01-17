using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class charge : MonoBehaviour
{
    public VisualEffect effect;
    public GameObject effectd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            VisualEffect obj = Instantiate(effect, new Vector3(2.0f, 0.0f, 0.0f),Quaternion.identity);
            obj.Play();
        }
    }
}
