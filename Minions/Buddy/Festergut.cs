// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Festergut
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Factory;
using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Festergut(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_HERO_100_Buddy";
  public const string Text = "<b>Deathrattle:</b> Summon and get a random Undead Creation.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon and get 2 random Undead Creations.";

  private List<Minion> _pool1
  {
    get
    {
      return MinionFactory.CardIdsPutricidePool1.Select<string, Minion>((Func<string, Minion>) (x => this.Simulator.MinionFactory.CreateFromCardId(x, this.ControlledByPlayer))).ToList<Minion>();
    }
  }

  private List<Minion> _pool2
  {
    get
    {
      return MinionFactory.CardIdsPutricidePool2.Select<string, Minion>((Func<string, Minion>) (x => this.Simulator.MinionFactory.CreateFromCardId(x, this.ControlledByPlayer))).ToList<Minion>();
    }
  }

  public Action<Minion> GetDeathrattle() => Festergut.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is Festergut festergut2))
        return;
      int tier = festergut2.ControlledByPlayer ? festergut2.Simulator.PlayerState.Tier : festergut2.Simulator.OpponentState.Tier;
      List<Minion> list1 = festergut2._pool1.Where<Minion>((Func<Minion, bool>) (x => x.tier <= tier)).ToList<Minion>();
      List<Minion> list2 = festergut2._pool2.Where<Minion>((Func<Minion, bool>) (x => x.tier <= tier)).ToList<Minion>();
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        Minion minion2;
        if (list1.TryGetRandom<Minion>(out minion1) && list2.TryGetRandom<Minion>(out minion2))
        {
          Minion minion3 = MinionFactory.CombineMinions(minion1, minion2);
          festergut2.TrySummonMinion((Summon) minion3);
          festergut2.AddCardToFriendlyHand(new CardEntity(minion3.CardID, (Entity) minion, minion.Simulator));
        }
      }
    });
  }
}
