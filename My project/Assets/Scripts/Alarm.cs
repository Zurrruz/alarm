using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _volumeUpStep;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource.volume = _minVolume;
    }

    public void StartAlarm()
    {
        StopCurrentCoroutine();

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _currentCoroutine = StartCoroutine(ChangesVolume(_maxVolume));
    }

    public void StopAlarm()
    {
        StopCurrentCoroutine();

        _currentCoroutine = StartCoroutine(ChangesVolume(_minVolume));
    }

    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }   

    private IEnumerator ChangesVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeUpStep * Time.deltaTime);

            yield return null;
        }

        if(_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}
