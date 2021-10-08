//using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//public class BGSound : MonoBehaviour
//{
//    [SerializeField] private AudioClip[] _audioClips;
//    private AudioSource _audioSource;
//    private AudioClip _currentClip;

//    private void Start()
//    {
//        _audioSource = GetComponent<AudioSource>();
//    }

//    private void Update()
//    {
//        if (!_audioSource.isPlaying)
//        {
//            _currentClip = _audioClips[Random.Range(0, _audioClips.Length)];
//            _audioSource.clip = _currentClip;
//            _audioSource.Play();
//        }
//    }
//}
