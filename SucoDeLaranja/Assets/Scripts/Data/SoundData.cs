using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "GameData/SoundData")]
public class SoundData : ScriptableObject {

    public List<AudioClipData> AudioClipList { get => _audioClipList; }

    [SerializeField] private List<AudioClipData> _audioClipList;
    public float volume;
}

[Serializable]
public class AudioClipData {
    public enum SFXType {
        Flip,
        Match,
        Mismatch,
        GameOver
    }

    public SFXType SfxType { get => _sfxType; }
    public AudioClip AudioClip { get => _audioClip; }

    [SerializeField] private SFXType _sfxType;
    [SerializeField] private AudioClip _audioClip;

}