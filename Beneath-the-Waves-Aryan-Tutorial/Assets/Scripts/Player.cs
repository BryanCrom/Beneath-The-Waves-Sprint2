using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float MAXHP = 100f;
    public float HP;
    public float chipSpeed = 2f;

    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;

    //cooldown to take damage
    public bool canTakeDamage = false;
    public float damageCooldown = 1.5f;

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

        if (!canTakeDamage) return;
        HP -= damage;

        if (HP <= 0f)
        {
            print("YOU ARE DEAD!");
        }
        else
        {
            print("HIT!");
        }

        StartCoroutine(DamageCooldown());
        lerpTimer = 0f;
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
}