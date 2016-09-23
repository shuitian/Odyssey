using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ODSetting : MonoBehaviour
{
    static Dictionary<string, int> _settingDic = new Dictionary<string, int>();
    static bool settingFlag = false;
    static public Dictionary<string, int> settingDic
    {
        get
        {
            if (!settingFlag)
            {
                LoadSetting();
                settingFlag = true;
            }
            return _settingDic;
        }
    }
    static void LoadSetting()
    {
        TextAsset txt = Resources.Load<TextAsset>("Bytes/setting");
        JObject jo = (JObject)JsonConvert.DeserializeObject(txt.text);
        foreach(var item in jo)
        {
            int value = int.Parse(item.Value.ToString());
            _settingDic[item.Key.ToString()] = value;
        }
        //IEnumerable<JProperty> properties = jo.Properties();
        //foreach (JProperty jp in properties)
        //{
        //    int value = int.Parse(jp.Value.ToString());
        //    _settingDic[jp.Name] = value;
        //}
    }

    static public AttDisTable attDisTable = new AttDisTable("Bytes/att_dis");
    static public AttackTable attackTable = new AttackTable("Bytes/attack");
    static public AttackSpeedTable attackSpeedTable = new AttackSpeedTable("Bytes/attackspeed");
    static public CellPriceTable cellPriceTable = new CellPriceTable("Bytes/cell_price");
    static public HArmorTable hArmorTable = new HArmorTable("Bytes/h_armor");
    static public HpTable hpTable = new HpTable("Bytes/hp");
    static public MapTable mapTable = new MapTable("Bytes/map");
    static public MoveTable moveTable = new MoveTable("Bytes/move");
    static public PokemonUpgrateTable pokemonUpgrateTable = new PokemonUpgrateTable("Bytes/pokemon_upgrate");
    static public PokemonsTable pokemonsTable = new PokemonsTable("Bytes/pokemons");
    static public ResourcesTable resourcesTable = new ResourcesTable("Bytes/resources");
}

public class ODTable
{
    string path;
    public Dictionary<int,Dictionary<string,string>>.KeyCollection Keys
    {
        get
        {
            if (!loaded)
            {
                tables = LoadTable(path);
                loaded = true;
            }
            return tables.Keys;
        }
    }

    public IEnumerator<Dictionary<string,string>> GetEnumerator()
    {
        if (!loaded)
        {
            tables = LoadTable(path);
            loaded = true;
        }
        foreach (var key in tables.Keys)
        {
            yield return tables[key];
        }
    }

    public Dictionary<int,Dictionary<string, string>> LoadTable(string path)
    {
        Dictionary<int, Dictionary<string, string>> ret = new Dictionary<int, Dictionary<string, string>>();
        TextAsset txt = Resources.Load<TextAsset>(path);
        JArray ja = (JArray)JsonConvert.DeserializeObject(txt.text);
        foreach (JObject jo in ja)
        {
            int id = (int)jo["id"];
            ret[id] = new Dictionary<string, string>();
            JObject jo1 = (JObject)jo["data"];
            foreach (var item in jo1)
            {
                string value = item.Value.ToString();
                //MonoBehaviour.print(path + "=>" + item.Key.ToString() + "=>" + value);
                ret[id][item.Key.ToString()] = value;
            }
        }
        return ret;
    }

    Dictionary<int, Dictionary<string, string>> tables = new Dictionary<int, Dictionary<string, string>>();
    bool loaded = false;
    public virtual Dictionary<string, string> this[int index]
    {
        get
        {
            if (!loaded)
            {
                tables = LoadTable(path);
                loaded = true;
            }
            if (tables.ContainsKey(index))
            {
                return tables[index];
            }
            else
            {
                Debug.LogError("Get index: " + index + " from tables error!");
                return null;
            }
        }
    }

    public ODTable(string path)
    {
        this.path = path;
    }
}

public class AttDisTable : ODTable
{
    public AttDisTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class AttackTable : ODTable
{
    public AttackTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class AttackSpeedTable : ODTable
{
    public AttackSpeedTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class CellPriceTable : ODTable
{
    public CellPriceTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class HArmorTable : ODTable
{
    public HArmorTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class HpTable : ODTable
{
    public HpTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class MapTable : ODTable
{
    public MapTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {   
            return base[index];
        }
    }
}

public class MoveTable : ODTable
{
    public MoveTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class PokemonUpgrateTable : ODTable
{
    public PokemonUpgrateTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class PokemonsTable : ODTable
{
    public PokemonsTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}

public class ResourcesTable : ODTable
{
    public ResourcesTable(string path) : base(path)
    {
    }

    public override Dictionary<string, string> this[int index]
    {
        get
        {
            return base[index];
        }
    }
}