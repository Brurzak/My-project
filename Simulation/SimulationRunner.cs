// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.SimulationRunner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace BobsBuddy.Simulation;

public class SimulationRunner
{
  public async Task<Output> SimulateMultiThreaded(
    Input input,
    int maxIterations,
    int threadCount,
    int maxDuration = 1500)
  {
    ConcurrentBag<Output> generatedOutputs = new ConcurrentBag<Output>();
    ParallelLoopResult parallelLoopResult = await Task.Factory.StartNew<ParallelLoopResult>((Func<ParallelLoopResult>) (() => Parallel.For(0, threadCount, new Action<int>(RunSimulator))));
    Output output1 = new Output(generatedOutputs.SelectMany<Output, int>((Func<Output, IEnumerable<int>>) (x => (IEnumerable<int>) x.damageResults)), input.Player.Health, input.Opponent.Health);
    output1.simulationCount = output1.damageResults.Count;
    output1.myExitCondition = Simulator.ExitConditions.CompletedSimulations;
    int num1 = 0;
    int num2 = 0;
    foreach (Output output2 in generatedOutputs)
    {
      if (output2.myExitCondition == Simulator.ExitConditions.Converge)
        ++num2;
      if (output2.myExitCondition == Simulator.ExitConditions.Time)
        ++num1;
    }
    if (num1 > 0 || num2 > 0)
      output1.myExitCondition = num1 > num2 ? Simulator.ExitConditions.Time : Simulator.ExitConditions.Converge;
    return output1;

    void RunSimulator(int _)
    {
      generatedOutputs.Add(new Simulator().SimulateForInput(input, maxIterations / threadCount, maxDuration));
    }
  }
}
