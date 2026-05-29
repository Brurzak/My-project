// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedRadioStar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedRadioStar(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_330";
  public const string Text = "<b>Deathrattle:</b> Get a copy of the minion that killed this with full Health and enchantments.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 copies of the minion that killed this with full Health and enchantments.";

  public Action<Minion> GetDeathrattle() => TimewarpedRadioStar.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion.KilledBy is Minion killedBy2))
        return;
      Minion fromCardId = minion.Simulator.MinionFactory.CreateFromCardId(minion.KilledBy.CardID, minion.ControlledByPlayer);
      fromCardId.SetStats(new int?(killedBy2.maxAttack), new int?(killedBy2.maxHealth));
      minion.AddCardToFriendlyHand((CardEntity) new MinionCardEntity(fromCardId.Clone(), (Entity) minion, minion.Simulator));
      if (!golden)
        return;
      minion.AddCardToFriendlyHand((CardEntity) new MinionCardEntity(fromCardId.Clone(), (Entity) minion, minion.Simulator));
    });
  }
}
