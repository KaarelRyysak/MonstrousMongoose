using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    CharacterController controller;
    
    private float mouseXInput;
    private float mouseYInput;
    private Transform alpha;

    public float dashSpeed = 50.0f;
    public float speed = .50f;
    public float rotateSpeed = 0.75f;
    public float radius = 8.0f;
    public GameObject target;
    public GameObject target1 = null;



    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        target1 = Instantiate<GameObject>(target, new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z + 1), gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Escape 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Application.Quit();
        }

        //Grab mouse input and move crosshair
        mouseXInput = Input.GetAxis("Mouse X") * rotateSpeed;
        mouseYInput = Input.GetAxis("Mouse Y") * rotateSpeed;
        target1.GetComponent<CharacterController>().Move(new Vector3(mouseXInput, 0, mouseYInput));

        //Grab position of crosshair and point character in that direction
        Vector3 lookAt = target1.transform.position;
        gameObject.transform.LookAt(lookAt);

        //Character Movement
        Vector3 characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (characterMovement != Vector3.zero)
        {
            characterMovement.Normalize();
        }
        controller.Move(characterMovement * speed);
        

        //Clamp crosshair within distance on player
        if (Vector3.Distance(gameObject.transform.position, target1.transform.position) > radius)
        {
            Vector3 v = target1.transform.position - gameObject.transform.position;
            v = Vector3.ClampMagnitude(v, radius);
            Vector3 clampedLocale = gameObject.transform.position + v;
            target1.transform.SetPositionAndRotation(clampedLocale, new Quaternion());
        }

        //Dash attack when right mouse is pressed
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
                Vector3 dashAt = new Vector3(target1.transform.position.x, 1, target1.transform.position.z);
                gameObject.transform.position = dashAt;
        }

        //Dash when Left Mouse Button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Linecast(gameObject.transform.position, target1.transform.position, out hit))
            {
                Vector3 dashAt = ((hit.point + hit.normal) - gameObject.transform.position);
                gameObject.GetComponent<CharacterController>().Move(dashAt * dashSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 dashAt = target1.transform.position - gameObject.transform.position;
                gameObject.GetComponent<CharacterController>().Move(dashAt * dashSpeed * Time.deltaTime);
            }
        }
    }
}

