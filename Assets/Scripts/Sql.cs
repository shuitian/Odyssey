﻿using UnityEngine;
using System;
using Mono.Data.Sqlite;
using Libgame;
using Libgame.Components;
using System.Collections;
using System.Collections.Generic;

public class Sql : MonoBehaviour
{

    static SqliteConnection dbConnection;
    static SqliteCommand dbCommand;
    static SqliteDataReader reader;
    // Use this for initialization

    static public SqliteConnection OpenDB(string dbName)
    {
        try
        {
            if (dbConnection == null)
            {

                dbConnection = new SqliteConnection("data source=" + Application.dataPath + "/StreamingAssets/" + dbName);
                Debug.Log("Connected to db");
                MonoBehaviour.print("Open " + dbName + " success");
                dbConnection.Open();

                return dbConnection;
            }
            else
            {
                return dbConnection;
            }
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
            MonoBehaviour.print(temp1);
        }
        return null;
    }

    static public void CloseDB()
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = null;
        if (reader != null)
        {
            reader.Dispose();
        }
        reader = null;
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
        Debug.Log("Disconnected from db.");
        //MonoBehaviour.print("Disconnected from db.");
    }

    static public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = OpenDB("database.db").CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }

    static public void ExecuteNonQuery(string sqlQuery)
    {
        dbCommand = OpenDB("database.db").CreateCommand();
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteNonQuery();
    }

    static public void SetHpComponentData(HpComponent hpComponent, int id)
    {
        string query = "SELECT * FROM hp where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            hpComponent.baseMaxHp = float.Parse(reader.GetString(1));
            hpComponent.ChangeMaxPoint(float.Parse(reader.GetString(2)), float.Parse(reader.GetString(3)));
            hpComponent.minMaxHp = float.Parse(reader.GetString(5));
            hpComponent.maxMaxHp = float.Parse(reader.GetString(6));
            hpComponent.baseHpRecover = float.Parse(reader.GetString(7));
            hpComponent.ChangePointRecover(float.Parse(reader.GetString(8)), float.Parse(reader.GetString(9)));
        }
        reader.Dispose();
    }

    static public void SetHArmorComponentData(HyperbolaArmorComponent hArmorComponent, int id)
    {
        string query = "SELECT * FROM h_armor where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            hArmorComponent.minArmor = float.Parse(reader.GetString(1));
            hArmorComponent.maxArmor = float.Parse(reader.GetString(2));
            hArmorComponent.damageModifiedValue = float.Parse(reader.GetString(3));
            hArmorComponent.armorList.baseArmorList = new float[1] { float.Parse(reader.GetString(4)) };
            hArmorComponent.armorList.armorAddedValueList = new float[1] { float.Parse(reader.GetString(5)) };
        }
        reader.Dispose();
    }

    static public void SetMoveComponentData(MoveComponent moveComponent, int id)
    {
        string query = "SELECT * FROM move where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            moveComponent.SetMoveSpeed(float.Parse(reader.GetString(1)), float.Parse(reader.GetString(2)), float.Parse(reader.GetString(3)));
            moveComponent.minMoveSpeed = float.Parse(reader.GetString(4));
            moveComponent.maxMoveSpeed = float.Parse(reader.GetString(5));
        }
        reader.Dispose();
    }

    static public void SetAttackComponentData(BaseAttackComponent attackComponent, int attackId, int attackSpeedId, int attackDisId)
    {
        string query = "SELECT * FROM attack where id = " + attackId;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            attackComponent.attacks = new Attacks(new float[1] { float.Parse(reader.GetString(1)) });
        }
        reader.Dispose();
        query = "SELECT * FROM attackspeed where id = " + attackSpeedId;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            attackComponent.attackSpeed.baseAttackInterval = float.Parse(reader.GetString(1));
            attackComponent.attackSpeed.timeModifiedValue = float.Parse(reader.GetString(2));
            attackComponent.attackSpeed.minAttackSpeed = float.Parse(reader.GetString(3));
            attackComponent.attackSpeed.maxAttackSpeed = float.Parse(reader.GetString(4));
            attackComponent.attackSpeed.attackSpeed = float.Parse(reader.GetString(5));
        }
        reader.Dispose();
        query = "SELECT * FROM att_dis where id = " + attackDisId;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            attackComponent.attackDistance.minDistance = float.Parse(reader.GetString(1));
            attackComponent.attackDistance.maxDistance = float.Parse(reader.GetString(2));
        }
        reader.Dispose();
    }

    //static public void SetMonsterData(Monster monster, int id)
    //{
    //    string query = "SELECT * FROM pokemons where id = " + id;
    //    reader = ExecuteQuery(query);
    //    if (reader.Read())
    //    {
    //        monster.characterName = reader.GetString(1);
    //        monster.introduction = reader.GetString(2);
    //        SetHpComponentData(monster.hpComponent, int.Parse(reader.GetString(3)));
    //        SetAttackComponentData(monster.attackComponent, int.Parse(reader.GetString(4)), int.Parse(reader.GetString(5)), int.Parse(reader.GetString(6)));
    //        SetHArmorComponentData((HyperbolaArmorComponent)monster.armorComponent, int.Parse(reader.GetString(7)));
    //        SetMoveComponentData(monster.moveComponent, int.Parse(reader.GetString(8)));
    //    }
    //    reader.Dispose();
    //}

    static public void SetPokemonData(Pokemon pokemon, int id)
    {
        string query = "SELECT * FROM pokemons where id = " + id;
        SqliteDataReader reader = ExecuteQuery(query);
        if (reader.Read())
        {
            pokemon.characterName = reader.GetString(1);
            pokemon.introduction = reader.GetString(2);
            SetHpComponentData(pokemon.hpComponent, int.Parse(reader.GetString(3)));
            SetAttackComponentData(pokemon.attackComponent, int.Parse(reader.GetString(4)), int.Parse(reader.GetString(5)), int.Parse(reader.GetString(6)));
            SetHArmorComponentData((HyperbolaArmorComponent)pokemon.armorComponent, int.Parse(reader.GetString(7)));
            SetMoveComponentData(pokemon.moveComponent, int.Parse(reader.GetString(8)));
        }
        reader.Dispose();
    }

    static public Dictionary<int,string> GetResourceData()
    {
        Dictionary<int, string> resourceNames = new Dictionary<int, string>();
        string query = "SELECT * FROM resources";
        reader = ExecuteQuery(query);
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            if (!resourceNames.ContainsKey(id))
            {
                resourceNames.Add(id, name);
            }
        }
        reader.Dispose();
        return resourceNames;
    }

    static public int[,] GetMap()
    {
        int[,] map = new int[SpaceShip.shipSize, SpaceShip.shipSize];
        string query = "SELECT * FROM map";
        reader = ExecuteQuery(query);
        while (reader.Read())
        {
            int x = (int)float.Parse(reader.GetString(1));
            int y = (int)float.Parse(reader.GetString(2));
            int flag = (int)float.Parse(reader.GetString(3));
            map[x, y] = flag;
        }
        reader.Dispose();
        return map;
    }

    static public int GetCellPrice(int id)
    {
        int price = 0;
        string query = "SELECT * FROM cell_price where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            price = int.Parse(reader.GetString(1));
        }
        reader.Dispose();
        return price;
    }

    static public int[] GetCellPrices()
    {
        int[] prices = new int[SpaceShip.shipSize * SpaceShip.shipSize - 3 * 3];
        string query = "SELECT * FROM cell_price";
        reader = ExecuteQuery(query);
        int k = 0;
        while (reader.Read())
        {
            prices[k++] = (int)float.Parse(reader.GetString(1));
        }
        reader.Dispose();
        return prices;
    }

    static public void UpdateCell(int x, int y, CellType type)
    {
        string query = "UPDATE map SET flag = '" + (int)type + "' where x = '" + x + "' and y = '" + y + "'";
        ExecuteNonQuery(query);
    }

    static public ArrayList GetStartPokemonsID()
    {
        ArrayList pokemons = new ArrayList();
        string query = "SELECT id FROM pokemon_upgrate where start_choose = '1'";
        reader = ExecuteQuery(query);
        while (reader.Read())
        {
            pokemons.Add(reader.GetInt32(0));
        }
        reader.Dispose();
        return pokemons;
    }

    static public Dictionary<int,float> GetCurrentResources()
    {
        Dictionary<int, float> resourceNums = new Dictionary<int, float>();
        string query = "SELECT * FROM resources";
        reader = ExecuteQuery(query);
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            float num = float.Parse(reader.GetString(3));
            if (!resourceNums.ContainsKey(id))
            {
                resourceNums.Add(id, num);
            }
        }
        reader.Dispose();
        return resourceNums;
    }

    static public ArrayList GetPokedex()
    {
        ArrayList dex = new ArrayList();

        //ArrayList pokemons = new ArrayList();
        //string query = "SELECT id FROM pokemon_upgrate where start_choose = '1'";
        //reader = ExecuteQuery(query);
        //while (reader.Read())
        //{
        //    pokemons.Add(reader.GetInt32(0));
        //}
        //reader.Dispose();
        return dex;
    }

    static public void ClearPokemons()
    {
        string query = "UPDATE pokemons SET owned = '0'";
        ExecuteNonQuery(query);
    }

    static public void OwnPokemon(int pokemonId)
    {
        string query = "UPDATE pokemons SET owned = '1' where id = " + pokemonId;
        ExecuteNonQuery(query);
    }

    static public void ClearSpaceShip()
    {
        string query = "UPDATE map SET flag = backup";
        ExecuteNonQuery(query);
    }

    static public void InitResource()
    {
        string query = "UPDATE resources SET currentOwn = ownAtFirst";
        ExecuteNonQuery(query);
    }
}
