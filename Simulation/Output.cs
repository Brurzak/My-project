// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Output
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class Output
{
  public List<int> damageResults = new List<int>();
  public float winRate;
  public float lossRate;
  public float tieRate;
  public float avDamage;
  public float medianDamage;
  public float myDeathRate;
  public float theirDeathRate;
  public int friendlyHealth;
  public int opponentHealth;
  public int simulationCount;
  public Simulator.ExitConditions myExitCondition;
  private const float RoundingPrecision = 0.001f;

  public Output(IEnumerable<int> results, int friendlyHealth, int opponentHealth)
  {
    this.friendlyHealth = friendlyHealth;
    this.opponentHealth = opponentHealth;
    this.damageResults.AddRange(results);
    this.PullStatsFromResult();
  }

  public void PullStatsFromResult()
  {
    this.damageResults.Sort();
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    float num4 = 0.0f;
    float num5 = 0.0f;
    float num6 = 0.0f;
    foreach (int damageResult in this.damageResults)
    {
      if (damageResult > 0)
      {
        ++num1;
        if (damageResult >= this.opponentHealth)
          ++num6;
      }
      else if (damageResult < 0)
      {
        ++num2;
        if (damageResult * -1 >= this.friendlyHealth)
          ++num5;
      }
      else
        ++num3;
      num4 += (float) damageResult;
    }
    this.winRate = num1 / (float) this.damageResults.Count;
    this.tieRate = num3 / (float) this.damageResults.Count;
    this.lossRate = num2 / (float) this.damageResults.Count;
    this.avDamage = num4 / (float) this.damageResults.Count;
    this.myDeathRate = num5 / (float) this.damageResults.Count;
    this.theirDeathRate = num6 / (float) this.damageResults.Count;
    if (this.damageResults.Count > 0)
      this.medianDamage = (float) this.damageResults[(int) Math.Floor((Decimal) this.damageResults.Count / 2M)];
    this.RoundResultsForDisplay();
  }

  private void RoundResultsForDisplay()
  {
    int num1 = (double) this.winRate == (double) this.theirDeathRate ? 1 : 0;
    bool flag = (double) this.lossRate == (double) this.myDeathRate;
    this.winRate = this.AdjustNumberForDisplay(this.winRate);
    this.tieRate = this.AdjustNumberForDisplay(this.tieRate);
    this.lossRate = this.AdjustNumberForDisplay(this.lossRate);
    this.theirDeathRate = this.AdjustNumberForDisplay(this.theirDeathRate);
    this.myDeathRate = this.AdjustNumberForDisplay(this.myDeathRate);
    if ((double) Math.Abs((float) (1.0 - ((double) this.winRate + (double) this.tieRate + (double) this.lossRate))) > 1E-05)
    {
      float num2 = (float) (1.0 - ((double) this.winRate + (double) this.tieRate + (double) this.lossRate));
      if ((double) this.winRate > (double) this.tieRate && (double) this.winRate > (double) this.lossRate)
        this.winRate += num2;
      else if ((double) this.tieRate > (double) this.winRate && (double) this.tieRate > (double) this.lossRate)
        this.tieRate += num2;
      else
        this.lossRate += num2;
    }
    if (num1 != 0 || (double) this.theirDeathRate > (double) this.winRate)
      this.theirDeathRate = this.winRate;
    if (!flag && (double) this.myDeathRate <= (double) this.lossRate)
      return;
    this.myDeathRate = this.lossRate;
  }

  private float AdjustNumberForDisplay(float input)
  {
    return (float) Math.Round((double) this.RoundAwayFromZeroOrOne(input), 3);
  }

  private float RoundAwayFromZeroOrOne(float value)
  {
    if ((double) value < 1.0 / 1000.0 && (double) value != 0.0)
      return 1f / 1000f;
    return (double) value > 0.99900001287460327 && (double) value != 1.0 ? 0.999f : value;
  }

  public bool IsDifferntFrom(
    Output compareTo,
    double percentMarginOfError = 0.05,
    double averageDamageMarginOfError = 0.1)
  {
    this.PullStatsFromResult();
    compareTo.PullStatsFromResult();
    return !this.AreWithin((double) this.winRate, (double) compareTo.winRate, percentMarginOfError) || !this.AreWithin((double) this.tieRate, (double) compareTo.tieRate, percentMarginOfError) || !this.AreWithin((double) this.lossRate, (double) compareTo.lossRate, percentMarginOfError) || !this.AreWithin((double) this.avDamage, (double) compareTo.avDamage, averageDamageMarginOfError);
  }

  public bool AreWithin(double a, double b, double margin) => Math.Abs(a - b) <= margin;

  public void ClearListsForReporting() => this.damageResults.Clear();

  public override string ToString()
  {
    return $"{this.myDeathRate.ToString()} {this.winRate.ToString()} {this.tieRate.ToString()} {this.lossRate.ToString()} {this.theirDeathRate.ToString()} {this.avDamage.ToString()}";
  }
}
