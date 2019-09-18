using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour
{
    [SerializeField]
    public string ButtonName;

    [SerializeField]
    private float radius = 64;

    [SerializeField]
    private bool ShowLabel = true;

    [SerializeField]
    private Text Label;
    
    private Image Image;

    public bool IsDown { get; private set; }

    void Awake()
    {
        HybridInput.Register(this);
        Label.enabled = ShowLabel;
        Label.text = ButtonName;
    }

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        IsDown = false;
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (Vector2.Distance(touch.position, transform.position) < radius)
                {
                    IsDown = true;
                    break;
                }
            }
        }

        if (HybridInput.IsDebugMode())
        {
            if (Input.GetKey(KeyCode.Mouse0) && Vector2.Distance(Input.mousePosition, transform.position) < radius)
            {
                IsDown = true;
            }
        }

        if (IsDown)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0.6f);
        }
        else
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0.5f);
        }
    }

    void OnDestroy()
    {
        HybridInput.Unregister(this);
    }
}
