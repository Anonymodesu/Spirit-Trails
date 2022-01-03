using Battle.Entities;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{



public class SkillSelect : MonoBehaviour
{

    [SerializeField]
    private GameObject skillButtonPrefab;
    [SerializeField]
    private SkillPlan skillPlan;
    private GameObject skillButtons;

    void Start()
    {
        skillButtons = this.transform.Find("SkillButtons").gameObject;
    }

    public void SetSkills(IEnumerable<Skill> skills, Action<Skill> skillButtonFunc) {
        foreach(Transform child in skillButtons.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Skill skill in skills) {
            Button button = Instantiate(skillButtonPrefab, skillButtons.transform).GetComponent<Button>();
            button.transform.Find("Text").GetComponent<Text>().text = skill.Name;
            button.onClick.AddListener(() => skillButtonFunc(skill));
        }
    }


}

}