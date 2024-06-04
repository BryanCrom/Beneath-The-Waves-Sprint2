using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float MAXHP = 100f;
    public float HP;
    public float chipSpeed = 2f;

    private float lerpTimer;
    public UnityEngine.UI.Image frontHealthBar;
    public UnityEngine.UI.Image backHealthBar;

    public GameObject DeathMsg;

    //cooldown to take damage
    public bool canTakeDamage = false;
    public float damageCooldown = 1.5f;

    //player death
    public bool isPDead;
    public GameObject bloodScreen;

    //sounds
    public AudioClip hurtSound;
    public AudioSource src;
    public void Start()
    {
        HP = MAXHP;
    }

    void Update()
    {
        HP = Mathf.Clamp(HP, 0, MAXHP);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = HP / MAXHP;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.grey;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void takeDamage(float damage)
    {
        src.clip = hurtSound;
        src.Play();

        if (!canTakeDamage) return;
        HP -= damage;

        if (HP <= 0f)
        {
            print("YOU ARE DEAD!");
            PlayerDead();
            isPDead = true;
        }
        else
        {
            print("HIT!");
            StartCoroutine(BloodyScreen());
        }

        StartCoroutine(DamageCooldown());
        lerpTimer = 0f;
    }

    private IEnumerator BloodyScreen()
    {
        if (bloodScreen.activeInHierarchy == false)
        {
            bloodScreen.SetActive(true);
        }

        //Blood Fading Effect, copy pasted from online resource
        var image = bloodScreen.GetComponentInChildren<UnityEngine.UI.Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodScreen.activeInHierarchy)
        {
            bloodScreen.SetActive(false);   
        }
    }

    public void healDamage(float heal)
    {
        HP += heal;
        if (HP > MAXHP)
        {
            HP = MAXHP;
        }
        lerpTimer = 0f;
    }

    public float getHP()
    {
        return HP;
    }

    public float getMAXHP()
    {
        return MAXHP;
    }

    //add cooldown 
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void PlayerDead()
    {
        //disable the scripts for movement 
        GetComponent<MouseLook>().enabled = false;
        GetComponent<PlayerMove>().enabled = false;

        //dying animation, the camera falls in the floor
        GetComponentInChildren<Animator>().enabled = true;

        //get death screen
        GetComponent<DeathScreen>().StartFade();

        StartCoroutine(ShowDeathMsg());
    }

    private IEnumerator ShowDeathMsg()
    {
        yield return new WaitForSeconds(1f);
        DeathMsg.gameObject.SetActive(true);
    }
}