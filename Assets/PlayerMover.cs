using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float distToGround = 1f;  //for grounded raycast - half the height of the character
    private bool jump; //will we jump this frame?
    bool sprint;

    bool isGrounded;

    //for movement
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sprintSpeed = 10f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float maxSprintSpeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;

    //Cameras
    [SerializeField]
    private GameObject camera3rdPerson;
    [SerializeField]
    private GameObject camera1stPerson;
    private bool firstPersonMode = true;
    private Camera cam;
    public GameObject boneToMove; // the bone we need to ove to look up or down
    public GameObject boneToMove2;
    public GameObject boneToMove3;


    private Rigidbody rb;



    //for the XRot CameraClamp
    [SerializeField]
    private float cameraRotationLimit = 85f;
    private float currentCameraRotationX = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = camera1stPerson.GetComponent<Camera>();
    }

    //Setter

    public void Move(Vector3 _velocity, bool sprint)
    {
        velocity = _velocity;
        this.sprint = sprint;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void Jump()
    {
        //check if is grounded
        if(IsGrounded())
        {
            jump = true;
        }
       
    }

    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, distToGround + 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //change from 3rd to first person
    public void ChangeCamera()
    {
        firstPersonMode = !firstPersonMode;

        if (firstPersonMode)
        {
            camera1stPerson.SetActive(true);
            camera3rdPerson.SetActive(false);
            cam = camera1stPerson.GetComponent<Camera>();
            cameraRotationLimit = 85f;
        }
        else
        {
            camera3rdPerson.SetActive(true);
            camera1stPerson.SetActive(false);
            cam = camera3rdPerson.GetComponent<Camera>();
            cameraRotationLimit = 40f;
        }
    }

    //runs every Physics iteration -> perform movment here
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
       
    }

    private void LateUpdate()
    {
        PerformCameraRotation();
       // UpdateAnimationParameters();
    }

    void PerformMovement()
    {
        if (IsGrounded())
        {
            if (velocity != Vector3.zero)
            {
                if (sprint)
                {
                    if(rb.velocity.magnitude<maxSprintSpeed) rb.AddForce(rb.position + velocity * Time.fixedDeltaTime * 1000 * sprintSpeed);   //better than just add force - or rb.MovePosition?
                }
                else
                {
                    if (rb.velocity.magnitude < maxSpeed) rb.AddForce(rb.position + velocity * Time.fixedDeltaTime * 1000 * speed);  //rb.AddForce(velocity * Time.fixedDeltaTime); //works also, but we have to increace speed and drag
                }
            }
            if (jump)
            {
                rb.AddForce(transform.up * jumpForce * 1000);
                jump = false;
            }
        }
        
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));  //Euler angles can take simple vector3´s and turn them into a quaternion
    }

    void PerformCameraRotation()
    {
        if (cam != null)
        {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply Rotation to the transform of our Camera
            //cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            boneToMove.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            boneToMove2.transform.localEulerAngles = new Vector3(currentCameraRotationX / 2 - 20, 0f, 0f); // soll die hälfte der brustrotation nehmen
            boneToMove3.transform.localEulerAngles = new Vector3(currentCameraRotationX / 4 - 147, 0f, 0f);
        }
    }
}
