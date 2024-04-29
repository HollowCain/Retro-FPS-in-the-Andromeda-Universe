using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(AudioSource))]
public class Weapon_1 : MonoBehaviour
{
    public GameObject bloodSplat;
    public Sprite idlePistol;
    public Sprite shotPistol;
    public float weaponDamage;
    public float pistolRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public TMP_Text ammoText;

    public int ammoAmount;
    public int ammoClipSize;
    int ammoLeft;
    int ammoClipLeft;

    public GameObject bulletHole;

    bool isShot = false;
    bool isReloading;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
    }
    void OnEnable()
    {
        isReloading = false;
    }
    void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false && Pause_Menu.isPaused == false)
        {
            isShot = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && Pause_Menu.isPaused == false)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
        Vector2 bulletOffset = UnityEngine.Random.insideUnitCircle * Dynamic_Crosshair.spread;
        Vector3 randomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(randomTarget);
        RaycastHit hit;
        if(isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            Dynamic_Crosshair.spread += Dynamic_Crosshair.PISTOL_SHOOTING_SPREAD;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");
            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(bloodSplat, hit.point, Quaternion.identity);    
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState ||
                        hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                    }
                    Debug.Log("Colide with" + hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage("AddDamage", weaponDamage, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                }
            }
        } else if(isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if(ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        } else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        } else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }

    public void AddAmmo(int value)
    {
        ammoLeft += value;
    }
}
