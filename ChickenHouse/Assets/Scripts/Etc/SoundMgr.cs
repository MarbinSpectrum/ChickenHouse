using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : AwakeSingleton<SoundMgr>
{
    [SerializeField] private Dictionary<Sound, AudioClip> sounds;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    private Dictionary<Sound, AudioSource> loopSE = new Dictionary<Sound, AudioSource>();
    private List<Sound> seList = new List<Sound>();

    private void Update()
    {
        //�� �����ӿ� ������ ���常 ����
        foreach(Sound sound in seList)
        {
            if(sounds.ContainsKey(sound))
            {
                AudioClip clip = sounds[sound];
                se.PlayOneShot(clip);
            }
        }
        seList.Clear();
    }

    public void PlaySE(Sound sound)
    {
        //sound�� �ش��ϴ� ���带 ȿ���� ����Ʈ�� ���
        seList.Add(sound);
    }

    public void PlayLoopSE(Sound sound)
    {
        //�ݺ��Ǿ���ϴ� ȿ����ó��
        if (sounds.ContainsKey(sound) == false)
        {
            //�ش� ���尡 ���������ʴ´�.
            return;
        }

        AudioClip clip = sounds[sound];
        AudioSource audio = null;

        if (loopSE.ContainsKey(sound))
        {
            //���� ������Ʈ�������� ����
            audio = loopSE[sound];
        }

        if (audio == null)
        {
            //�ش� ���� ������Ʈ�� �����Ƿ� ����
            loopSE[sound] = Instantiate(se, transform);
            audio = loopSE[sound];
            audio.transform.name = sound.ToString();
        }

        audio.clip = clip;
        audio.Play();
        audio.loop = true;
    }

    public void StopLoopSE(Sound sound)
    {
        //�ݺ�ȿ������ �����.
        if (loopSE.ContainsKey(sound))
        {
            //���� ������Ʈ�� �ش� ���尡 ������ �����.
            loopSE[sound].Stop();
        }
    }

    public void PlayBGM(Sound sound)
    {
        //sound�� �ش��ϴ� ���带 ��������� ����
        if (sounds.ContainsKey(sound))
        {
            AudioClip clip = sounds[sound];
            bgm.clip = clip;
            bgm.Play();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }
}
