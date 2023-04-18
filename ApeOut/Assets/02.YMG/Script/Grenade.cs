using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = default;
    public float fuse = default;

    public GameObject explosionEffect;
    public GameObject useEffect;

    private float countdown;
    private float firing;
    bool hasExploded = false;

    public float effectTime;

    void Start()
    {
        countdown = delay;
        firing = fuse;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        firing -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded) 
        {
            Debug.Log("anyKeyDown");
            Explode();
            hasExploded = true;
        }

        //if (firing <= 0f && hasExploded == true)
        //{
        //    Debug.Log("false");
        //    useEffect.SetActive(false);

        //}

    }

    void Explode() 
    {
        // show effect

        useEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Debug.Log($" 원본 이름 : {explosionEffect}, 복제된 이름 : {useEffect}");

        //effectTime = useEffect.GetComponent<ParticleSystem>().duration;
        // get nearby objects
        // add force
        // damage

        // remove grenade
        //test_.SetActive(false);

    }
}
