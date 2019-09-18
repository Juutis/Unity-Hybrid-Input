using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    SpriteRenderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(HybridInput.GetAxis("Horizontal"), HybridInput.GetAxis("Vertical"), 0f) * Time.deltaTime);
        if (HybridInput.GetButton("Button"))
        {
            r.color = new Color(0, 0, 0);
        }
        else
        {
            r.color = new Color(1, 1, 1);
        }
    }
}
