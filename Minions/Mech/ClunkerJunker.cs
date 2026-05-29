// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.ClunkerJunker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class ClunkerJunker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG29_503";
  public const string Text = "<b>Battlecry:</b> Choose a friendly Mech. <b>Discover</b> a Mech to <b>Magnetize</b> to it.";
  public const string GoldenText = "<b>Battlecry:</b> Choose a friendly Mech. <b>Discover</b> 2 Mechs to <b>Magnetize</b> to it.";

  public static List<Card>? Magnetics { get; set; }

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      if (ClunkerJunker.Magnetics == null)
        ClunkerJunker.Magnetics = this.Simulator.MinionFactory.MinionPoolOptionsPlayer(this.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Entity.GetTag((GameTag) 849) > 0)).ToList<Card>();
      Minion target;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsMech() && m.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
        return;
      Card card;
      for (int index = 0; index < this.DoubleIfGolden(1) && ClunkerJunker.Magnetics != null && ClunkerJunker.Magnetics.TryGetRandom<Card>(out card); ++index)
        this.Simulator.MagnetizeMech(target, card.Id, (Entity) this);
    });
  }
}
