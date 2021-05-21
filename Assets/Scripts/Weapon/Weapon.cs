using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    /*public GameObject shootEffectPrefab;*/
    public Transform weapon;
    public GameObject bulletPrefab;
    public Joystick joystick;
    public Button firebutton;
    public Button heButton;
    public GameObject bombPrefab;
    public AudioClip fireSound;
    public GameObject weaponBar;

    public long shootDelayMillis;
    public long throwBombDelayMillis;
    public float weaponAngel = 0f;
    public float buletForce = 10;
    public float throwForce;

    public int bulletsCount;
    public int heCount;

    private void Start()
    {
        weaponBar.GetComponent<WeaponListener>().onBulletChange(bulletsCount);
        weaponBar.GetComponent<WeaponListener>().onHeChange(heCount);
        firebutton.onClick.AddListener(Shoot);
        heButton.onClick.AddListener(ThrowBomb);
    }

    public void addBullets(int count)
    {
        bulletsCount += count;
        weaponBar.GetComponent<WeaponListener>().onBulletChange(bulletsCount);
    }

    public void addHe(int count)
    {
        heCount += count;
        weaponBar.GetComponent<WeaponListener>().onHeChange(heCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.isGamePaused)
            return;
        HandleAiming();
        /*if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }*/
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Shoot();
        }
        /*if (Input.GetButtonDown("Fire2"))
        {
            ThrowBomb();
        }*/
    }


    private long lastFireMillis = 0;
    private long lastThrowBombMillis = 0;

    private void ThrowBomb()
    {
        if (GameApplication.GetInstance().isCountdownRunning)
            return;
        if (heCount == 0)
            return;

        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (lastThrowBombMillis + throwBombDelayMillis > now)
            return;
        var bulletPoint = weapon.GetChild(0);
        var bomb = Instantiate(bombPrefab, bulletPoint.position, bulletPoint.rotation);
        lastThrowBombMillis = now;
        var x = throwForce * Mathf.Cos(weaponAngel * Mathf.Deg2Rad);
        var y = throwForce * Mathf.Sin(weaponAngel * Mathf.Deg2Rad);
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        heCount--;
        weaponBar.GetComponent<WeaponListener>().onHeChange(heCount);
    }
    public void Shoot() 
    {
        if (GameApplication.GetInstance().isCountdownRunning)
            return;
        firebutton.interactable = false;
        if (bulletsCount == 0)
        {
            firebutton.interactable = true;
            return;
        }
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (lastFireMillis + shootDelayMillis > now)
        {
            firebutton.interactable = true;
            return;
        }
        gameObject.GetComponent<AudioSource>().PlayOneShot(fireSound);
        var bulletPoint = weapon.GetChild(0);
        var bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        var weaponAngel = bullet.transform.eulerAngles.z;

        var x = buletForce * Mathf.Cos(weaponAngel * Mathf.Deg2Rad);
        var y = buletForce * Mathf.Sin(weaponAngel * Mathf.Deg2Rad);
        /*var shootEffect = Instantiate(shootEffectPrefab, bulletPoint.position, bulletPoint.rotation);*/
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        lastFireMillis = now;
       /* Destroy(shootEffect, 0.2f);*/
        bulletsCount--;
        weaponBar.GetComponent<WeaponListener>().onBulletChange(bulletsCount);
        firebutton.interactable = true;
    }

    private void HandleAiming() 
    {

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            HandleJoystickAiming();
        else
            HandleMouseAiming();
    }

    private void HandleMouseAiming()
    {
        var mousePosition = Utils.GetMouseWorldPosition();
        var aimDirtection = (mousePosition - transform.position).normalized;
        var angel = Mathf.Atan2(aimDirtection.y, aimDirtection.x) * Mathf.Rad2Deg;
        weapon.eulerAngles = new Vector3(0, 0, angel);
        weaponAngel = weapon.GetChild(0).transform.eulerAngles.z;
    }

    private void HandleJoystickAiming()
    {
        var aimDirtection = new Vector2(joystick.Horizontal, joystick.Vertical);
        var angel = Mathf.Atan2(aimDirtection.y, aimDirtection.x) * Mathf.Rad2Deg;
        weapon.eulerAngles = new Vector3(0, 0, angel);
        weaponAngel = weapon.GetChild(0).transform.eulerAngles.z;
    }
}
