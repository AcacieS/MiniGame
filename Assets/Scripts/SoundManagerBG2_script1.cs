using UnityEngine;

public class SoundManagerBG2_script : MonoBehaviour
{
    public static  SoundManagerBG2_script instance { get; private set; } //to get music in other group
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
