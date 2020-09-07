using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public interface ITransaction
    {
        string Id { get; set; }
    }

    public class Transaction : ITransaction
    {
        public Transaction()
        {
        }
        public Transaction(string _id)
        {
            Id = _id;
        }
        public string Id { get; set; }
    }

    public static class TransactionExtension
    {
        public static Transaction GetTransaction(this string transactionId)
        {
            Transaction response;
            if (transactionId.IsNullOrEmpty())
                response = new Transaction(Guid.NewGuid().ToString("N").ToUpper());
            else
                response = new Transaction(transactionId);

            return response;
        }
        public static Transaction NewTransaction(this string transactionId)
        {
            try
            {
                return new Transaction(Guid.NewGuid().ToString("N").ToUpper());
            }
            catch
            {
                return new Transaction("NoTransactionIdGenerated");
            }

        }
    }

    public static class OriginExtension
    {
        public static string GetOrigin(this string origen)
        {
            string response;
            if (!origen.IsNullOrEmpty())
                response = origen;
            else
                response = AppConstants.Unknow;

            return response;
        }
    }
}
