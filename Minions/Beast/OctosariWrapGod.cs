// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.OctosariWrapGod
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class OctosariWrapGod(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IOnFriendlyMinionSummoned
{
  public const string CardId = "BG26_804";
  public const string Text = "<b>Deathrattle:</b> Summon a 8/8 Tentacle. <i>(It gains +4/+4 permanently after you summon a minion in combat!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 16/16 Tentacle. <i>(It gains +8/+8 permanently after you summon a minion in combat!)</i>";
  public const string SummonCardId = "BG26_803t";

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() => this.ScriptDataNum1 += this.DoubleIfGolden(2));
  }

  public Action<Minion> GetDeathrattle() => OctosariWrapGod.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion fromCardId = minion.Simulator.MinionFactory.CreateFromCardId("BG26_803t", minion.ControlledByPlayer);
      if (golden)
        fromCardId.golden = true;
      fromCardId.baseAttack = minion.ScriptDataNum1;
      fromCardId.baseHealth = minion.ScriptDataNum1;
      minion.TrySummonMinion((Summon) fromCardId);
    });
  }
}
