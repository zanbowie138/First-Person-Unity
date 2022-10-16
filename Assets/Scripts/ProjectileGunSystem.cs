using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileGunSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    private int bulletsLeft, bulletsShot;

    private bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI ammoDisplay;

    public AudioSource audioFire;
    public AudioSource audioReload;

    public bool allowInvoke = true;

    private void Awake() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update() {
        myInput();
        if (ammoDisplay != null) {
            if (!reloading) {
                ammoDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
            }
            else {
                ammoDisplay.SetText("Reloading...");
            }
        }
        transform.rotation = fpsCam.transform.rotation;
            
    }

    private void myInput() {
        if (allowButtonHold) {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading && bulletsLeft < magazineSize) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        if (readyToShoot && !reloading && shooting && bulletsLeft > 0) {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot() {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //middle of screen

        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray,out hit)) {
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.GetPoint(1000); //just point far away from character
        }
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calc spread
        float xSpread = Random.Range(-spread,spread);
        float ySpread = Random.Range(-spread,spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(xSpread,ySpread,0);

        GameObject currentBullet = Instantiate(bullet,attackPoint.position,attackPoint.rotation); //instantiate bullet
        currentBullet.transform.forward = directionWithoutSpread.normalized;  //rotate bullet
        Debug.Log(directionWithoutSpread.normalized.ToString());
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke) {
            Invoke("resetShot",timeBetweenShooting);
            allowInvoke = false;
        }
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0) {
            Invoke("Shoot", timeBetweenShots);
        }

        if (muzzleFlash != null) {
            muzzleFlash.Play();
        }
        audioFire.Play();
    }

    private void resetShot () {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload() {
        reloading = true;
        audioReload.Play();
        Invoke("ReloadFinished",reloadTime);
    }

    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
