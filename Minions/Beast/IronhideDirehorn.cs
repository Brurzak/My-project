// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.IronhideDirehorn
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class IronhideDirehorn(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "TRL_232";
  public const string Text = "<b>Overkill:</b> Summon a 5/5 Ironhide Runt.";

  public override Action? OnOverkill(Minion killed, int overkillDamage)
  {
    return (Action) (() => this.TrySummonMinion(new Summon("TRL_232t", this.golden)));
  }
}
