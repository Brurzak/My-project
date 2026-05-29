// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.MechanizedGiftHorse
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class MechanizedGiftHorse(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG27_008";
  public const string Text = "<b>Deathrattle:</b> Summon two 2/2 Mechorses with \"<b>Deathrattle:</b> Summon a 1/1 Mechapony.\"";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 4/4 Mechorses with \"<b>Deathrattle:</b> Summon a 2/2 Mechapony.\"";

  public Action<Minion> GetDeathrattle() => MechanizedGiftHorse.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinion(new Summon("BG27_008t", golden));
      minion.TrySummonMinion(new Summon("BG27_008t", golden));
    });
  }
}
