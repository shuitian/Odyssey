using UnityEngine;
using System;
using Mono.Data.Sqlite;
using Libgame;
using Libgame.Components;

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
        MonoBehaviour.print("Disconnected from db.");
    }

    static public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = OpenDB("database.db").CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }

    static public void SetHpComponentData(HpComponent hpComponent, int id)
    {
        string query = "SELECT * FROM hp where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            hpComponent.baseMaxHp = float.Parse(reader.GetString(1));
            hpComponent.ChangeMaxHp(float.Parse(reader.GetString(2)), float.Parse(reader.GetString(3)));
            hpComponent.minMaxHp = float.Parse(reader.GetString(5));
            hpComponent.maxMaxHp = float.Parse(reader.GetString(6));
            hpComponent.baseHpRecover = float.Parse(reader.GetString(7));
            hpComponent.ChangeHpRecover(float.Parse(reader.GetString(8)), float.Parse(reader.GetString(9)));
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
        query = "SELECT * FROM attack_dis where id = " + attackDisId;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            attackComponent.attackDistance.minDistance = float.Parse(reader.GetString(1));
            attackComponent.attackDistance.maxDistance = float.Parse(reader.GetString(2));
        }
        reader.Dispose();
    }

    static public void SetMonsterData(Monster monster, int id)
    {
        string query = "SELECT * FROM monsters where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            monster.characterName = reader.GetString(1);
            monster.introduction = reader.GetString(2);
            SetHpComponentData(monster.hpComponent, int.Parse(reader.GetString(3)));
            SetAttackComponentData(monster.attackComponent, int.Parse(reader.GetString(4)), int.Parse(reader.GetString(5)), int.Parse(reader.GetString(6)));
            SetHArmorComponentData((HyperbolaArmorComponent)monster.armorComponent, int.Parse(reader.GetString(7)));
            SetMoveComponentData(monster.moveComponent, int.Parse(reader.GetString(8)));
        }
        reader.Dispose();
    }

    static public void SetBuildingData(Building building, int id)
    {
        string query = "SELECT * FROM buildings where id = " + id;
        reader = ExecuteQuery(query);
        if (reader.Read())
        {
            building.characterName = reader.GetString(1);
            building.introduction = reader.GetString(2);
            SetHpComponentData(building.hpComponent, int.Parse(reader.GetString(3)));
            SetAttackComponentData(building.attackComponent, int.Parse(reader.GetString(4)), int.Parse(reader.GetString(5)), int.Parse(reader.GetString(6)));
            SetHArmorComponentData((HyperbolaArmorComponent)building.armorComponent, int.Parse(reader.GetString(7)));
            SetMoveComponentData(building.moveComponent, int.Parse(reader.GetString(8)));
        }
        reader.Dispose();
    }
}
