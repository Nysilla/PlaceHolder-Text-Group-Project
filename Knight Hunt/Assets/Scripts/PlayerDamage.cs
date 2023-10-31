using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] float distance = 3, damageDelay = 1;
    [SerializeField] int damageAmount;

    [Header("Attack SFX")]
    [SerializeField] private AudioClip[] attackSFX;
    [SerializeField] private float audioLength = 1;

    private AudioSource audioSource;
    float timer;
    [HideInInspector] public bool playerCloseToEnemy;

    public int Song { get; set; } = 0;
    //public float SongLength { get; set; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!GameObject.FindWithTag("Enemy"))
        {
            return;
        }

        Vector3 enemyPosition = GameObject.FindWithTag("Enemy").transform.position - transform.position;

        if (enemyPosition.magnitude < distance && Input.GetKeyDown(KeyCode.Mouse0) && timer >= damageDelay)
        {
            GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            timer = 0;

            if (Song != attackSFX.Length)
            {
                PlayNextSFX(attackSFX);
            }
        }
    }

    private void PlayNextSFX(AudioClip[] audios)
    {
        if (Song < audios.Length - 1)
        {
            Song++;
        }
        else
        {
            Song = 0;
        }
        audioSource.PlayOneShot(audios[Song]);
        //SongLength = 0;
    }
}
