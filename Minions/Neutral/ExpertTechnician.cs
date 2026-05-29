// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ExpertTechnician
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class ExpertTechnician(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_370";
  public const string Text = "<b>Taunt</b>. <b>Deathrattle:</b> Your left- most minion immediately attacks the enemy minion that killed this.";
  public const string GoldenText = "<b>Taunt</b>. <b>Deathrattle:</b> Your left- most minion immediately attacks the enemy minion that killed this, twice.";

  public Action<Minion> GetDeathrattle() => ExpertTechnician.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion.KilledBy is Minion killedBy2) || !killedBy2.IsAlive() || killedBy2.ControlledByPlayer == minion.ControlledByPlayer)
        return;
      Minion attacker = minion.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (m => m.IsAlive()));
      if (attacker == null || !attacker.ControlledByPlayer)
        return;
      minion.Simulator.AttackWithMinion(attacker, killedBy2);
      if (!golden || !attacker.IsAlive())
        return;
      minion.Simulator.AttackWithMinion(attacker, killedBy2);
    });
  }
}
