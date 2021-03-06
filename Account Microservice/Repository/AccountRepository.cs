﻿using Account_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Repository
{
   

    public class AccountRepository : IAccountRepository
    {
       public static DateTime d;
       public static int checkno= 78;
       public static int countac = 0;
        public static int countst = 0;

      public static  List<Account> accounts = new List<Account>();
        public static List<Statement> statements = new List<Statement>();

        public AccountCreationStatus AddAccount(int CId, string AType)
        {
            Account a = new Account();
            countac=countac+1;
            a.AccountId = countac;
            a.CustomerId = CId;
            a.Balance = 1000;
            a.AccountType = AType;
            accounts.Add(a);
            return new AccountCreationStatus() { Message = "Account has been successfully created", AccountId = a.AccountId };
        }

        public TransactionStatus depositAccount(int AId, int amount)
        {
            int sbalance=0, dbalance=0;
            foreach(var item in accounts)
            {
                if (item.AccountId == AId)
                {
                    sbalance = item.Balance;
                    item.Balance = item.Balance+amount;
                    dbalance = item.Balance;

                    Statement s = new Statement();
                    countst=countst+1;
                    s.StatementId = countst;
                    s.AccountId = AId;
                    d = DateTime.Now;
                   s.date = d;

                    checkno += 50;
                    s.refno = Convert.ToString(checkno);
                    s.ValueDate = d;//d.AddDays(5);
                    s.Withdrawal = 0;
                    s.Deposit = amount;
                    s.ClosingBalance = dbalance;
                    statements.Add(s);

                    d = s.date;
                    break;
                }
            }
            return new TransactionStatus() { Message = "Your account has been credited", source_balance = sbalance, destination_balance = dbalance };
        }

        public IEnumerable<Account> getAllAccounts(int CId)
        {
            List<Account> li = new List<Account>();
            foreach(var item in accounts)
            {
                if (item.CustomerId == CId)
                {
                    li.Add(item);
                }
            }
            return li;
        }

        public Account getCustomerAccount(int AId)
        {
            Account a = new Account();
            foreach (var item in accounts)
            {
                if (item.AccountId == AId)
                {
                    a = item ;
                }
            }
            return a;
        }

        public IEnumerable<Statement> getStatement(int AId, DateTime from_date, DateTime to_date)
        {
            DateTime tempDate = Convert.ToDateTime("1/1/0001 12:00:00 AM");
            List<Statement> li = new List<Statement>();
            if (from_date==tempDate)
            {

              
                    DateTime f = DateTime.Now, t = DateTime.Now;
                    f = f.AddMonths(-1);
                    foreach (var item in statements)
                    {
                        if (item.AccountId == AId && item.date >= f && item.date <= t)
                        {
                            li.Add(item);
                        }
                    }
            }
            else
            {
                foreach(var item in statements)
                {
                    if (item.AccountId == AId && item.date >= from_date && item.date <= to_date)
                    {
                        li.Add(item);
                    }
                }
                
            }
           
            
            return li;
        }

        public TransactionStatus withdrawAccount(int AId, int amount)
        {
            int sbalance = 0, dbalance = 0;
            foreach (var item in accounts)
            {
                if (item.AccountId == AId)
                {
                    sbalance = item.Balance;
                    item.Balance = item.Balance - amount;
                    dbalance = item.Balance;

                    Statement s = new Statement();
                    countst=countst+1;
                    s.StatementId = countst;
                    s.AccountId = AId;
                    d =DateTime.Now;
                    s.date = d;
                    checkno += 499;
                    s.refno = Convert.ToString(checkno);
                    s.ValueDate = d;//d.AddDays(5);
                    s.Withdrawal = amount;
                    s.Deposit = 0;
                    s.ClosingBalance = dbalance;
                    statements.Add(s);

                    d = s.date;
                    break;
                }
            }
            return new TransactionStatus() { Message = "Your account has been debited", source_balance = sbalance, destination_balance = dbalance };

        }
    }
}
