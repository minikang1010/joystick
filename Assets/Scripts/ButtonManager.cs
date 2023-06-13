using UnityEngine;

public class ButtonManager : MonoBehaviour
{ 
    private bool m_IsButtonDowning;
    
    private Camera Camera;
    private Rigidbody2D rb;
    
    private Vector2 p;
    private float sr;
    
    private Vector2 Mpos;
    [Range(1,10)] [SerializeField] private float speed;
    [SerializeField] private float max = 3f;
    [Space]
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject zero;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject obj;
    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GameObject.Find("Triangle").GetComponent<Rigidbody2D>();
    }

    
    private Transform trans;
    private void FixedUpdate()
    {
        if (m_IsButtonDowning)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out Mpos);
            Mpos = myCanvas.transform.TransformPoint(Mpos);
            Vector2 zeros = myCanvas.transform.TransformPoint(zero.transform.localPosition);
            
            p = new Vector2(Mpos.x, Mpos.y) - new Vector2(zeros.x,zeros.y);
            sr = Mathf.Rad2Deg * Mathf.Atan2(p.x, p.y);
            bar.transform.localRotation = Quaternion.Euler(0f,0f,-sr);
            var sum = Mathf.Pow(Mathf.Pow(Mpos.x-zeros.x,2f)+Mathf.Pow(Mpos.y-zeros.y,2f),0.5f) / 100;
            if (sum > max) sum = max;
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0.4f,sum);
            transform.localPosition = new Vector3(0,sum,0);
            obj.transform.rotation = Quaternion.Euler(0f,0f,-sr);
            rb.velocity = p.normalized * (speed * sum);

        }
        else
        {
            const int sum = 0;
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0.4f,sum);
            transform.localPosition = new Vector3(0,sum,0);
            obj.transform.rotation = Quaternion.Euler(0f,0f,-sr);
            rb.velocity = p.normalized * (speed * sum);
        }

    }

    public void PointerDown()
    {
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        m_IsButtonDowning = false;
    }
}
