using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TouchStick : MonoBehaviour
{
    private static readonly int ID_NONE = -1;
    private static readonly int ID_MOUSE = -2;

    [SerializeField]
    private string HorizontalAxisName, VerticalAxisName;

    [SerializeField]
    private float Radius = 64;

    [SerializeField]
    private GameObject Knob;

    private TouchAxis HorizontalAxis, VerticalAxis;

    private bool Grabbed;
    private int FingerId = -1;

    void Awake()
    {
        if (HorizontalAxisName != null && HorizontalAxisName.Length > 0)
        {
            HorizontalAxis = new TouchAxis(HorizontalAxisName);
            HybridInput.Register(HorizontalAxis);
        }

        if (VerticalAxisName != null && VerticalAxisName.Length > 0)
        {
            VerticalAxis = new TouchAxis(VerticalAxisName);
            HybridInput.Register(VerticalAxis);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FingerId == ID_NONE)
        {
            if (HybridInput.IsDebugMode()
                && (Input.GetKeyDown(KeyCode.Mouse0)
                && Vector2.Distance(Input.mousePosition, transform.position) < Radius))
            {
                FingerId = ID_MOUSE;
            }
            foreach (Touch touch in Input.touches)
            {
                if (Vector2.Distance(touch.position, transform.position) < Radius)
                {
                    FingerId = touch.fingerId;
                }
            }

        }

        Vector3 touchPosition = Vector3.zero;
        bool foundActiveTouch = false;
        if (FingerId == ID_MOUSE)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                touchPosition = Input.mousePosition;
            }
            else
            {
                FingerId = ID_NONE;
            }
        }
        else if (FingerId != ID_NONE)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == FingerId)
                {
                    touchPosition = touch.position;
                    foundActiveTouch = true;
                    break;
                }
            }
            if (!foundActiveTouch)
            {
                FingerId = ID_NONE;
            }
        }
        
        Vector3 diff;

        if (FingerId != ID_NONE)
        {
            diff = (touchPosition - transform.position) / Radius;
            diff.z = 0;
            if (diff.magnitude > 1.0f)
            {
                diff = diff.normalized;
            }
        }
        else
        {
            diff = Vector3.zero;
        }
        HorizontalAxis.Axis = diff.x;
        VerticalAxis.Axis = diff.y;

        Vector2 knobPos = transform.position + diff * Radius;
        Knob.transform.position = new Vector3(knobPos.x, knobPos.y, Knob.transform.position.z);
    }

    void OnDestroy()
    {
        HybridInput.Unregister(HorizontalAxis);
        HybridInput.Unregister(VerticalAxis);
    }
}
