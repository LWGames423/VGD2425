using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Kill : MonoBehaviour
{
    private int currentCharacter;
    public List<int> killable;
    private PlayerManager pm;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        currentCharacter = pm.currentCharacter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (killable.Count == 0 || killable.Contains(currentCharacter)))
        {
            pm.Respawn();
        }
    }
}
