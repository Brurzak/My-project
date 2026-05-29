// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.MurlocTidecaller
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class MurlocTidecaller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "EX1_509";
  public const string Text = "Whenever you summon a Murloc, gain +1 Attack.";

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsMurloc())
        return;
      this.IncreaseStats(this.DoubleIfGolden(1), 0);
    });
  }
}
