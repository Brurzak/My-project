// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.AggemThorncurse
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class AggemThorncurse(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterBloodGemCast,
  IEntity
{
  public const string CardId = "BG20_302";
  public const string Text = "After a <b>Blood Gem</b> is played on this, this plays a <b>Blood Gem</b> on a different friendly minion of each type.";
  public const string GoldenText = "After a <b>Blood Gem</b> is played on this, this plays 2 <b>Blood Gems</b> on a different friendly minion of each type.";

  public Action? OnAfterBloodGemCast(Minion? target)
  {
    return target != this ? (Action) null : (Action) (() =>
    {
      foreach (Minion target1 in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (e => e.CardID != "BG20_302")).ToList<Minion>().GetRandomPerRace())
      {
        for (int index = 0; index < this.DoubleIfGolden(1); ++index)
          this.Simulator.CastBloodGem(target1, (Entity) this);
      }
    });
  }
}
