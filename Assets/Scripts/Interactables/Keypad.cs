using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    public GameObject boss;
    public Transform spawnPoint;

    private GameObject currentBoss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {

        if (currentBoss == null)
        {
            currentBoss = Instantiate(boss, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
