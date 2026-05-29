// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.SoulRewinder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class SoulRewinder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyHeroDamaged,
  IEntity,
  IOnFriendlyHeroDamagedRewind
{
  public const string CardId = "BG26_174";
  public const string Text = "After your hero takes damage, rewind it and give this +1 Health.";
  public const string GoldenText = "After your hero takes damage, rewind it and give this +2 Health.";

  public Action? OnFriendlyHeroDamaged(int damage, Entity? source)
  {
    return (Action) (() => this.IncreaseStats(this.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x.CardID == "BG30_MagicItem_868")) ? this.DoubleIfGolden(1) : 0, this.DoubleIfGolden(1)));
  }
}
