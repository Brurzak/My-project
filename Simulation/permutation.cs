// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.permutation
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class permutation
{
  public List<int> ordering = new List<int>();
  public List<int> damageResults = new List<int>();

  public float returnAvDamage()
  {
    float num = 0.0f;
    foreach (int damageResult in this.damageResults)
      num += (float) damageResult;
    return num / (float) this.damageResults.Count;
  }

  public permutation(List<int> l)
  {
    for (int index = 0; index < l.Count; ++index)
      this.ordering.Add(l[index]);
  }

  public void printMe() => Console.WriteLine("i had avdamage " + this.returnAvDamage().ToString());
}
