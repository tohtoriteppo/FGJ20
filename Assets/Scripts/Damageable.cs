﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class Damageable : MonoBehaviour
{
    public float maxHP;
    public float HP;
    public GameObject healthBar;


    private AudioSource audioSource;
    public AudioClip[] damageSounds;
    public AudioClip[] repairSounds;

    private float lastSoundPlayed = 0;

    public virtual void Start()
    {
        HP = maxHP;
        if (healthBar)
        {
            healthBar = Instantiate(healthBar, FindObjectOfType<Canvas>().transform);
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = new Vector2(pos.x, pos.y + 40);
            healthBar.GetComponent<Slider>().value = HP;
        }
        UpdateHealthBar();
        audioSource = gameObject.AddComponent<AudioSource>();
        
    }

    public virtual float Repair(float value)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + value * (1 - ((HP-1) / (maxHP))), maxHP);
        float repairAmount = HP - oldHP;
        PlayRepairSound();
        UpdateHealthBar();
        UpdateState();
        return repairAmount; // Return amount repaired
    }

    public  virtual float Damage(float value)
    {
        float oldHP = HP;
        HP = Mathf.Max(HP - value, 0);
        float damageAmount = oldHP - HP;
        PlayDamageSound();
        UpdateHealthBar();
        UpdateState();
        return damageAmount; // Return amount damaged
    }

    protected virtual void UpdateHealthBar()
    {
        if(healthBar != null)
        {
            if (healthBar) healthBar.GetComponent<Slider>().value = HP / maxHP * 100;
            if (HP == maxHP) healthBar.SetActive(false);
            else healthBar.SetActive(true);
        }
        
    }

    protected abstract void UpdateState();

    protected virtual void PlayDamageSound()
    {
        PlayRandomSound(damageSounds);
    }

    protected virtual void PlayRepairSound()
    {
        PlayRandomSound(repairSounds);
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length == 0) return;
        if (Time.time - lastSoundPlayed > 0.1f)
        {
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            lastSoundPlayed = Time.time;
        }   
    }


    public virtual bool CanBeRepaired()
    {
        return true;
    }
}

