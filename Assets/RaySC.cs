using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySC : MonoBehaviour {

    public AudioClip handGunSE;
    public AudioClip jamSE;
    public AudioClip reloadSE;
    public TextMesh bulletLast;
    public GameObject noBulletText;

    Ray ray;
    AudioSource audioS;
    int ammo = 6;
    int havedBullet = 18;
    int handGunAmmo = 6;


    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        ammo = 6;
    }

    void Update () {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position,transform.forward * 10f);

        //ショット
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            if (ammo != 0)
            {
                Shoot();
            }
            else 
            {
                AudioPlay(jamSE);
                StartCoroutine(NoBullet("弾切れ"));
            }
        }

        //リロード
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioPlay(reloadSE);
            //ammo = 6;
            //havedBullet -= 6;
            for (int i = 0;i < handGunAmmo;i++)
            {
                if (ammo != handGunAmmo && havedBullet != 0)
                {
                    ammo++;
                    havedBullet--;
                }
                else if (havedBullet == 0)
                {
                    StartCoroutine(NoBullet("リロード不可"));
                }
                else if (ammo == handGunAmmo)
                {
                    break;
                }
                
            }
        }

        bulletLast.text = "残弾：" + ammo + "/" + havedBullet ;

	}

    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,10f))
        {
            AudioPlay(handGunSE);

            string score = hit.collider.gameObject.tag;
            Debug.Log("Score is " + score);
            ammo--;
        }
    }

    void AudioPlay(AudioClip source)
    {
        audioS.clip = source;
        audioS.Play();
    }

    IEnumerator NoBullet(string str)
    {
        noBulletText.GetComponent<TextMesh>().text = str;
        noBulletText.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        noBulletText.SetActive(false);
    }
}
