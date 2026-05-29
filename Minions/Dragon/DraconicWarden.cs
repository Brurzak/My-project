// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.DraconicWarden
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class DraconicWarden(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG34_633";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Get a random <b>Chromadrake</b>.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Get 2 random <b>Chromadrakes</b>.";
  public static readonly List<string> ChromadrakeCardIds = new List<string>()
  {
    "BG34_638t",
    "BG34_635t",
    "BG34_636t",
    "BG34_637t"
  };

  public Action<Minion> GetDeathrattle() => DraconicWarden.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        string random = DraconicWarden.ChromadrakeCardIds.GetRandom<string>();
        minion.AddMinionToFriendlyHand(random);
      }
    });
  }

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.AddMinionToFriendlyHand(DraconicWarden.ChromadrakeCardIds.GetRandom<string>());
    });
  }
}
