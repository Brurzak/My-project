using System;
using System.Collections.Generic;

[Serializable]
public class CardData
{
    public string id;
    public int dbfId; // ID numerico univoco della carta base
    public string name;
    public int techLevel;
    public string set;
    public string race;
    public List<string> races;
    public string text;

    public int attack;
    public int health;
    public bool isBattlegroundsPoolMinion;

    // COLLEGAMENTI NATIVI DEL JSON DI HEARTHSTONE PER LE GOLDEN
    public int battlegroundsPremiumDbfId;
    public int battlegroundsNormalDbfId;

    public List<string> mechanics;
    public string rarity;
    public bool elite;
}