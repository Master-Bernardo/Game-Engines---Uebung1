using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour {

   
    private float mouseSensitivty = 3f;
    float movAdder = 0; //for sprint

    private PlayerMover mover;
    public Animator animator;

    public float interactableScannIntervall; //how often do we check for interactables
    float nextInteractableScannTime;
    public float interactableScanDistance; // how distant can an interactable be to activate it
    Interactable currentInteractableInReach;


    void Start()
    {
        mover = GetComponent<PlayerMover>();
        nextInteractableScannTime = Time.time + interactableScannIntervall;
    }

    private void Update()
    {
        CheckForInteractables();


        //calculate movement velocity as a 3d Vector
        float _xMov = Input.GetAxis("Horizontal"); //the Raw is used when we want some smoothing, when we want full control
        float _zMov = Input.GetAxis("Vertical");  //both will go from -1 to 1


        Vector3 _moveHorizontal = transform.right * _xMov;  //Vector right is (1, 0, 0)
        Vector3 _moveVertical = transform.forward * _zMov;     //Vector front is (0, 0, 1)


        //Final Movement Vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized;


        if (Input.GetKey(KeyCode.LeftShift) && _zMov > 0)
        {
            if(mover.IsGrounded()) mover.Move(_velocity, true);
            if (movAdder < 5) movAdder += 0.1f;
            _zMov += movAdder;  //raise the z Mov a bit

        }
        else
        {

            if (movAdder > 0) movAdder -= 0.1f;
            else movAdder = 0;
            _zMov += movAdder;
            //if (mover.IsGrounded())
                mover.Move(_velocity, false);
        }

        //animations
        if (mover.IsGrounded())
        {
            animator.SetBool("grounded", true);
            animator.SetFloat("VelZ", _zMov);
            animator.SetFloat("VelX", _xMov);
        }
        else
        {
            animator.SetBool("grounded", false);
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
            if(mover.IsGrounded())mover.Jump();
        }

        //check for 3rd person Camera enabled
        if (Input.GetKeyDown(KeyCode.T))
        {
            mover.ChangeCamera();
        }

        //use an interactible if aviable
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentInteractableInReach != null) currentInteractableInReach.Interact();
        }


    }

    void CheckForInteractables()
    {
        if(Time.time > nextInteractableScannTime)
        {
            nextInteractableScannTime = Time.time + interactableScannIntervall;

            if (currentInteractableInReach != null) currentInteractableInReach.HideUI();
            currentInteractableInReach = null;

            float nearestDistance = float.PositiveInfinity;
            Interactable nearestInteractable = null;

            foreach (Interactable interactable in GameController.Instance.GetAllInteractables())
            {
                float currentDistance = Vector3.Distance(interactable.gameObject.transform.position, (transform.position + transform.forward));
                if (currentDistance < interactableScanDistance)
                {
                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestInteractable = interactable;
                    }
                        
                }
            }

            currentInteractableInReach = nearestInteractable;

            if (currentInteractableInReach!=null) currentInteractableInReach.ShowUI();
        }
        
    }

}
