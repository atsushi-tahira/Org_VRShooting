using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySC : MonoBehaviour {

    Ray ray;

	void Update () {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position,transform.forward * 10f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
	}

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,10f))
        {
            string score = hit.collider.gameObject.tag;
            Debug.Log("Score is " + score);
        }
    }
}
