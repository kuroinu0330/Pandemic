using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �I�[�f�B�I�\�[�X
    private AudioSource bgmAudioSource;
    // SE�͉����d�Ȃ�\��������̂Ń��X�g�ɂ���
    private List<AudioSource> seAudioSourceList = new List<AudioSource>();

    // BGM�p�N���b�v
    [SerializeField]
    private List<AudioClip> bgmClipList = new List<AudioClip>();
    // SE�p�N���b�v
    [SerializeField]
    private List<AudioClip> seClipList = new List<AudioClip>();

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
        DontDestroyOnLoad(gameObject);
        PlayBGM(0);
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
        bgmAudioSource.volume = 0.5f;
        bgmAudioSource.Play();
    }

    // seClipList�̈����Ԗڂɓo�^���Ă��鉹���Đ�
    public void PlaySE(int i)
    {
        var audioSource = GetUnusedAudioSource();
        var clip = seClipList[i];
        if (audioSource == null)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();


        // SE�̍Đ����I�������audioSource���폜
        //StartCoroutine(DestroyAudioSourceWhenFinished(audioSource));
    }

    public void TemporaryStopSE()
    {
        seAudioSourceList[0].Stop();
    }

    public void AllMute()
    {
        seAudioSourceList[0].Stop();
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
