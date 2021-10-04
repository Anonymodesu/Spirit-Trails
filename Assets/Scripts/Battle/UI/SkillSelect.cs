using Battle.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Entities
{

public class SkillSelect : MonoBehaviour
{

    [SerializeField]
    private GameObject skillButtonPrefab;

    private GameObject skillButtons;
    // Start is called before the first frame update
    void Start()
    {
        skillButtons = this.transform.Find("SkillButtons").gameObject;
    }

    public void SetSkills(IEnumerable<Skill> skills) {
        foreach(Transform child in skillButtons.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Skill skill in skills) {
            GameObject button = Instantiate(skillButtonPrefab, skillButtons.transform);
            button.transform.Find("Text").GetComponent<Text>().text = skill.Name;
        }
    }
}

}