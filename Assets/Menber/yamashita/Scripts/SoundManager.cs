using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // オーディオソース
    private AudioSource bgmAudioSource;
    // SEは音が重なる可能性があるのでリストにする
    private List<AudioSource> seAudioSourceList = new List<AudioSource>();

    // BGM用クリップ
    [SerializeField]
    private List<AudioClip> bgmClipList = new List<AudioClip>();
    // SE用クリップ
    [SerializeField]
    private List<AudioClip> seClipList = new List<AudioClip>();

    void Awake()
    {
        // スピーカーを用意
        bgmAudioSource = gameObject.AddComponent<AudioSource>();

        List<AudioSource> AudiioList = new List<AudioSource>();

        // 配列の数だけAudioSourceを生成して格納
        for (int i = 0; i < seAudioSourceList.Count; i++)
        {
            //seAudioSourceList.Add(gameObject.AddComponent<AudioSource>());

            AudiioList.Add(gameObject.AddComponent<AudioSource>());
        }

        seAudioSourceList = AudiioList;

        // シーン遷移しても破棄されない
        DontDestroyOnLoad(gameObject);
        PlayBGM(0);
    }

    // 未使用のAudioSourceを取得
    private AudioSource GetUnusedAudioSource()
    {
        for (int i = 0; i < seAudioSourceList.Count; i++)
        {
            if (!seAudioSourceList[i].isPlaying)
            {
                return seAudioSourceList[i];
            }
        }

        AudioSource audio = gameObject.AddComponent<AudioSource>();
        seAudioSourceList.Add(audio);
        return audio;
    }

    // bgmClipListの引数番目に登録してある曲を再生
    public void PlayBGM(int i)
    {
        var clip = bgmClipList[i];
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    // seClipListの引数番目に登録してある音を再生
    public void PlaySE(int i)
    {
        var audioSource = GetUnusedAudioSource();
        var clip = seClipList[i];
        if (audioSource == null)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();


        // SEの再生が終わったらaudioSourceを削除
        StartCoroutine(DestroyAudioSourceWhenFinished(audioSource));
    }

    private IEnumerator DestroyAudioSourceWhenFinished(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        // SEの再生が終わったら関連するAudioSourceを削除
        seAudioSourceList.Remove(audioSource);
        Destroy(audioSource);
    }
}
