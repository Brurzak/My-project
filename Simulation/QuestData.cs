// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.QuestData
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.Simulation;

public class QuestData
{
  public string QuestCardId { get; set; } = "";

  public string RewardCardId { get; set; } = "";

  public int QuestProgress { get; set; }

  public int QuestProgressTotal { get; set; }

  public int RewardScriptDataNum1 { get; set; }

  public int RewardScriptDataNum2 { get; set; }
}
