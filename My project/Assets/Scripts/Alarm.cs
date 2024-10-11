using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _volumeUpStep;

    private float _currentVolume;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        _audioSource.volume = _minVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCurrentCoroutine();

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        _currentCoroutine = StartCoroutine(ChangesVolume(_maxVolume));
    }

    private void OnTriggerExit2D(Collider2D collision)
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
        float percentage = 0;

        _currentVolume = _audioSource.volume;

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, targetVolume, percentage);
            percentage += Time.deltaTime * _volumeUpStep;

            yield return null;
        }

        if(_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}
