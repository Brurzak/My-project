// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.IncubationResearcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class IncubationResearcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_632";
  public const string Text = "<b>Avenge ({0}):</b> Get a random <b>Chromadrake</b>.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 random <b>Chromadrakes</b>.";
  public static readonly List<string> ChromadrakeCardIds = new List<string>()
  {
    "BG34_638t",
    "BG34_635t",
    "BG34_636t",
    "BG34_637t"
  };

  public int AvengeRequirement => this.ScriptDataNum1;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.AddMinionToFriendlyHand(IncubationResearcher.ChromadrakeCardIds.GetRandom<string>());
    });
  }
}
