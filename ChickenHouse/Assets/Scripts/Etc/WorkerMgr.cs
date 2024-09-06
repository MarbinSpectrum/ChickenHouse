using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMgr : AwakeSingleton<WorkerMgr>
{
    [SerializeField] private Dictionary<EWorker, WorkerData> workerData = new Dictionary<EWorker, WorkerData>();

    public WorkerData GetWorkerData(EWorker pWorker)
    {
        //직원 정보 얻기
        if (workerData.ContainsKey(pWorker))
            return workerData[pWorker];
        return null;
    }

    public string GetWorkSkillNameString(WorkerSkill pWorkerSkill)
    {
        //직원 스킬 이름
        switch (pWorkerSkill)
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
            case WorkerSkill.WorkerSkill_7:
                return LanguageMgr.GetText("WORKER_SKILL_7");
            case WorkerSkill.WorkerSkill_8:
                return LanguageMgr.GetText("WORKER_SKILL_8");
        }
        return string.Empty;
    }

    public string GetWorkSkillInfoString(WorkerSkill pWorkerSkill)
    {
        //직원 스킬 이름
        switch (pWorkerSkill)
        {
            case WorkerSkill.WorkerSkill_1:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_1");
            case WorkerSkill.WorkerSkill_2:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_2");
            case WorkerSkill.WorkerSkill_3:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_3");
            case WorkerSkill.WorkerSkill_4:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_4");
            case WorkerSkill.WorkerSkill_5:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_5");
            case WorkerSkill.WorkerSkill_6:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_6");
            case WorkerSkill.WorkerSkill_7:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_7");
            case WorkerSkill.WorkerSkill_8:
                return LanguageMgr.GetText("WORKER_SKILL_INFO_8");
        }
        return string.Empty;
    }
}