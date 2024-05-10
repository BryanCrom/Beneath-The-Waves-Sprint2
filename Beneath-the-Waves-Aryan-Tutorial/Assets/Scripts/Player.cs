using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private readonly float MAXHP = 100;
    private float HP;
    public float chipSpeed = 2f;

    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;

    void Start()
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
        Debug.Log(HP);
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
        HP -= damage;

        if (HP <= 0)
        {
            print("YOU ARE DEAD!");
        }
        else
        {
            print("HIT!");
        }
        lerpTimer = 0f;
    }
    public void healDamage(float heal)
    {
        HP += heal;
        if(HP > MAXHP)
        {
            HP = MAXHP;
        }
        lerpTimer = 0f;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("EnemyHand"))
        {
            print("Player is colliding w enemyhand");
            takeDamage(other.gameObject.GetComponent<FishmanHand>().damage);
        }
    }

    public float getHP()
    {
        return HP;
    }

    public float getMAXHP()
    {
        return MAXHP;
    }
}
