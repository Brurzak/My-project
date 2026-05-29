// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedMagnanimoose
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedMagnanimoose(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_619";
  public const string Text = "<b>Deathrattle:</b> Summon and get a minion from a random opponent's warband.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon and get a minion from a random opponent's warband, twice.";

  public Action<Minion> GetDeathrattle() => TimewarpedMagnanimoose.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden) => (Action<Minion>) (minion => { });
}
