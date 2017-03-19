﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    private static Dictionary<string, MusicPlayer> _musicPlayers = new Dictionary<string, MusicPlayer>();

    public AudioSource startInterruptSound = null;
    public AudioSource musicLoopSound = null;
    public AudioSource endInterruptSound = null;
    
    private void Awake()
    {
        _musicPlayers[name] = this;
    }

    public static MusicPlayer GetMusicPlayer(string name)
    {
        return _musicPlayers[name];
    }

    public void StartMusic()
    {
        StopAllCoroutines();
        StartCoroutine(StartMusicCoroutine());
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        StartCoroutine(EndMusicCoroutine());
    }

    private IEnumerator StartMusicCoroutine()
    {
        InterruptMusic.Instance.SetMusicInterrupt(true);

        if (startInterruptSound != null)
        {
            startInterruptSound.time = 0.0f;
            startInterruptSound.Play();
        }
        yield return new WaitForSeconds(startInterruptSound.clip.length);

        if (musicLoopSound != null)
        {
            musicLoopSound.time = 0.0f;
            musicLoopSound.Play();
        }
    }

    private IEnumerator EndMusicCoroutine()
    {
        if (musicLoopSound != null)
        {
            musicLoopSound.Stop();
        }

        if (endInterruptSound != null)
        {
            endInterruptSound.time = 0.0f;
            endInterruptSound.Play();
        }
        yield return new WaitForSeconds(startInterruptSound.clip.length);

        InterruptMusic.Instance.SetMusicInterrupt(false);
    }
}