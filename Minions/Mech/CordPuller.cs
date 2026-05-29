// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.CordPuller
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class CordPuller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_611";
  public const string Text = "<b>Divine Shield</b> <b>Deathrattle:</b> Summon a 1/1 Microbot.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Deathrattle:</b> Summon a 2/2 Microbot.";

  public Action<Minion> GetDeathrattle() => CordPuller.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BG_BOT_312t", golden)));
  }
}
