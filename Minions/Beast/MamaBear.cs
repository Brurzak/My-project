// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.MamaBear
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class MamaBear(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BGS_021";
  public const string Text = "Whenever you summon a Beast, give it +{0}/+{1}.";
  public const string GoldenText = "Whenever you summon a Beast, give it +{0}/+{1}.";

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsBeast())
        return;
      summoned.IncreaseStats(this.DoubleIfGolden(6));
    });
  }
}
