// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.WhelpWatcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class WhelpWatcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_631";
  public const string Text = "<b>Rally:</b> Summon a {0}/{1} Whelp to attack the target first.";
  public const string GoldenText = "<b>Rally:</b> Summon a {0}/{1} Whelp to attack the target first.";
  public const string SummonCardId = "BG34_630t";
  public const string GoldenSummonCardId = "BG34_630_Gt";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      using (minion.Simulator.state.TriggerScope.New(nameof (WhelpWatcher)))
        minion.TrySummonMinion((Summon) (isGolden ? "BG34_630_Gt" : "BG34_630t"), isGolden);
    });
  }
}
