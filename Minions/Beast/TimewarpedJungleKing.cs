// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedJungleKing
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedJungleKing(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG34_PreMadeChamp_004";
  public const string Text = "<b>Stealth</b> After you summon a Beast, give it +{0}/+{1}. Improves after you cast a spell.";
  public const string GoldenText = "<b>Stealth</b> After you summon a Beast, give it +{0}/+{1}. Improves after you cast a spell.";

  public Action OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (summoned.IsDead() || !summoned.IsBeast())
        return;
      summoned.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2);
    });
  }
}
