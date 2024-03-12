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
        //한 프레임에 정해진 사운드만 실행
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
        //sound에 해당하는 사운드를 효과음 리스트에 등록
        seList.Add(sound);
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

        audio.clip = clip;
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

    public void PlayBGM(Sound sound)
    {
        //sound에 해당하는 사운드를 배경음으로 실행
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
