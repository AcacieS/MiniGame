
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance { get; private set; } //to get music in other group
    private AudioSource source;
   
    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
