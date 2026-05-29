// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedTideRazor
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
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedTideRazor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_328";
  public const string Text = "<b>Deathrattle:</b> Summon and get {0} random Pirates.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon and get {0} random Pirates.";

  public Action<Minion> GetDeathrattle() => TimewarpedTideRazor.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 8 : 4;
      List<Card> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(minion.ControlledByPlayer, (Race) 23).Where<Card>((Func<Card, bool>) (x => x.Id != "BG34_Giant_328")).ToList<Card>();
      for (int index = 0; index < num; ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
        {
          minion.AddMinionToFriendlyHand();
        }
        else
        {
          minion.TrySummonMinion((Summon) card.Id);
          minion.AddMinionToFriendlyHand(card.Id);
        }
      }
    });
  }
}
