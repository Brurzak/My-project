// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.CaptainBonerender
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class CaptainBonerender(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG31_840";
  public const string Text = "After you summon a different minion in combat, summon an extra copy of it.";
  public const string GoldenText = "After you summon a different minion in combat, summon two extra copies of it.";

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (source?.CardID == "BG31_840" || summoned.CardID == "BG31_840")
        return;
      this.TrySummonMinion((Summon) summoned.Clone());
      if (!this.golden)
        return;
      this.TrySummonMinion((Summon) summoned.Clone());
    });
  }
}
