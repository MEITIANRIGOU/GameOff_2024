using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb_self;
    private float inputX;
    private float inputY;

    public float moveSpeed;

    private Shooter shooter;

    private Animator anim;

    bool key_flashlight = false;

   [SerializeField] SpriteRenderer flashlight;

 
    void Start()
    {
        if (!TryGetComponent(out rb_self))
        {
            Debug.LogWarning("Rigid Body 2D not found!");
        }

        if (!transform.GetChild(0).GetChild(1).TryGetComponent(out shooter))
        {
            Debug.LogWarning("Shooter not found!");
        }

        if (!TryGetComponent(out anim))
        {
            Debug.LogWarning("Animator not found!");
        }
    }

    void Update()
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
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, (float)Angle_360(AttackVector));

        if (Input.GetMouseButtonDown(0))
        {
            shooter.OnShoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            transform.GetChild(1).rotation = transform.GetChild(0).rotation;
            anim.Play("MeleeAtk");
        }

        /*
        key_flashlight = Input.GetKeyDown(KeyCode.F);
        if (key_flashlight)
        {
            flashlight.enabled = !flashlight.enabled;
        }
        */
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