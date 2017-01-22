using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AgentController : MonoBehaviour {
    private CharacterController characterController;

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forwardDirection = camera.forward;
        Vector3 rightDirection = camera.right;
        forwardDirection.Scale(new Vector3(1, 0, 1));
        rightDirection.Scale(new Vector3(1, 0, 1));

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 aimDirection = transform.forward;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask, QueryTriggerInteraction.Collide))
        {
            aimDirection = (hit.point - transform.position);
            aimDirection.Scale(new Vector3(1,0,1));
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
            Debug.Log(hit.collider.gameObject.tag);
            transform.forward = aimDirection.normalized;
        }

        Vector3 direction = (vertical * forwardDirection + horizontal * rightDirection).normalized;
        Debug.DrawRay(transform.position, direction, Color.blue);
        characterController.Move(direction * speed * Time.deltaTime);
        animator.SetFloat("Speed", (direction * speed).magnitude);

        float angle = Vector3.Angle(direction, aimDirection);
        Vector3 cross = Vector3.Cross(direction, aimDirection);
        angle *= cross.y < 0 ? -1 : 1;
        //Debug.Log(cross);
        Debug.Log(angle);
        animator.SetFloat("Angle", angle);
    }
}
