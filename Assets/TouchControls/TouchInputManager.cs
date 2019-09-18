using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    [SerializeField]
    GameObject TouchInputUI;

    [SerializeField]
    bool DebugMode;

    // Start is called before the first frame update
    void Start()
    {
        HybridInput.SetDebugMode(DebugMode);
        TouchInputUI.SetActive(DebugMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (!TouchInputUI.activeSelf)
        {
            if (Input.touches.Length > 0)
            {
                TouchInputUI.SetActive(true);
            }
        }
    }
}
