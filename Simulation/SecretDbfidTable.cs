// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.SecretDbfidTable
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using HearthDb;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public static class SecretDbfidTable
{
  public static Dictionary<int, Simulator.Secrets> secretDbfidTable = new Dictionary<int, Simulator.Secrets>();

  static SecretDbfidTable()
  {
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_07", Simulator.Secrets.AutodefenseMatrix);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_08", Simulator.Secrets.Avenge);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_13", Simulator.Secrets.CompetitiveSpirit);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_15", Simulator.Secrets.PackTactics);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_14", Simulator.Secrets.Reckoning);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_10", Simulator.Secrets.Redemption);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_02", Simulator.Secrets.SnakeTrap);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_01", Simulator.Secrets.VenomstrikeTrap);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_04", Simulator.Secrets.SplittingImage);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_12", Simulator.Secrets.IceBlock);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_10b", Simulator.Secrets.BetterRedemption);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_07b", Simulator.Secrets.BetterAutodefenseMatrix);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_15b", Simulator.Secrets.BetterPackTactics);
    SecretDbfidTable.AddSecretDbfidForSecret("TB_Bacon_Secrets_01b", Simulator.Secrets.BetterVenomstrikeTrap);
  }

  private static void AddSecretDbfidForSecret(string secretID, Simulator.Secrets toAdd)
  {
    Card card;
    if (!Cards.All.TryGetValue(secretID, out card))
      return;
    SecretDbfidTable.secretDbfidTable[card.DbfId] = toAdd;
  }
}
