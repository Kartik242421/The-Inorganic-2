using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image AmtImage;
    [SerializeField] TextMeshProUGUI AmtText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateHealth(float health, float delta, float maxHealth)
    {
        AmtImage.fillAmount = health / maxHealth;
        AmtText.SetText(health.ToString());
    }
}
