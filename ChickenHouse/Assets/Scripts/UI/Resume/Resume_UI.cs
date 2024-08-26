using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resume_UI : Mgr
{
    [SerializeField] private Image                  face;
    [SerializeField] private TextMeshProUGUI        nameText;
    [SerializeField] private TextMeshProUGUI        ageText;
    [SerializeField] private TextMeshProUGUI        residenceText;
    [SerializeField] private TextMeshProUGUI        salaryText;
    [SerializeField] private TextMeshProUGUI        skillAbilityLabel;
    [SerializeField] private List<ResumeInfoList>   skillList;
    [SerializeField] private Animator               animator;
    private NoParaDel aniEndFun;

    public void SetData(WorkerData pResumeData, NoParaDel pFun = null)
    {
        aniEndFun = pFun;
        animator.Rebind();

        //증명사진
        face.sprite             = pResumeData.face;

        //이력서 이름 설정
        string nameLabelText    = LanguageMgr.GetText("NAME");
        string resumeName       = LanguageMgr.GetText(pResumeData.nameKey);
        string nameString       = string.Format("{0} : {1}", nameLabelText, resumeName);
        LanguageMgr.SetText(nameText, nameString);

        //나이 설정
        string ageLabelText     = LanguageMgr.GetText("AGE");
        int    resumeAge        = pResumeData.age;
        string ageString        = string.Format("{0} : {1}", ageLabelText, resumeAge);
        LanguageMgr.SetText(ageText, ageString);

        //거주지
        string residenceLabelText   = LanguageMgr.GetText("RESIDENCE");
        string resumeResidence      = LanguageMgr.GetText(pResumeData.residenceKey);
        string residenceString      = string.Format("{0} : {1}", residenceLabelText, resumeResidence);
        LanguageMgr.SetText(residenceText, residenceString);

        //희망 급여
        string salaryLabelText      = LanguageMgr.GetText("WORKER_SALARY");
        int    resumeSalary         = pResumeData.salary;
        string salaryFormatString   = string.Format(LanguageMgr.GetText("SALARY_FORMAT"), resumeSalary);
        string salaryString         = string.Format("{0} : {1}", salaryLabelText, salaryFormatString);
        LanguageMgr.SetText(salaryText, salaryString);

        //기술&능력
        LanguageMgr.SetString(skillAbilityLabel, "SKILL_ABILITY");
        skillList.ForEach((x) => x.gameObject.SetActive(false));
        for(int i = 0; i < skillList.Count; i++)
        {
            if (pResumeData.skill.Count <= i)
                break;
            skillList[i].SetUI(pResumeData.skill[i]);
            skillList[i].gameObject.SetActive(true);
        }
    }

    public void AniEndFun()
    {
        //ShowResume애니메이션의 애니메이터로 끌어서 사용중임 
        aniEndFun?.Invoke();
    }
}
