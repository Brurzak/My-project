// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.ShipMasterEudora
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class ShipMasterEudora(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_828";
  public const string Text = "<b>Deathrattle:</b> Give your minions +{0}/+{1}. Golden ones keep it permanently.";
  public const string GoldenText = "<b>Deathrattle:</b> Give your minions +{0}/+{1} twice. Golden ones keep it permanently.";

  public Action<Minion> GetDeathrattle() => ShipMasterEudora.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int by = golden ? 16 /*0x10*/ : 8;
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion1.IncreaseStats(by, (Entity) minion);
    });
  }
}
