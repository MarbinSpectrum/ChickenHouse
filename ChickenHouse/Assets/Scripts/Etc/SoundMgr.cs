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

    public float bgmValue   { get; private set; }
    public float seValue    { get; private set; }

    private const string BGM_KEY = "BGM";
    private const string SE_KEY = "SE";

    protected override void Awake()
    {
        base.Awake();
        seValue     = PlayerPrefs.GetFloat(SE_KEY, 0.5f);
        bgmValue    = PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
        SetSE_Volume(seValue);
        SetBGM_Volume(bgmValue);
    }

    private void Update()
    {
        //�� �����ӿ� ������ ���常 ����
        foreach(Sound sound in seList)
        {
            if(sounds.ContainsKey(sound))
            {
                AudioClip clip = sounds[sound];
                se.volume = seValue;
                se.PlayOneShot(clip);
            }
        }
        seList.Clear();
    }

    public void SetSE_Volume(float v)
    {
        PlayerPrefs.SetFloat(SE_KEY, v);
        seValue = v;
        se.volume = bgmValue;
    }

    public void SetBGM_Volume(float v)
    {
        PlayerPrefs.SetFloat(BGM_KEY, v);
        bgmValue = v;
        bgm.volume = bgmValue;
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


        audio.volume = seValue;
        audio.clip = clip;
        if (audio.isPlaying == false)
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

    public void StopSE()
    {
        foreach (var pair in loopSE)
        {
            pair.Value.Stop();
        }
        se.Stop();
    }

    public void MuteSE(bool state)
    {
        foreach(var pair in loopSE)
        {
            pair.Value.mute = state;
        }
        se.mute = state;
    }

    public void PlayBGM(Sound sound)
    {
        //sound�� �ش��ϴ� ���带 ��������� ����
        if (sounds.ContainsKey(sound))
        {
            AudioClip clip = sounds[sound];
            bgm.volume = bgmValue;
            bgm.clip = clip;
            bgm.Play();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }
}
