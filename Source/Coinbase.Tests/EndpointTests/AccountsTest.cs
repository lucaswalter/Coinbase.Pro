﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Coinbase.Tests.EndpointTests
{
   public class AccountsTest : TestWithAuth
   {
      [Test]
      public async Task list()
      {
         server.RespondWith(Examples.AccountListJson);

         var r = await client.Accounts.List();

         r.Dump();
         var a = r.First();
         a.Currency.Should().Be("BTC");
         a.Id.Should().Be("71452118-efc7-4cc4-8780-a5e22d4baa53");

         server.ShouldHaveExactCall("https://api.pro.coinbase.com/accounts");
      }

      [Test]
      public async Task can_get_account()
      {
         server.RespondWith(Examples.Account1Json);

         var r = await client.Accounts.GetAccount("fff");

         r.Dump();

         r.Currency.Should().Be("BTC");
         r.Id.Should().Be("71452118-efc7-4cc4-8780-a5e22d4baa53");

         server.ShouldHaveExactCall("https://api.pro.coinbase.com/accounts/fff");
      }

      [Test]
      public async Task get_history()
      {
         server.RespondWith(Examples.AccountHistoryJson);

         var r = await client.Accounts.GetAccountHistory("fff");

         r.Dump();

         //r.Before.Should().Be(9);
         var a = r.Data.First();
         a.Id.Should().Be("44512583");
         a.Amount.Should().Be(1000.0000000000000000m);

         server.ShouldHaveExactCall("https://api.pro.coinbase.com/accounts/fff/ledger");
      }

      [Test]
      public async Task get_hold()
      {
         server.RespondWith(Examples.AccountHoldJson);

         var r = await client.Accounts.GetAccountHold("fff");

         r.Dump();

         var h = r.Data.First();
         h.AccountId.Should().Be("e0b3f39a-183d-453e-b754-0c13e5bab0b3");

         server.ShouldHaveExactCall("https://api.pro.coinbase.com/accounts/fff/holds");
      }
   }
}
