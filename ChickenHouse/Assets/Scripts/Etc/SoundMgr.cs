using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : AwakeSingleton<SoundMgr>
{
    private Dictionary<Sound, AudioClip> sounds = new();
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource se;

    public struct SE_List
    {
        public Sound sound;
        public float seValue;
        public SE_List(Sound pSound,float pSeValue)
        {
            sound = pSound;
            seValue = pSeValue;
        }
    }

    private Dictionary<Sound, AudioSource> loopSE = new Dictionary<Sound, AudioSource>();
    private List<SE_List> seList = new List<SE_List>();

    public float bgmValue   { get; private set; }
    public float seValue    { get; private set; }

    private const string BGM_KEY = "BGM";
    private const string SE_KEY = "SE";

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;
        init = true;
        for (Sound sound = Sound.InGame_BG; sound < Sound.MAX; sound++)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sound/{sound.ToString()}");
            if (clip == null)
                continue;
            sounds.Add(sound, clip);
        }

        seValue     = PlayerPrefs.GetFloat(SE_KEY, 0.5f);
        bgmValue    = PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
        SetSE_Volume(seValue);
        SetBGM_Volume(bgmValue);
    }

    private void Update()
    {
        //한 프레임에 정해진 사운드만 실행
        foreach(SE_List list in seList)
        {
            if(sounds.ContainsKey(list.sound))
            {
                AudioClip clip = sounds[list.sound];
                se.volume = list.seValue;
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
        //sound에 해당하는 사운드를 효과음 리스트에 등록
        seList.Add(new SE_List(sound,seValue));
    }

    public void PlaySE(Sound sound,float v)
    {
        //sound에 해당하는 사운드를 효과음 리스트에 등록
        seList.Add(new SE_List(sound, v));
    }

    public void PlayLoopSE(Sound sound)
    {
        //반복되어야하는 효과음처리
        if (sounds.ContainsKey(sound) == false)
        {
            //해당 사운드가 존재하지않는다.
            return;
        }

        AudioClip clip = sounds[sound];
        AudioSource audio = null;

        if (loopSE.ContainsKey(sound))
        {
            //사운드 오브젝트가있으면 재사용
            audio = loopSE[sound];
        }


        if (audio == null)
        {
            //해당 사운드 오브젝트가 없으므로 생성
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
        //반복효과음을 멈춘다.
        if (loopSE.ContainsKey(sound))
        {
            //사운드 오브젝트에 해당 사운드가 있으면 멈춘다.
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
        //sound에 해당하는 사운드를 배경음으로 실행
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
