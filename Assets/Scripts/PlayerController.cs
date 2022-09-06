using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Control Variables")]
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float baseSpeed = 18f;
    [SerializeField] private float boostSpeed = 25f;

    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rb;
    private bool canMove = true;

    public bool CanMove
    {
        get { return canMove; }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GetBoost();
            GetRotation();
        }
    }

    private void GetRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-torqueAmount);
        }
    }

    private void GetBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }
}
