// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.GeomagusRoogug
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class GeomagusRoogug(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterBloodGemCast,
  IEntity
{
  public const string CardId = "BG28_583";
  public const string Text = "<b>Divine Shield</b>. Whenever a <b>Blood Gem</b> is played on this, this plays a <b>Blood Gem</b> on a different friendly minion.";
  public const string GoldenText = "<b>Divine Shield</b>. Whenever a <b>Blood Gem</b> is played on this, this plays 2 <b>Blood Gems</b> on a different friendly minion.";

  public Action? OnAfterBloodGemCast(Minion? target)
  {
    return target != this ? (Action) null : (Action) (() =>
    {
      Minion target1;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.CardID != "BG28_583" && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target1))
        return;
      this.Simulator.CastBloodGem(target1, (Entity) this);
      if (!this.golden)
        return;
      this.Simulator.CastBloodGem(target1, (Entity) this);
    });
  }
}
