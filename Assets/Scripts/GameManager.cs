using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        enemyObject.GetComponent<EnemyPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
