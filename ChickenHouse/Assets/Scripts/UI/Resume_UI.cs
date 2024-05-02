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
    [SerializeField] private List<TextMeshProUGUI>  skillList;
    [SerializeField] private Button                 closeBtn;
    public void SetData(ResumeData pResumeData)
    {
        gameObject.SetActive(true);

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
        string salaryLabelText      = LanguageMgr.GetText("SALARY");
        int    resumeSalary         = pResumeData.salary;
        string salaryString         = string.Format("{0} : {1:N0} $", salaryLabelText, resumeSalary);
        LanguageMgr.SetText(salaryText, salaryString);

        //기술&능력
        LanguageMgr.SetString(skillAbilityLabel, "SKILL_ABILITY");
        skillList.ForEach((x) => x.gameObject.SetActive(false));
        for(int i = 0; i < skillList.Count; i++)
        {
            if (pResumeData.skill.Count <= i)
                break;
            string abilityText      = GetAbilityText(pResumeData.skill[i]);
            string abilityString    = string.Format("- {0}", abilityText);
            LanguageMgr.SetText(skillList[i], abilityString);
            skillList[i].gameObject.SetActive(true);
        }

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private string GetAbilityText(WorkerSkill pWorkerSkill)
    {
        //기술&능력 텍스트
        switch(pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                return LanguageMgr.GetText("WORKER_SKILL_1");
            case WorkerSkill.WorkerSkill_2:
                return LanguageMgr.GetText("WORKER_SKILL_2");
            case WorkerSkill.WorkerSkill_3:
                return LanguageMgr.GetText("WORKER_SKILL_3");
            case WorkerSkill.WorkerSkill_4:
                return LanguageMgr.GetText("WORKER_SKILL_4");
            case WorkerSkill.WorkerSkill_5:
                return LanguageMgr.GetText("WORKER_SKILL_5");
            case WorkerSkill.WorkerSkill_6:
                return LanguageMgr.GetText("WORKER_SKILL_6");
        }
        return string.Empty;
    }
}
