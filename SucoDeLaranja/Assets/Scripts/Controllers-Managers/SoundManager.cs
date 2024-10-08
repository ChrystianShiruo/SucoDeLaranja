using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    [SerializeField] private SoundData _soundData;

    private List<AudioSource> _busyAudioSources;
    private List<AudioSource> _availableAudioSources;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        _availableAudioSources = new List<AudioSource>();
        _busyAudioSources = new List<AudioSource>();
    }
    private void Start() {

        GameController.Instance.OnMatch += PlayMatchSound;
        GameController.Instance.OnMismatch += PlayMismatchSound;
        GameController.Instance.OnGameOver += PlayGameOverSound;
        CardsManager.Instance.OnSelectFirstCard += PlayFlipSound;
        
    }

    private void PlayMatchSound() {
        PlaySound(AudioClipData.SFXType.Match);
    }
    private void PlayMismatchSound() {
        PlaySound(AudioClipData.SFXType.Mismatch);
    }
    private void PlayFlipSound() {
        PlaySound(AudioClipData.SFXType.Flip);
    }
    private void PlayGameOverSound() {
        KillAllSounds();
        PlaySound(AudioClipData.SFXType.GameOver);
    }

    private void KillAllSounds() {
        _busyAudioSources.ForEach(aSource => aSource.Stop());
    }

    public void PlaySound(AudioClipData.SFXType sfxType) {
        PlaySound(_soundData.AudioClipList.Find(clipData =>  clipData.SfxType == sfxType).AudioClip);
    }
    public void PlaySound(AudioClip clip) {
        AudioSource audioSource = GetAvailableAudioSource();
        audioSource.volume = _soundData.volume;
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(ManageAudioSourceRoutine(audioSource));
    }

    private AudioSource GetAvailableAudioSource() {
        if(_availableAudioSources.Count == 0) {
            GameObject go = Instantiate(new GameObject(), transform);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            _availableAudioSources.Add(audioSource);
        }
        return _availableAudioSources[0];
    }

    private IEnumerator ManageAudioSourceRoutine(AudioSource audioSource) {

        _busyAudioSources.Add(audioSource);
        _availableAudioSources.Remove(audioSource);
        while(audioSource.isPlaying == true) {
            yield return new WaitForSeconds(.2f);
        }

        _busyAudioSources.Remove(audioSource);
        _availableAudioSources.Add(audioSource);
    }
}
