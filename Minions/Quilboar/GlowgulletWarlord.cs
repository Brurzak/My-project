// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.GlowgulletWarlord
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class GlowgulletWarlord(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_430";
  public const string Text = "<b>Deathrattle:</b> Summon two 1/1 Quilboar with <b>Taunt</b>. This plays a <b>Blood Gem</b> on them.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 2/2 Quilboar with <b>Taunt</b>. This plays 2 <b>Blood Gems</b> on them.";

  public Action<Minion> GetDeathrattle() => GlowgulletWarlord.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> source = minion.TrySummonMinions(new Summon("BG32_430t", golden), new Summon("BG32_430t", golden));
      if (!source.Any<Minion>())
        return;
      foreach (Minion target in source)
      {
        minion.Simulator.CastBloodGem(target, (Entity) minion);
        if (golden)
          minion.Simulator.CastBloodGem(target, (Entity) minion);
      }
    });
  }
}
