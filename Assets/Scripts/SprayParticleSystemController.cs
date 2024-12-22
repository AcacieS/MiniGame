using UnityEngine;

public class SprayParticleSystemController : MonoBehaviour
{
    private ParticleSystem sprayParticleSystem;
    [SerializeField] private GameObject vfx;

    void Start()
    {
        sprayParticleSystem = GetComponent<ParticleSystem>();
        var mainModule = sprayParticleSystem.main;
        mainModule.cullingMode = ParticleSystemCullingMode.AlwaysSimulate; // Prevent culling
        var emission = sprayParticleSystem.emission; // Access the emission module
        TurnOffParticles();
    }

    public void TurnOnParticles()
    {
        var emission = sprayParticleSystem.emission; // Access the emission module
        emission.enabled = true; // Start emitting particles
    }

    public void TurnOffParticles()
    {
        var emission = sprayParticleSystem.emission; // Access the emission module
        emission.enabled = false; // Start emitting particles
    }
}
