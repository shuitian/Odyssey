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
    public void SetUIDate(PokedexItem item, bool discoveryMask = true)
    {
        bool undiscovery = !(discoveryMask || item.discovery);
        string undiscoveryText = "未知";
        this.item = item;
        if (icon)
        {
            icon.sprite = item.icon;
            if (undiscovery)
            {
                icon.color = Color.black;
            }
            else
            {
                icon.color = Color.white;
            }
        }
        if (characterNameText)
            characterNameText.text = (undiscovery)?undiscoveryText:item.characterName;
        if (maxHpText)
            maxHpText.text = (undiscovery)?undiscoveryText:item.maxHp + "";
        if (hpRecoverText)
            hpRecoverText.text = (undiscovery)?undiscoveryText:item.hpRecover + "";
        if (attackText)
            attackText.text = (undiscovery)?undiscoveryText:item.attack + "";
        if (attackIntervalText)
            attackIntervalText.text = (undiscovery)?undiscoveryText:item.attackInterval + "";
        if (minAttackDisText)
            minAttackDisText.text = (undiscovery)?undiscoveryText:item.minAttackDis + "";
        if (maxAttackDisText)
            maxAttackDisText.text = (undiscovery)?undiscoveryText:item.maxAttackDis + "";
        if (armorText)
            armorText.text = (undiscovery)?undiscoveryText:item.armor + "";
        if (damageDerateText)
            damageDerateText.text = (undiscovery)?undiscoveryText:item.damageDerate + "";
        if (moveSpeedText)
            moveSpeedText.text = (undiscovery)?undiscoveryText:item.moveSpeed + "";

        if (chooseButton)
            chooseButton.onClick.AddListener(() => Pokedex.instance.ChooseStartPokemon(item));
    }
}
