// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.TidemistressAthissa
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class TidemistressAthissa(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG23_013";
  public const string Text = "Whenever you cast a spell, give all your Naga +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever you cast a spell, give all your Naga +{0}/+{1} permanently.";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      foreach (Minion randomElement in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsNaga())).ToList<Minion>().GetRandomElements<Minion>(4))
        randomElement.IncreaseStats(this.DoubleIfGolden(1), this.DoubleIfGolden(1), (Entity) this);
    });
  }
}
