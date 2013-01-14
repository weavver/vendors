/*
    Copyright ©2002-2008 D. P. Bullington
    Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftwareIsHardwork.Tools.QifConvUtil
{
     public class NonInvestmentAccount
     {
          #region Constructors/Destructors

          public NonInvestmentAccount()
          {
          }

          #endregion

          #region Fields/Constants

          private readonly IList<NonInvestmentAccountTransaction> transactions = new List<NonInvestmentAccountTransaction>();

          #endregion

          #region Properties/Indexers/Events

          public double Balance
          {
               get
               {
                    double balanace = 0.0;

                    foreach (NonInvestmentAccountTransaction transaction in this.transactions)
                         balanace += transaction.Amount ?? 0.0;

                    return balanace;
               }
          }

          public int Count
          {
               get
               {
                    return this.transactions.Count;
               }
          }

          public IEnumerable<NonInvestmentAccountTransaction> Transactions
          {
               get
               {
                    return this.transactions;
               }
          }

          #endregion

          #region Methods/Operators

          public void PostTransaction(NonInvestmentAccountTransaction transaction)
          {
               if (transaction == null)
                    throw new ArgumentNullException("transaction");

               if (this.transactions.Contains(transaction))
                    throw new InvalidOperationException("Transaction already exists in account.");

               this.transactions.Add(transaction);
          }

          #endregion
     }

     public class NonInvestmentAccountTransaction
     {
          #region Constructors/Destructors

          public NonInvestmentAccountTransaction(double? amount,
                                                 string category,
                                                 bool? cleared,
                                                 DateTime? date,
                                                 string memo,
                                                 string number,
                                                 string payee)
          {
               this.amount = amount;
               this.category = category;
               this.cleared = cleared;
               this.date = date;
               this.memo = memo;
               this.number = number;
               this.payee = payee;
          }

          #endregion

          #region Fields/Constants

          private readonly double? amount;
          private readonly string category;
          private readonly bool? cleared;
          private readonly DateTime? date;
          private readonly string memo;
          private readonly string number;
          private readonly string payee;

          #endregion

          #region Properties/Indexers/Events

          public double? Amount
          {
               get
               {
                    return this.amount;
               }
          }

          public string Category
          {
               get
               {
                    return this.category;
               }
          }

          public bool? Cleared
          {
               get
               {
                    return this.cleared;
               }
          }

          public DateTime? Date
          {
               get
               {
                    return this.date;
               }
          }

          public string Memo
          {
               get
               {
                    return this.memo;
               }
          }

          public string Number
          {
               get
               {
                    return this.number;
               }
          }

          public string Payee
          {
               get
               {
                    return this.payee;
               }
          }

          #endregion
     }

     public static class QifParser
     {
          #region Methods/Operators

          public static void FormatToTsvFile(NonInvestmentAccount account, string filePath)
          {
               StreamWriter swa, swp, swc;
               string line;
               Dictionary<string, object> payees, categories;

               if (account == null)
                    throw new ArgumentNullException("account");

               payees = new Dictionary<string, object>();
               categories = new Dictionary<string, object>();

               using (swa = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 32, FileOptions.None), Encoding.ASCII))
               {
                    foreach (NonInvestmentAccountTransaction transaction in account.Transactions)
                    {
                         if (!payees.ContainsKey(transaction.Payee ?? ""))
                              payees.Add(transaction.Payee ?? "", null);

                         if (!categories.ContainsKey(transaction.Category ?? ""))
                              categories.Add(transaction.Category ?? "", null);

                         line = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
                                              transaction.Number, transaction.Payee, transaction.Category, transaction.Date.Value.ToString("MM/dd/yyyy"),
                                              (transaction.Amount ?? 0).ToString("n"), transaction.Cleared ?? false, transaction.Memo);
                         swa.Write(line);
                    }

                    using (swp = new StreamWriter(new FileStream(filePath + ".pay", FileMode.Create, FileAccess.Write, FileShare.None, 32, FileOptions.None), Encoding.ASCII))
                    {
                         foreach (KeyValuePair<string, object> payee in payees)
                              swp.WriteLine(payee.Key);
                    }

                    using (swc = new StreamWriter(new FileStream(filePath + ".cat", FileMode.Create, FileAccess.Write, FileShare.None, 32, FileOptions.None), Encoding.ASCII))
                    {
                         foreach (KeyValuePair<string, object> category in categories)
                              swc.WriteLine(category.Key);
                    }
               }
          }

          public static NonInvestmentAccount ParseNonInvestmentAccountFile(string filePath)
          {
               StreamReader sr;
               string line;
               NonInvestmentAccount account;
               NonInvestmentAccountTransaction transaction;
               Dictionary<string, object> context;
               const string QIF_HEADER = "!Type:Bank";
               const string QIF_TX_TERM = "^";
               const string QIF_CLRDIND = "*";

               const string QIF_KEY_DATE = "D";
               const string QIF_KEY_AMOUNT = "T";
               const string QIF_KEY_CLEARED = "C";
               const string QIF_KEY_NUMBER = "N";
               const string QIF_KEY_PAYEE = "P";
               const string QIF_KEY_MEMO = "M";
               const string QIF_KEY_CATEGORY = "L";

               account = new NonInvestmentAccount();
               context = new Dictionary<string, object>();

               using (sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 32, FileOptions.None), Encoding.ASCII))
               {
                    if ((line = sr.ReadLine() ?? "") != "")
                    {
                         if (line != QIF_HEADER && line != "!Type:CCard")
                              throw new InvalidOperationException("Invalid QIF header: " + line);
                    }

                    while ((line = sr.ReadLine() ?? "") != "")
                    {
                         if (line == QIF_TX_TERM)
                         {
                              if (context.Count == 0)
                                   throw new InvalidOperationException("Invalid QIF transaction context");

                              transaction = new NonInvestmentAccountTransaction(
                                  context.ContainsKey(QIF_KEY_AMOUNT) ? (double?)context[QIF_KEY_AMOUNT] : null,
                                  context.ContainsKey(QIF_KEY_CATEGORY) ? (string)context[QIF_KEY_CATEGORY] : null,
                                  context.ContainsKey(QIF_KEY_CLEARED) ? (bool?)context[QIF_KEY_CLEARED] : null,
                                  context.ContainsKey(QIF_KEY_DATE) ? (DateTime?)context[QIF_KEY_DATE] : null,
                                  context.ContainsKey(QIF_KEY_MEMO) ? (string)context[QIF_KEY_MEMO] : null,
                                  context.ContainsKey(QIF_KEY_NUMBER) ? (string)context[QIF_KEY_NUMBER] : null,
                                  context.ContainsKey(QIF_KEY_PAYEE) ? (string)context[QIF_KEY_PAYEE] : null);

                              account.PostTransaction(transaction);

                              context.Clear();
                              continue;
                         }
                         else
                         {
                              string key;
                              object value;

                              if (line.Length < 2)
                                   throw new InvalidOperationException("Invalid QIF item length: " + line);

                              key = line[0].ToString();
                              line = line.Substring(1);

                              switch (key)
                              {
                                   case QIF_KEY_AMOUNT:
                                        value = double.Parse(line);
                                        break;

                                   case QIF_KEY_CATEGORY:
                                        value = line.ToUpper();
                                        break;

                                   case QIF_KEY_CLEARED:
                                        value = line == QIF_CLRDIND;
                                        break;

                                   case QIF_KEY_DATE:
                                        value = DateTime.Parse(line.Replace("'", "/"));
                                        break;

                                   case QIF_KEY_MEMO:
                                        value = line.ToUpper();
                                        break;

                                   case QIF_KEY_NUMBER:
                                        value = line.ToUpper();
                                        break;

                                   case QIF_KEY_PAYEE:
                                        value = line.ToUpper();
                                        break;

                                   default:
                                        throw new InvalidOperationException("Invalid QIF item key: " + key);
                              }

                              context.Add(key, value);
                         }
                    }
               }

               return account;
          }

          #endregion
     }
}