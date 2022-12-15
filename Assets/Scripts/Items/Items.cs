using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class Items : ScriptableObject
{
    //Declaration of list of different types of item
    public List<Weapon> weapons;
    public List<Equipment> equipment;
    public List<Consumables> consumables;

    //The enum for the type of Weapon
    public enum WeaponHandling
    {
        ONEHANDED,
        TWOHANDED,
        SPEARHEADED,
        OFFHANDED
    }
    
    //Declaration of the structs of the different types of items
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

    //Class and function for the creation of the Items List ASSET
    [MenuItem("Assets/Create/Scriptable Objects/Items List", priority = 60)]
    public static Items Create()
    {
        Items asset = Items.CreateInstance<Items>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects and Data/Items");
        AssetDatabase.SaveAssets();
        return asset;
    }
}


