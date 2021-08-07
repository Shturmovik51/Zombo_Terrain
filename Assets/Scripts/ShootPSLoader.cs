using UnityEngine;

public class ShootPSLoader : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        ps.Play();
    }
}
