using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechSoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource attentionVoiceClip;
    [SerializeField]
    AudioSource criticalDamVoiceClip;
    [SerializeField]
    AudioSource outOfAmmoVoiceClip;
    [SerializeField]
    AudioSource weaponOverHeatVoiceClip;

    [SerializeField]
    AudioSource warningSound;
    [SerializeField]
    AudioSource nanoPakRepairSound;

    public bool warningSoundPlaying;

    // Start is called before the first frame update
    void Start()
    {
        warningSoundPlaying = false;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttentionVoiceClip()
    {
        attentionVoiceClip.Play();
    }

    public void PlayCriticalDamVoiceClip()
    {
        criticalDamVoiceClip.Play();
    }

    public void PlayOutOfAmmonVoiceClip()
    {
        outOfAmmoVoiceClip.Play();
    }

    public void PlayWeaponOverHeatVoiceClip()
    {
        weaponOverHeatVoiceClip.Play();
    }

    public void StartWarningClip()
    {
        warningSound.Play();
        warningSoundPlaying = true;
    }

    public void EndWarningClip()
    {
        warningSound.Stop();
        warningSoundPlaying = false;
    }

    public void PlayNanoRepairClip()
    {
        nanoPakRepairSound.Play();
    }


}
