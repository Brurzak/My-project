// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.HarmlessBonehead
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class HarmlessBonehead(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG28_300";
  public const string Text = "<b>Deathrattle:</b> Summon two 1/1 Skeletons.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon four 1/1 Skeletons.";
  public const string SummonCardId = "BG_ICC_026t";

  public Action<Minion> GetDeathrattle() => HarmlessBonehead.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinions(new Summon("BG_ICC_026t"), new Summon("BG_ICC_026t"));
      if (!golden)
        return;
      minion.TrySummonMinions(new Summon("BG_ICC_026t"), new Summon("BG_ICC_026t"));
    });
  }
}
