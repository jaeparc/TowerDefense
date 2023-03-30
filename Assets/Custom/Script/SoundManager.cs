using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void playSound(AudioClip clip){
        _effectsSource.PlayOneShot(clip);
    }

    public void playMusic(AudioClip clip){
        _musicSource.PlayOneShot(clip);
    }

    public void stopMusic(){
        _musicSource.Stop();
    }

    public bool isEffectPlaying(){
        return _effectsSource.isPlaying;
    }
}
