using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �I�[�f�B�I�\�[�X
    private AudioSource bgmAudioSource;
    // SE�͉����d�Ȃ�\��������̂Ń��X�g�ɂ���
    [SerializeField]
    private List<AudioSource> seAudioSourceList = new List<AudioSource>();

    // BGM�p�N���b�v
    [SerializeField]
    private List<AudioClip> bgmClipList = new List<AudioClip>();
    // SE�p�N���b�v
    [SerializeField]
    private List<AudioClip> seClipList = new List<AudioClip>();

    public static SoundManager instance;

    void Awake()
    {
        // �X�s�[�J�[��p��
        bgmAudioSource = gameObject.AddComponent<AudioSource>();

        List<AudioSource> AudiioList = new List<AudioSource>();

        // �z��̐�����AudioSource�𐶐����Ċi�[
        for (int i = 0; i < seAudioSourceList.Count; i++)
        {
            //seAudioSourceList.Add(gameObject.AddComponent<AudioSource>());

            AudiioList.Add(gameObject.AddComponent<AudioSource>());
        }

        seAudioSourceList = AudiioList;

        // �V�[���J�ڂ��Ă��j������Ȃ�
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            for (int i = 0; i < seClipList.Count; i++)
            {
                seAudioSourceList.Add(this.gameObject.AddComponent<AudioSource>());
                seAudioSourceList[i].clip = seClipList[i];
                seAudioSourceList[i].loop = (i == 0) ? true : false;
                seAudioSourceList[i].volume = 0.5f;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        //PlayBGM(0);
    }

    // ���g�p��AudioSource���擾
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

    // bgmClipList�̈����Ԗڂɓo�^���Ă���Ȃ��Đ�
    public void PlayBGM(int i)
    {
        var clip = bgmClipList[i];
        bgmAudioSource.clip = clip;
        bgmAudioSource.volume = 1.25f;
        bgmAudioSource.Play();
    }

    // seClipList�̈����Ԗڂɓo�^���Ă��鉹���Đ�
    public void PlaySE(int i)
    {
        // var audioSource = GetUnusedAudioSource();
        // var clip = seClipList[i];
        // if (audioSource == null)
        // {
        //     return;
        // }
        // audioSource.clip = clip;
        // audioSource.loop = true;
        // audioSource.volume = 0.5f;

        switch (i)
        {
            case 0:
                seAudioSourceList[i].Play();
                break;
            case 1:

                if (seAudioSourceList[i].isPlaying)
                {
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    seAudioSourceList.Add(audioSource);
                    audioSource.clip = seClipList[1];
                    audioSource.Play();
                    StartCoroutine(DestroyAudioSourceWhenFinished(audioSource));
                }
                else
                {
                    seAudioSourceList[i].Play();
                }

                break;
        }


        // SE�̍Đ����I�������audioSource���폜
        //StartCoroutine(DestroyAudioSourceWhenFinished(audioSource));
    }

    public void TemporaryStopSE(int i)
    {
        seAudioSourceList[i].Stop();
    }

    public void AllMute()
    {
        for (int i = 0; i < seAudioSourceList.Count; i++)
        {
            seAudioSourceList[i].Stop();
        }
        bgmAudioSource.Stop();
    }

    private IEnumerator DestroyAudioSourceWhenFinished(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        // SE�̍Đ����I�������֘A����AudioSource���폜
        seAudioSourceList.Remove(audioSource);
        Destroy(audioSource);
    }
}
