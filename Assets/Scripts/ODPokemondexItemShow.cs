using UnityEngine;
using UnityEngine.UI;

public class ODPokemondexItemShow : MonoBehaviour {

    PokedexItem item;

    public Image icon;
    public Text characterNameText;
    public Text maxHpText;
    public Text hpRecoverText;
    public Text attackText;
    public Text attackIntervalText;
    public Text minAttackDisText;
    public Text maxAttackDisText;
    public Text armorText;
    public Text damageDerateText;
    public Text moveSpeedText;
    public Button chooseButton;
    public void SetUIDate(PokedexItem item)
    {
        this.item = item;
        if (icon)
            icon.sprite = item.icon;
        if (characterNameText)
            characterNameText.text = item.characterName;
        if (maxHpText)
            maxHpText.text = item.maxHp + "";
        if (hpRecoverText)
            hpRecoverText.text = item.hpRecover + "";
        if (attackText)
            attackText.text = item.attack + "";
        if (attackIntervalText)
            attackIntervalText.text = item.attackInterval + "";
        if (minAttackDisText)
            minAttackDisText.text = item.minAttackDis + "";
        if (maxAttackDisText)
            maxAttackDisText.text = item.maxAttackDis + "";
        if (armorText)
            armorText.text = item.armor + "";
        if (damageDerateText)
            damageDerateText.text = item.damageDerate + "";
        if (moveSpeedText)
            moveSpeedText.text = item.moveSpeed + "";

        if (chooseButton)
            chooseButton.onClick.AddListener(() => Pokedex.instance.ChooseStartPokemon(item));
    }
}
