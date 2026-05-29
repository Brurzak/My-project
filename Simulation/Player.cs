// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Player
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Simulation;

public class Player
{
  private readonly Input? _input;

  public Player(Input? input) => this._input = input;

  public List<Minion> Side { get; set; } = new List<Minion>();

  public List<HeroPowerData> HeroPowers { get; set; } = new List<HeroPowerData>();

  public List<QuestData> Quests { get; set; } = new List<QuestData>();

  public List<Objective> Objectives { get; set; } = new List<Objective>();

  public List<Trinket> Trinkets { get; set; } = new List<Trinket>();

  public List<Simulator.Secrets> Secrets { get; set; } = new List<Simulator.Secrets>();

  public List<CardEntity> Hand { get; set; } = new List<CardEntity>();

  public int FriendlyMinionsDeadLastCombatCounter { get; set; }

  public int EternalKnightCounter { get; set; }

  public int UndeadAttackBonus { get; set; }

  public int WhelpAttackBonus { get; set; }

  public int WhelpHealthBonus { get; set; }

  public int ElementalPlayCounter { get; set; }

  public int BloodGemAtkBuff { get; set; }

  public int BloodGemHealthBuff { get; set; }

  public int PiratesSummonCounter { get; set; }

  public int ResourcesSpentThisGame { get; set; }

  public int BeastsSummonCounter { get; set; }

  public int BeastAttackBonus { get; set; }

  public int BeastHealthBonus { get; set; }

  public int BeetlesAtkBuff { get; set; }

  public int BeetlesHealthBuff { get; set; }

  public int BattlecryCounter { get; set; }

  public int DeathrattleCounter { get; set; }

  public int SanlaynScribeCounter { get; set; }

  public int TavernSpellAtkBuff { get; set; }

  public int TavernSpellHealthBuff { get; set; }

  public int HauntedAtkBuff { get; set; }

  public int HauntedHealthBuff { get; set; }

  public int Health { get; set; }

  public int DamageTaken { get; set; }

  public int Tier { get; set; } = 1;

  public int AncestralAutomatonCounter { get; set; }

  public int DeepBluesCounter { get; set; }

  public int TavernSpellCounter { get; set; }

  public int AnySpellCounter { get; set; }

  public int VolumizerAtkBuff { get; set; }

  public int VolumizerHealthBuff { get; set; }

  public void AddHeroPower(
    string heroPowerCardId,
    bool friendly,
    bool isActivated,
    int data = 0,
    int data2 = 0,
    int data3 = 0,
    Minion? attachedMinion = null)
  {
    this.HeroPowers.Add(new HeroPowerData()
    {
      CardId = !(heroPowerCardId == "") || friendly ? heroPowerCardId : "kel'thuzad",
      IsActivated = isActivated,
      Data = data,
      Data2 = data2,
      Data3 = data3,
      AttachedMinion = attachedMinion
    });
  }

  public void SetSecrets(IEnumerable<int?> dbfIds)
  {
    this.Secrets.Clear();
    foreach (int? dbfId in dbfIds)
      this.AddSecret(dbfId);
  }

  public void SetSecretsHstracker(IEnumerable<int> dbfIds)
  {
    this.SetSecrets(dbfIds.Select<int, int?>((Func<int, int?>) (id => id == 0 ? new int?() : new int?(id))));
  }

  private void AddSecret(int? dbfid) => this._input?.AddSecretFromDbfId(dbfid, this.Secrets);
}
