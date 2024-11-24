using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb_self;
    private float inputX;
    private float inputY;

    public float moveSpeed;

    private Shooter shooter;

    private Animator anim;

    // bool key_flashlight = false;

    [SerializeField] SpriteRenderer flashlight;

    public GameObject goFlash;


    // New boolean variables
    public bool isCursorInTopRight;
    public bool isCursorInBottomRight;
    public bool isCursorInBottomLeft;
    public bool isCursorInTopLeft;
    public float currentAngle;
    private float smoothedAngle; // New variable for smoothed angle
    public float smoothingSpeed = 5f; // Smoothing speed for the angle

    public Transform flashPosBL, flashPosTL, flashPosBR, flashPosTR;

    public bool canMove = true;

    void Start()
    {
        if (!TryGetComponent(out rb_self))
        {
            Debug.LogWarning("Rigid Body 2D not found!");
        }
/*
        if (!transform.GetChild(0).GetChild(1).TryGetComponent(out shooter))
        {
            Debug.LogWarning("Shooter not found!");
        }
*/
        if (!TryGetComponent(out anim))
        {
            Debug.LogWarning("Animator not found!");
        }
    }

    void Update()
    {
        if (canMove)
        {
            inputX = 0;
            inputY = 0;

            if (Input.GetKey(KeyCode.W))
            {
                inputY += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputY -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputX -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputX += 1;
            }

            Vector2 AttackVector = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position;
            currentAngle = Angle_360(AttackVector); // Store the current angle

            // Smooth out the angle
            smoothedAngle = Mathf.LerpAngle(smoothedAngle, currentAngle, smoothingSpeed * Time.deltaTime);

            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, smoothedAngle);
            /*
            if (Input.GetMouseButtonDown(0))
            {
                shooter.OnShoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                transform.GetChild(1).rotation = transform.GetChild(0).rotation;
                anim.Play("MeleeAtk");
            }
            */

            if (currentAngle >= 0 && currentAngle <= 90)
            {
                isCursorInTopLeft = true;
                goFlash.transform.position = flashPosTL.position;
            }
            else
            {
                isCursorInTopLeft = false;
            }

            if (currentAngle >= 91 && currentAngle <= 180)
            {
                isCursorInBottomLeft = true;
                goFlash.transform.position = flashPosBL.position;
            }
            else
            {
                isCursorInBottomLeft = false;
            }

            if (currentAngle <= -1 && currentAngle >= -90)
            {
                isCursorInTopRight = true;
                goFlash.transform.position = flashPosTR.position;
            }
            else
            {
                isCursorInTopRight = false;
            }

            if (currentAngle <= -91 && currentAngle >= -180)
            {
                isCursorInBottomRight = true;
                goFlash.transform.position = flashPosBR.position;
            }
            else
            {
                isCursorInBottomRight = false;
            }
        }
        else
        {
            inputX = 0;
            inputY = 0;
        }

    }

    private void FixedUpdate()
    {
        rb_self.velocity = new Vector2(inputX, inputY).normalized * moveSpeed;
    }

    private float Angle_360(Vector2 Vector)
    {
        float x = Vector.x;
        float y = Vector.y;

        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        float cos = y / hypotenuse;
        float radian = Mathf.Acos(cos);

        float angle = 180 / (Mathf.PI / radian);

        if (x > 0)
        {
            angle = -angle;
        }
        return angle;
    }
}
