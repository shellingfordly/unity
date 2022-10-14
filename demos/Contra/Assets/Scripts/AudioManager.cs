using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 单例
    public static AudioManager Instance;
    // 音乐播放器
    public AudioSource MusicPlayer;
    // 音效播放器
    public AudioSource SoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void PlaySound(string name) {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.PlayOneShot(clip);
    }
}
