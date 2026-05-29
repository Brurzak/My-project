// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.LeeroyTheReckless
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class LeeroyTheReckless(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG23_318";
  public const string Text = "<b>Deathrattle:</b> Destroy the minion that killed this.";
  public const string GoldenText = "<b>Deathrattle:</b> Destroy the minion that killed this.";

  public Action<Minion> GetDeathrattle() => LeeroyTheReckless.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion.KilledBy is Minion killedBy2) || !killedBy2.IsAlive() || killedBy2.ControlledByPlayer == minion.ControlledByPlayer)
        return;
      minion.Simulator.Destroy(killedBy2, (Entity) minion);
    });
  }
}
