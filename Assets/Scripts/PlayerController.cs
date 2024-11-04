using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb_self;
    private float inputX;
    private float inputY;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Rigidbody2D>(out rb_self))
        {
            Debug.LogWarning("Rigid Body 2D not found!");
        }
    }

    // Update is called once per frame
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

        Vector2 AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
        float Angle = Angle_360(AttackVector);

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Angle);
    }

    private void FixedUpdate()
    {
        rb_self.velocity = new Vector2(inputX, inputY).normalized * moveSpeed;
    }

    public float Angle_360(Vector2 Vector)
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