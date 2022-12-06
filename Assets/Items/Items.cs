using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Items : ScriptableObject
{
    public List<Weapon> weapons;
    public List<Equipment> equipment;
    public List<Consumables> consumables;

    public enum WeaponHandling
    {
        ONEHANDED,
        TWOHANDED,
        SPEARHEADED,
        OFFHANDED
    }

    [System.Serializable]
    public struct Weapon
    {
        public string name;
        public Texture2D icon;
        public WeaponHandling handling;
        public float damage;
        public float range;
        public float weight;
    }

    [System.Serializable]
    public struct Equipment
    {
        public string name;
        public Texture2D icon;
        public float weight;
    }

    [System.Serializable]
    public struct Consumables
    {
        public string name;
        public Texture2D icon;
        public float weight;
    }

}

public class CreateItemList
{
    [MenuItem("Assets/Create/Scriptable Objects/Items List", priority = 60)]
    public static Items Create()
    {
        Items asset = Items.CreateInstance<Items>();
        AssetDatabase.CreateAsset(asset, "Assets/Items/ItemsList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
