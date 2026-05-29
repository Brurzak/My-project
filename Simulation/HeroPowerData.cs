// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.HeroPowerData
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.Simulation;

public class HeroPowerData
{
  public string CardId { get; set; } = "";

  public bool IsActivated { get; set; }

  public int Data { get; set; }

  public int Data2 { get; set; }

  public int Data3 { get; set; }

  public Minion? AttachedMinion { get; set; }

  public HeroPowerData Clone(Simulator simulator)
  {
    return new HeroPowerData()
    {
      CardId = this.CardId,
      IsActivated = this.IsActivated,
      Data = this.Data,
      Data2 = this.Data2,
      Data3 = this.Data3,
      AttachedMinion = this.AttachedMinion?.Clone(simulator)
    };
  }
}
