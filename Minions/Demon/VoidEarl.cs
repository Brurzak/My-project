// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.VoidEarl
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class VoidEarl(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_157";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Summon two 1/3 Demons with <b>Taunt</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Summon four 1/3 Demons with <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => VoidEarl.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinions(new Summon("BG_CS2_065"), new Summon("BG_CS2_065"));
      if (!golden)
        return;
      minion.TrySummonMinions(new Summon("BG_CS2_065"), new Summon("BG_CS2_065"));
    });
  }
}
