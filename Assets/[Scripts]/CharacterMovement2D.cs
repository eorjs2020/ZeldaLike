using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{

    [Header("Movement Properties")]
    public float horizontalForce;
    public float horizontalSpeed;

    public float verticalForce;
    public float airFactor;
    public Transform groundPoint; // the origin of the circle
    public float groundRadius; // the size of ths circle
    public LayerMask groundLayerMask; // the stuff we can collide with
    public bool isGrounded;

    private Rigidbody2D rigid2D;
        
    Animator _anim;
    public ChangeGameManager _gameManager;
    public float speed = 6f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public bool run;
    public GameObject trans;
    public float smoothness = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        else
            run = false;
        if (Input.GetKeyUp(KeyCode.T))
            _gameManager.ChangeGame3D(0);
        Move();
        Jump();


    }
    void Move()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 left = transform.TransformDirection(Vector3.right);
        //Vector3 right = transform.TransformDirection(Vector3.right);
        var input = Input.GetAxis("Horizontal");

        if (input != 0 && isGrounded)
        {


            input = (input > 0.0) ? 1.0f : -1.0f;
            rigid2D.velocity = new Vector2(input * horizontalSpeed, rigid2D.velocity.y);

            //rigid2D.velocity = Vector2.ClampMagnitude(rigid2D.velocity, horizontalSpeed);

            var clampXVelocity = Mathf.Clamp(rigid2D.velocity.x, -horizontalSpeed, horizontalSpeed);

            //rigid2D.velocity = new Vector2(clampXVelocity, rigid2D.velocity.y);
            _anim.SetFloat("Blend", 1.0f, 0.1f, Time.deltaTime);
        }
        else
        {
            _anim.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
            return;
        }

        if (input > 0)
            trans.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, 180, gameObject.transform.eulerAngles.z); 
        else if (input < 0)
            trans.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, 0, gameObject.transform.eulerAngles.z);
        
        
     /*   Vector3 moveDirection = left * input;

        
        transform.position += moveDirection * finalSpeed * Time.deltaTime;
        float percent = ((run) ? 1f : 0.5f) * moveDirection.magnitude;

*/
      
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0f))
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, verticalForce);
            //rigid2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
