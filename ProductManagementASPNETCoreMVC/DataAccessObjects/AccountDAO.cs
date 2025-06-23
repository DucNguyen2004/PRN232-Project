using BusinessObjects;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        public static AccountNumber GetAccountById(string accountId)
        {
            using var db = new MyStoreDbContext();
            return db.AccountNumbers.FirstOrDefault(c => c.MemberId.Equals(accountId));
        }
    }
}
