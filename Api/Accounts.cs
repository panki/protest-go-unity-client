using RSG;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ProtestGoClient
{
    namespace Req
    {
        [Serializable]
        public class AccountExchange
        {
            public string from;
            public string to;
            public uint amount;
        }
    }

    namespace Res
    {

        [Serializable]
        public class Account
        {
            public uint realAmount;
            public uint liberoAmount;
            public uint orderoAmount;
        }

        [Serializable]
        public class AccountResponse
        {
            public Account account;
        }
    }

    public static partial class Client
    {
        public static class Accounts
        {
            public static IPromise<Res.Account> My()
            {
                return get<Res.AccountResponse>("/accounts/my").Then(res => res.account);
            }

            public static IPromise<Res.Account> Exchange(string from, string to, uint amount)
            {
                Req.AccountExchange req = new Req.AccountExchange { from = from, to = to, amount = amount };
                return post<Res.AccountResponse>("/accounts/exchange", req)
                .Then(res => res.account);
            }

            public static IPromise<Res.Account> ExchangeReal2Libero(uint amount)
            {
                return Exchange(Constants.Currency.Real, Constants.Currency.Libero, amount);
            }

            public static IPromise<Res.Account> ExchangeReal2Ordero(uint amount)
            {
                return Exchange(Constants.Currency.Real, Constants.Currency.Ordero, amount);
            }
        }
    }
}