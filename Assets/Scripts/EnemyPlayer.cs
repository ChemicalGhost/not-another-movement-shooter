using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{

    [SerializeField] public int health = 100;
    [SerializeField] TextMeshPro healthText;

    void Start()
    {
        DisplayHealth();
    }

    void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth()
    {
        healthText.text = health.ToString();
    }


}
