// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.TavernSpells.ChefsChoiceSpell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Spells.TavernSpells;

public class ChefsChoiceSpell : ITavernSpell
{
  public const string CardId = "BG28_518";
  public const string Text = "Choose a minion. Get a different minion of the same type.";

  public void Cast(Entity source, Simulator simulator, Minion? target)
  {
    if (target == null || target.IsNoType())
      return;
    List<Card> cardList = source.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(source.ControlledByPlayer, target.PrimaryRace);
    if (target.PrimaryRace != 26 && target.SecondaryRace != null)
    {
      cardList.AddRange((IEnumerable<Card>) source.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(source.ControlledByPlayer, target.SecondaryRace));
      cardList = new List<Card>((IEnumerable<Card>) new HashSet<Card>((IEnumerable<Card>) cardList));
    }
    Card card;
    if (!cardList.TryGetRandom<Card>(out card))
      source.AddMinionToFriendlyHand();
    else
      source.AddMinionToFriendlyHand(card.Id);
  }
}
