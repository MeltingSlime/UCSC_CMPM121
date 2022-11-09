using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    //grab global data
    public GameObject gobal;
    private Initialization gobal_init;
    private Setting gobal_setting;
    
    //int tempCount = 0;
    //speed of movement
    public float speed;
    //current controller
    public CharacterController controller;


    //check collision with ground for jumping
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    //variable about trigger
    private GameObject spawner;
    private Spawner spawner_script;



    //varibel used to control switcher
    [HideInInspector] public bool firstPersonEnabled;
    [HideInInspector] public bool thirdPersonEnabled;

    //control if player controller is enable or not
    bool enableControl;

    float horizontal;
    float vertical;

    float Angle;
    float smoothAngle;
    float turnSmooth_time = 0.1f;
    float turnSmooth_velocity;
    float gravity = -18f;
    Vector3 direction;
    Vector3 actual_moveDirection;
    Vector3 velocity;
    Vector3 teleportPos;
    bool isGrounded;
    public float collidingDistance = 0.4f;

    //third person camera that you want the player to follow
    GameObject thirdPerson_Camera;
    Transform cam;

    bool hasInteracted = false;


     




    private void Start()
    {
        firstPersonEnabled = true;
        thirdPersonEnabled = false;
        enableControl = true;
        gobal_init = gobal.GetComponent<Initialization>();
        gobal_setting = gobal.GetComponent<Setting>();
        thirdPerson_Camera = GameObject.Find("Camera ThirdPerson");
        cam = thirdPerson_Camera.transform;
        spawner = GameObject.Find("Spawner");
        spawner_script = spawner.GetComponent<Spawner>();
        
    }


    private void FixedUpdate()
    {

        //physic related
        if (enableControl)
        {

            //=================================================================
            /*                      player movement setup                    */
            playerMovementSetup();
            /*                             end                               */
            //=================================================================

        }
    }

    

    private void Update()
    {
        if (enableControl)
        {
            //=================================================================
            /*                           Jumping                             */
            jumpFeature();
            /*                             end                               */
            //=================================================================


            //player control in first person camera
            if (firstPersonEnabled)
            {


                //=================================================================
                /*                       movement control                        */
                firstpersonControll();
                /*                             end                               */
                //=================================================================
            }

            //player control in third person camera
            if (thirdPersonEnabled)
            {
                //=================================================================
                /*                       movement control                        */
                thirdpersonControll();
                /*                             end                               */
                //=================================================================
            }


        }

    }

    void playerMovementSetup()
    {
        //get key wasd and arrow key
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //create an invisiable sphere use to check if it's colliding with certain layer
        isGrounded = Physics.CheckSphere(groundCheck.position, collidingDistance, groundMask);


        //reset y velocity when it's on ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
            
        }


        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, collidingDistance);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.tag == "triggers")
            {
                spawner_script.enableGenerate = true;
                spawner_script.generateUp = false;
                spawner_script.generateDown = true;
            }
            else if(hitCollider.tag == "triggersUp")
            {
                spawner_script.enableGenerate = true;
                spawner_script.generateDown = false;
                spawner_script.generateUp = true;
            }else if (hitCollider.tag == "triggerSet" && hasInteracted == false)
            {
                //Debug.Log("generate set");
                spawner_script.furnitureSetGenerate = true;
                hasInteracted = true;
                StartCoroutine(waitThreeSecond());
            }
            else
            {

            }
        }
    }

    IEnumerator waitThreeSecond()
    {
        yield return new WaitForSeconds(1);
        hasInteracted = false;
    }

    void jumpFeature()
    {
        //jump features
        //applies to both first person and third person camera
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            
            //v = sqrt (h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
          


        }
        //apply gravity
        velocity.y += gravity * Time.deltaTime;
        //x = vt
        controller.Move(velocity * Time.deltaTime);
    }

    void firstpersonControll()
    {
        direction = transform.right * horizontal + transform.forward * vertical;
        controller.Move(direction.normalized * speed * Time.deltaTime);
    }

    void thirdpersonControll()
    {

        //find moving angle in vector
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            //convert vector3 into angle + add camera angle, so the player will go toward
            Angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //get smooth angle for turning
            smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref turnSmooth_velocity, turnSmooth_time);
            //make player rotate
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            //make finding moving direction of camera
            actual_moveDirection = Quaternion.Euler(0f, Angle, 0f) * Vector3.forward;
            //make player move
            controller.Move(actual_moveDirection.normalized * speed * Time.deltaTime);

        }

    }



    //old code about teleporting
    //IEnumerator Teleport()
    //{
    //    //avoid overlapping pos control
    //    enableControl = false;
    //    yield return new WaitForSeconds(0.1f);
    //    //teleport to location
    //    gameObject.transform.position = teleportPos;
    //    //regenerate the room objects
    //    gobal_setting.regeneratePos = true;
    //    yield return new WaitForSeconds(0.1f);
    //    enableControl = true;

    //}
}




