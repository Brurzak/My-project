// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.LaboratoryAssistant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class LaboratoryAssistant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG35_150";
  public const string Text = "<b>Battlecry:</b> Add a <b>Fodder</b> to your next {0} <b>Refreshes</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Add two <b>Fodders</b> to your next 3 <b>Refreshes</b>.";

  public Action? OnBattlecry() => (Action) null;
}
