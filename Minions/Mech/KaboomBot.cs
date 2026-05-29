// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.KaboomBot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class KaboomBot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_BOT_606";
  public const string Text = "<b>Deathrattle:</b> Deal {0} damage to a random enemy minion.4<b>Deathrattle:</b> Deal <b>*{0}*</b> damage to a random enemy minion.";
  public const string GoldenText = "<b>Deathrattle:</b> Deal {0} damage to a random enemy minion, twice.4<b>Deathrattle:</b> Deal <b>*{0}*</b> damage to a random enemy minion, twice.";

  public Action<Minion> GetDeathrattle() => KaboomBot.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      int amount = 4 + minion.FriendlyEntities.Sum<Entity>((Func<Entity, int>) (x => !(x is KaboomBotPortrait kaboomBotPortrait2) ? 0 : kaboomBotPortrait2.ExtraBoomBotDamage()));
      for (int index = 0; index < num; ++index)
      {
        Minion target;
        if (minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
          minion.Simulator.ProcessDamage(amount, target, (Entity) minion);
      }
    });
  }
}
