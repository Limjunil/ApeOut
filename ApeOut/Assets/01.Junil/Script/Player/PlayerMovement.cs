using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody playerRigid = default;

    public float playerMoveSpeed = default;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        playerMoveSpeed = 10f;
    }


    public void OnMove(float inputX_, float inputZ_)
    {
        Vector3 inputData_ = new Vector3(inputX_, 0f, inputZ_).normalized;

        playerRigid.velocity = inputData_ * playerMoveSpeed;
    }
}
