using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResumeInfoList : Mgr
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillInfo;

    public void SetUI(WorkerSkill pWorkerSkill)
    {
        switch(pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_1");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_1");
                break;
            case WorkerSkill.WorkerSkill_2:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_2");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_2");
                break;
            case WorkerSkill.WorkerSkill_3:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_3");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_3");
                break;
            case WorkerSkill.WorkerSkill_4:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_4");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_4");
                break;
            case WorkerSkill.WorkerSkill_5:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_5");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_5");
                break;
            case WorkerSkill.WorkerSkill_6:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_6");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_6");
                break;
            case WorkerSkill.WorkerSkill_7:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_7");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_7");
                break;
            case WorkerSkill.WorkerSkill_8:
                LanguageMgr.SetString(skillName, "WORKER_SKILL_8");
                LanguageMgr.SetString(skillInfo, "WORKER_SKILL_INFO_8");
                break;
        }
    }
}
