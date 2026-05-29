// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.MechanoEgg
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class MechanoEgg(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BOT_537";
  public const string Text = "<b>Deathrattle:</b> Summon an 8/8 Robosaur.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 16/16 Robosaur.";

  public Action<Minion> GetDeathrattle() => MechanoEgg.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BOT_537t", golden)));
  }
}
