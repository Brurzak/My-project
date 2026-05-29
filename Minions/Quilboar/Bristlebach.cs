// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.Bristlebach
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class Bristlebach(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG26_157";
  public const string Text = "<b>Avenge (2):</b> This plays 2 <b>Blood Gems</b> on all your Quilboar.";
  public const string GoldenText = "<b>Avenge (2):</b> This plays 4 <b>Blood Gems</b> on all your Quilboar.";

  public int AvengeRequirement => 2;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      foreach (Minion target in this.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is BristlebachPortrait)) ? this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>() : this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsQuilboar() && x.IsAlive())).ToList<Minion>())
      {
        for (int index = 0; index < this.DoubleIfGolden(2); ++index)
          this.Simulator.CastBloodGem(target, (Entity) this);
      }
    });
  }
}
