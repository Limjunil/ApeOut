using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // watch?v=s_v9JnTDCCY

    public float time = 2f;

    public float cubeSize = 0.2f;
    public int cubesInRow = default;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public GameObject piece;
    public GameObject windowPiecesObj;

    public List<GameObject> windowPiecesList = new List<GameObject>();

    //! 비활성화 되면 모든 코루틴 꺼라
    private void OnDisable() 
    {
        StopAllCoroutines();
    }

    void Start()
    {
        // calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        // use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        cubesInRow = 4;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Explode();
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("PlayerAttack")) 
    //    {
    //        Explode();
    //    }
    //}

    public void Explode() 
    {
        // make object disapper
        gameObject.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        // loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) 
        {
            for (int y = 0; y < cubesInRow; y++) 
            {
                for (int z = 0; z < cubesInRow; z++) 
                {
                    CreatePiece(x, y, z);
                }
            }
        }

        // get explosion position
        Vector3 explosionPos = transform.position;
        // get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        // add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) 
        {
            // get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                // add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
        StartCoroutine(AfterClose());

        //gameObject.SetActive(false);

    }

    void CreatePiece(int x, int y, int z) 
    {
        // create piece
        //GameObject piece;

        //piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject pieceTemp_ = Instantiate(piece, windowPiecesObj.transform);

        windowPiecesList.Add(pieceTemp_);

        // set piece position and scale
        pieceTemp_.transform.position = transform.position + new Vector3
            (cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;

        //Debug.Log($"{pieceTemp_.transform.localScale}");

        pieceTemp_.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //Debug.Log($"{pieceTemp_.transform.localScale}");


        // add rigidbody and set mass
        pieceTemp_.AddComponent<Rigidbody>();
        pieceTemp_.GetComponent<Rigidbody>().mass = cubeSize;

        //Debug.Log($"{pieceTemp_.transform.localScale}");

    }

    IEnumerator AfterClose() 
    {
        yield return new WaitForSeconds( 2.0f );
        foreach (GameObject closeWin in windowPiecesList)
        {
            closeWin.SetActive(false);
        }
    }
}
