using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Animator _anim;
    CharacterController _controller;
    Camera _cam;
    public ChangeGameManager _gameManager;
    public float speed = 6f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public bool toggleCameraRotation;
    public bool run;
    public bool isAttacking = false;
    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        //_controller = GetComponent<CharacterController>();
        _cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.T))
            toggleCameraRotation = true;
        else
            toggleCameraRotation = false;*/
        
       

        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        else
            run = false;

        if (Input.GetKeyUp(KeyCode.T))
            _gameManager.ChangeGame(0);
        InputMovement();
        if (Input.GetKeyDown(KeyCode.Space) && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            Attack();



    }

    void Attack()
    {        
        _anim.SetTrigger("Attack");        
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), smoothness * Time.deltaTime);
        }
    }


    void InputMovement()
    {
        if (_anim.GetBool("Attack") == false)
        {
            finalSpeed = (run) ? runSpeed : speed;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            //Vector3 right = transform.TransformDirection(Vector3.right);

            Vector3 moveDirection = forward * Input.GetAxis("Vertical");

            //_controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                transform.position += moveDirection * finalSpeed * Time.deltaTime;
            }            
            float percent = ((run) ? 1f : 0.5f) * moveDirection.magnitude;

            transform.rotation = Quaternion.Euler(0, _cam.transform.rotation.eulerAngles.y, 0);

            _anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
        }
    }
}
