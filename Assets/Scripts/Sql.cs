using UnityEngine;
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
    static public Dictionary<string,int> setting
    {
        get
        {
            return ODSetting.settingDic;
        }
    }

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

    static public void UpdateCell(int x, int y, CellType type)
    {
        string query = "UPDATE map SET flag = '" + (int)type + "' where x = '" + x + "' and y = '" + y + "'";
        print(query);
        ExecuteNonQuery(query);
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

    //static public ArrayList GetPokedex()
    //{
    //    ArrayList dex = new ArrayList();

    //    //ArrayList pokemons = new ArrayList();
    //    //string query = "SELECT id FROM pokemon_upgrate where start_choose = '1'";
    //    //reader = ExecuteQuery(query);
    //    //while (reader.Read())
    //    //{
    //    //    pokemons.Add(reader.GetInt32(0));
    //    //}
    //    //reader.Dispose();
    //    return dex;
    //}

    static public void ClearPokemons()
    {
        string query = "UPDATE pokemon_dex SET owned = '0'";
        ExecuteNonQuery(query);
    }

    static public bool IsHavePokemon(int pokemonId)
    {
        string query = "SELECT * FROM pokemon_dex where owned = '1' and id = " + pokemonId;
        reader = ExecuteQuery(query);
        bool owned = reader.Read();
        reader.Dispose();
        return owned;
    }

    static public void OwnPokemon(int pokemonId)
    {
        string query = "UPDATE pokemon_dex SET owned = '1' where id = " + pokemonId;
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
