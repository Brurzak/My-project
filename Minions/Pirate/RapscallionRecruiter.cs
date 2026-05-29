// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.RapscallionRecruiter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class RapscallionRecruiter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_018";
  public const string Text = "<b>Deathrattle:</b> Summon 2 Scallywags.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 2 Golden Scallywags.";

  public Action<Minion> GetDeathrattle() => RapscallionRecruiter.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BGS_061", golden), new Summon("BGS_061", golden)));
  }
}
