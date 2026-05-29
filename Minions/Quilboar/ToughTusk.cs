// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.ToughTusk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class ToughTusk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterBloodGemCast,
  IEntity
{
  public const string CardId = "BG20_102";
  public const string Text = "After a <b>Blood Gem</b> is played on this, gain <b>Divine Shield</b> until next turn.";
  public const string GoldenText = "After a <b>Blood Gem</b> is played on this, gain <b>Divine Shield</b>.";

  public Action? OnAfterBloodGemCast(Minion? target)
  {
    return target == this ? (Action) (() => this.div = 1) : (Action) null;
  }
}
