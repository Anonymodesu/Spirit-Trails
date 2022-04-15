
using UnityEngine.UI;
using UnityEngine;
using Battle.Entities;

namespace Battle.UI.Entities {
public class StatsDisplayLite : MonoBehaviour {
    private const float maxWidth = 60;

    [SerializeField]
    private RectTransform maxHealthDisplay = default;
    [SerializeField]
    private RectTransform currentHealthDisplay = default;
    [SerializeField]
    private RectTransform staminaDisplay = default;
    [SerializeField]
    private Text healthText = default;
    [SerializeField]
    private RectTransform maxManaDisplay = default;
    [SerializeField]
    private RectTransform currentManaDisplay = default;
    [SerializeField]
    private Text manaText = default;
    [SerializeField]
    private Text titleDisplay = default;


    

    public void UpdateStats(Entity entity) {
        var stats = entity.EntityStats;

        float healthWidth = maxWidth * stats.CurrentHealth / (float) stats.MaxHealth;
        float staminaWidth = maxWidth * stats.Stamina / (float) stats.MaxHealth;
        float manaWidth = maxWidth * stats.CurrentMana / (float) stats.MaxMana;

        currentHealthDisplay.sizeDelta = new Vector2(healthWidth, 0);
        staminaDisplay.sizeDelta = new Vector2(staminaWidth, 0);
        currentManaDisplay.sizeDelta = new Vector2(manaWidth, 0);

        healthText.text = $"{stats.Stamina}/{stats.CurrentHealth}/{stats.MaxHealth}";
        manaText.text = $"{stats.CurrentMana}/{stats.MaxMana}";
        titleDisplay.text = entity.Name;
    }


}
}