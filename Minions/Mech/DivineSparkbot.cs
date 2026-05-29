// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.DivineSparkbot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class DivineSparkbot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_809";
  public const string Text = "<b>Taunt</b>, <b>Divine Shield</b> <b>Deathrattle:</b> Get a Sanctify.";
  public const string GoldenText = "<b>Taunt</b>, <b>Divine Shield</b> <b>Deathrattle:</b> Get 2 Sanctifies.";

  public Action<Minion> GetDeathrattle() => DivineSparkbot.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!golden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
