using UnityEngine;
using System;
using Mono.Data.Sqlite;
using Libgame;
using Libgame.Components;
using System.Collections;
using System.Collections.Generic;

public class ODData : MonoBehaviour
{
    static public void SetHpComponentData(HpComponent hpComponent, int id)
    {
        hpComponent.baseMaxHp = float.Parse(ODSetting.hpTable[id]["baseMaxHp"]);
        hpComponent.ChangeMaxPoint(ODSetting.settingDic["maxHpAddedValue"], ODSetting.settingDic["maxHpRate"]);
        hpComponent.minMaxHp = ODSetting.settingDic["minMaxHp"];
        hpComponent.maxMaxHp = ODSetting.settingDic["maxMaxHp"];

        hpComponent.baseHpRecover = float.Parse(ODSetting.hpTable[id]["baseHpRecover"]);
        hpComponent.ChangePointRecover(ODSetting.settingDic["hpRecoverAddedValue"], ODSetting.settingDic["hpRecoverRate"]);
    }

    static public void SetHArmorComponentData(HyperbolaArmorComponent hArmorComponent, int id)
    {
        hArmorComponent.minArmor = ODSetting.settingDic["minHArmor"];
        hArmorComponent.maxArmor = ODSetting.settingDic["maxHArmor"];
        hArmorComponent.damageModifiedValue = ODSetting.settingDic["damageHArmorModifiedValue"];
        hArmorComponent.armorList.baseArmorList = new float[1] { float.Parse(ODSetting.hArmorTable[id]["baseArmor"]) };
        hArmorComponent.armorList.armorAddedValueList = new float[1] { ODSetting.settingDic["armorAddedValue"] };
    }

    static public void SetMoveComponentData(MoveComponent moveComponent, int id)
    {
        moveComponent.SetMoveSpeed(float.Parse(ODSetting.moveTable[id]["baseMoveSpeed"]), ODSetting.settingDic["moveSpeedAddedvalue"], ODSetting.settingDic["moveSpeedAddedRate"]);
        moveComponent.minMoveSpeed = ODSetting.settingDic["minMoveSpeed"];
        moveComponent.maxMoveSpeed = ODSetting.settingDic["maxMoveSpeed"];
    }

    static public void SetAttackComponentData(BaseAttackComponent attackComponent, int attackId, int attackSpeedId, int attackDisId)
    {
        attackComponent.attacks = new Attacks(new float[1] { float.Parse(ODSetting.attackTable[attackId]["attacks"]) });

        attackComponent.attackSpeed.baseAttackInterval = float.Parse(ODSetting.attackSpeedTable[attackSpeedId]["baseAttackInterval"]);
        attackComponent.attackSpeed.timeModifiedValue = ODSetting.settingDic["attackSpeedTimeModifiedValue"];
        attackComponent.attackSpeed.minAttackSpeed = ODSetting.settingDic["minAttackSpeed"];
        attackComponent.attackSpeed.maxAttackSpeed = ODSetting.settingDic["maxAttackSpeed"];
        attackComponent.attackSpeed.attackSpeed = ODSetting.settingDic["baseAttackSpeed"];

        attackComponent.attackDistance.minDistance = float.Parse(ODSetting.attDisTable[attackDisId]["min"]);
        attackComponent.attackDistance.maxDistance = float.Parse(ODSetting.attDisTable[attackDisId]["max"]);
    }

    static public void SetHpData(PokedexItem item, int id)
    {
        item.maxHp = float.Parse(ODSetting.hpTable[id]["baseMaxHp"]);
        item.hpRecover = float.Parse(ODSetting.hpTable[id]["baseHpRecover"]);
    }

    static public void SetHArmorData(PokedexItem item, int id)
    {
        item.armor = float.Parse(ODSetting.hArmorTable[id]["baseArmor"]);
        item.damageDerate = HyperbolaArmorComponent.CalculateDamageDerates(ODSetting.settingDic["damageHArmorModifiedValue"], new float[1] { item.armor })[0];
    }

    static public void SetMoveData(PokedexItem item, int id)
    {
        item.moveSpeed = float.Parse(ODSetting.moveTable[id]["baseMoveSpeed"]);
    }

    static public void SetAttackData(PokedexItem item, int attackId, int attackSpeedId, int attackDisId)
    {
        item.attack = new float[1] { float.Parse(ODSetting.attackTable[attackId]["attacks"]) }[0];

        item.attackInterval = AttackSpeed.GetAttackInterval(float.Parse(ODSetting.attackSpeedTable[attackSpeedId]["baseAttackInterval"]), ODSetting.settingDic["attackSpeedTimeModifiedValue"], ODSetting.settingDic["baseAttackSpeed"]);

        item.minAttackDis = float.Parse(ODSetting.attDisTable[attackDisId]["min"]);
        item.maxAttackDis = float.Parse(ODSetting.attDisTable[attackDisId]["max"]);
    }

    static public PokedexItem GetPokedexItemById(int id)
    {
        PokedexItem item = new PokedexItem();
        item.id = id;
        item.characterName = ODSetting.pokemonsTable[id]["name"];
        SetHpData(item, int.Parse(ODSetting.pokemonsTable[id]["hp_id"]));
        SetAttackData(item, int.Parse(ODSetting.pokemonsTable[id]["attack_id"]), int.Parse(ODSetting.pokemonsTable[id]["attackspeed_id"]), int.Parse(ODSetting.pokemonsTable[id]["att_dis_id"]));
        SetHArmorData(item, int.Parse(ODSetting.pokemonsTable[id]["h_armor_id"]));
        SetMoveData(item, int.Parse(ODSetting.pokemonsTable[id]["move_id"]));
        item.discovery = Sql.IsHavePokemon(id);
        return item;
    }

    static public void SetPokemonData(Pokemon pokemon, int id)
    {
        pokemon.id = id;
        pokemon.characterName = ODSetting.pokemonsTable[id]["name"];
        SetHpComponentData(pokemon.hpComponent, int.Parse(ODSetting.pokemonsTable[id]["hp_id"]));
        SetAttackComponentData(pokemon.attackComponent, int.Parse(ODSetting.pokemonsTable[id]["attack_id"]), int.Parse(ODSetting.pokemonsTable[id]["attackspeed_id"]), int.Parse(ODSetting.pokemonsTable[id]["att_dis_id"]));
        SetHArmorComponentData((HyperbolaArmorComponent)pokemon.armorComponent, int.Parse(ODSetting.pokemonsTable[id]["h_armor_id"]));
        SetMoveComponentData(pokemon.moveComponent, int.Parse(ODSetting.pokemonsTable[id]["move_id"]));
    }

    static public Dictionary<int, string> GetResourceData()
    {
        Dictionary<int, string> resourceNames = new Dictionary<int, string>();
        foreach(int id in ODSetting.resourcesTable.Keys)
        {
            string name = ODSetting.resourcesTable[id]["name"];
            if (!resourceNames.ContainsKey(id))
            {
                resourceNames.Add(id, name);
            }
        }
        return resourceNames;
    }

    static public int[,] GetMap()
    {
        //int[,] map = new int[SpaceShip.shipSize, SpaceShip.shipSize];
        //foreach (int id in ODSetting.mapTable.Keys)
        //{
        //    int x = int.Parse(ODSetting.mapTable[id]["x"]);
        //    int y = int.Parse(ODSetting.mapTable[id]["y"]);
        //    int flag = int.Parse(ODSetting.mapTable[id]["flag"]);
        //    map[x, y] = flag;
        //}
        //return map;
        return Sql.GetMap();
    }

    static public int GetCellPrice(int id)
    {
        int price = int.Parse(ODSetting.cellPriceTable[id]["gold"]);
        return price;
    }

    static public int[] GetCellPrices()
    {
        int[] prices = new int[SpaceShip.shipSize * SpaceShip.shipSize - 3 * 3];
        int k = 0;
        foreach (int id in ODSetting.mapTable.Keys)
        {
            if (id > 9)
            {
                prices[k++] = int.Parse(ODSetting.cellPriceTable[id-9]["gold"]);
            }
        }
        return prices;
    }

    static public void UpdateCell(int x, int y, CellType type)
    {
        Sql.UpdateCell(x, y, type);
    }

    static public ArrayList GetStartPokemonsID()
    {
        ArrayList pokemons = new ArrayList();
        foreach (int id in ODSetting.pokemonUpgrateTable.Keys)
        {
            int start_choose = int.Parse(ODSetting.pokemonUpgrateTable[id]["start_choose"]);
            if (start_choose == 1)
            {
                pokemons.Add(id);
            }
        }
        return pokemons;
    }

    static public Dictionary<int, float> GetCurrentResources()
    {
        return Sql.GetCurrentResources();
    }

    static public ArrayList GetPokedex()
    {
        ArrayList pokemons = new ArrayList();
        foreach (int id in ODSetting.pokemonsTable.Keys)
        {
            pokemons.Add(GetPokedexItemById(id));
        }
        return pokemons;
        //return Sql.GetPokedex();
    }

    //static public ArrayList GetStartPokemons()
    //{
    //    ArrayList pokemons = new ArrayList();
    //    foreach (int id in ODSetting.pokemonUpgrateTable.Keys)
    //    {
    //        int start_choose = int.Parse(ODSetting.pokemonUpgrateTable[id]["start_choose"]);
    //        if (start_choose == 1)
    //        {
    //            PokedexItem item = GetPokedexItemById(id);
    //            pokemons.Add(item);
    //        }
    //    }
    //    return pokemons;
    //}

    static public void ClearPokemons()
    {
        Sql.ClearPokemons();
    }

    static public void OwnPokemon(int pokemonId)
    {
        Sql.OwnPokemon(pokemonId);
    }

    static public void ClearSpaceShip()
    {
        Sql.ClearSpaceShip();
    }

    static public void InitResource()
    {
        Sql.InitResource();
    }
}
