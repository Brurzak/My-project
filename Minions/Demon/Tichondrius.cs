// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.Tichondrius
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class Tichondrius(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyHeroDamaged,
  IEntity
{
  public const string CardId = "BG26_523";
  public const string Text = "After your hero takes damage, give your Demons +{0}/+{1}.";
  public const string GoldenText = "After your hero takes damage, give your Demons +{0}/+{1}.";

  public Action? OnFriendlyHeroDamaged(int damage, Entity? source)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDemon() && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(2));
    });
  }
}
