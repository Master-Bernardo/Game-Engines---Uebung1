using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivty = 3f;

    public Animator animator;

    private PlayerMover mover;

    void Start()
    {
        mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        //calculate movement velocity as a 3d Vector
        float _xMov = Input.GetAxisRaw("Horizontal"); //the Raw is used when we want some smoothing, when we want full control
        float _zMov = Input.GetAxisRaw("Vertical");  //both will go from -1 to 1


        Vector3 _moveHorizontal = transform.right * _xMov;  //Vector right is (1, 0, 0)
        Vector3 _moveVertical = transform.forward * _zMov;     //Vector front is (0, 0, 1)

        //Final Movement Vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //apply this movement
        mover.Move(_velocity);

        if (_velocity.magnitude > 0.1)
        {
            Debug.Log("yep");
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }


        //Calculate Rotationas a 3D Vector: (turning around y axis only) the rest of the rotation is only on the camera
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * mouseSensitivty;

        //apply Rotation
        mover.Rotate(_rotation);

        //Calculate Camera Rotation (up/down)
        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRot * mouseSensitivty;

        //apply camera Rotation
        mover.RotateCamera(_cameraRotationX);

        //check for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mover.Jump();
        }

        //check for 3rd person Camera enabled
        if (Input.GetKeyDown(KeyCode.T))
        {
            mover.ChangeCamera();
        }
    }
}
