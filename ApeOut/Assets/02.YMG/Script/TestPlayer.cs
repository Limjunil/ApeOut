using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    float speed = 8.0f;

    Rigidbody rig;
    Camera viewCamera;
    Vector3 velocity;
    float rotDegree;

    void Awake()
    {
        //Insert this code inside Awake()
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), GetComponentsInChildren<CapsuleCollider>()[1]);
    }

    void Start()
    {
        rig = transform.GetComponent<Rigidbody>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = viewCamera.ScreenToWorldPoint(mousePos);

        float dz = mousePos.z - rig.position.z;
        float dx = mousePos.x - rig.position.x;
        rotDegree = -(Mathf.Rad2Deg * Mathf.Atan2(dz, dx) - 90);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
    }

    void FixedUpdate()
    {
        rig.MoveRotation(Quaternion.Euler(0, rotDegree, 0));
        rig.MovePosition(rig.position + velocity * Time.fixedDeltaTime);
    }
}
