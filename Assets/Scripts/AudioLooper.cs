using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    public List<AudioClip> startClips;
    public List<AudioClip> loopClips;
    public List<AudioClip> endClips;

    public bool waitUntilLoopEnds = true;

    private AudioSource audioSource;
    private bool isLooping = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartAudioLoop()
    {
        StopAllCoroutines();

        if (startClips.Count > 0)
        {
            AudioClip startClip = startClips[Random.Range(0, startClips.Count)];
            audioSource.clip = startClip;
            audioSource.loop = false;
            audioSource.Play();
            StartCoroutine(WaitForClipToEndThenStartLoop(startClip.length));
        }
        else
        {
            StartLoop();
        }
    }

    private void StartLoop()
    {
        if (loopClips.Count > 0)
        {
            isLooping = true;
            StartCoroutine(LoopAudioClips());
        }
    }

    public void EndAudioLoop()
    {
        if (isLooping)
        {
            isLooping = false;
            if (waitUntilLoopEnds && audioSource.isPlaying)
            {
                StartCoroutine(WaitForLoopToEndThenStopOrPlayEndClip());
            }
            else
            {
                StopLoopingAudio();
                PlayEndClip();
            }
        }
        else
        {
            PlayEndClip();
        }
    }

    private IEnumerator WaitForClipToEndThenStartLoop(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        StartLoop();
    }

    private IEnumerator LoopAudioClips()
    {
        while (isLooping && loopClips.Count > 0)
        {
            AudioClip loopClip = loopClips[Random.Range(0, loopClips.Count)];
            audioSource.clip = loopClip;
            audioSource.loop = false;
            audioSource.Play();

            yield return new WaitForSeconds(loopClip.length);
        }
    }

    private IEnumerator WaitForLoopToEndThenStopOrPlayEndClip()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        StopLoopingAudio();
        PlayEndClip();
    }

    private void StopLoopingAudio()
    {
        audioSource.Stop();
    }

    private void PlayEndClip()
    {
        if (endClips.Count > 0 && !isLooping)
        {
            AudioClip endClip = endClips[Random.Range(0, endClips.Count)];
            audioSource.clip = endClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
