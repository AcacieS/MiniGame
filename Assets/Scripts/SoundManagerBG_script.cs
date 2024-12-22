using UnityEngine;

public class SoundManagerBG_script : MonoBehaviour
{
    public static  SoundManagerBG_script instance { get; private set; } //to get music in other group
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
    public void SetPitch(){
        source.pitch += Time.deltaTime * 0.01f;
    }

}
