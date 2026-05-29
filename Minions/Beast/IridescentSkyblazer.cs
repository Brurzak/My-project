// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.IridescentSkyblazer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class IridescentSkyblazer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionTakeDamage,
  IEntity
{
  public const string CardId = "BG29_806";
  public const string Text = "Whenever a friendly Beast takes damage, give a friendly Beast other than it +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever a friendly Beast takes damage, give a friendly Beast other than it +{0}/+{1} permanently.";

  public Action? OnFriendlyMinionTakeDamage(Minion target, int value)
  {
    return (Action) (() =>
    {
      Minion minion;
      if (!target.IsBeast() || !target.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != target && x.IsBeast() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(1));
    });
  }
}
