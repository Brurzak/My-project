// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BalladistPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BalladistPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG30_MagicItem_987";
}
