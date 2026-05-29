// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.AshenCorruptor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class AshenCorruptor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyHeroDamagedRewind,
  IEntity
{
  public const string CardId = "BG32_873";
  public const string Text = "After your hero takes damage, rewind it and give minions in the Tavern +{0}/+{1} this turn.";
  public const string GoldenText = "After your hero takes damage, rewind it and give minions in the Tavern +{0}/+{1} this turn.";
}
