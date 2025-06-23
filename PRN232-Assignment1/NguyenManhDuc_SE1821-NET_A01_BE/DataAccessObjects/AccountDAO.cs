using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        public SystemAccount GetUserByEmailAndPassword(string email, string password)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.SystemAccounts.FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
            }
        }

        public List<SystemAccount> GetAllUsers()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.SystemAccounts.ToList();
            }
        }

        public void DeleteUser(short userId)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.Include(a => a.NewsArticles).FirstOrDefault(a => a.AccountId == userId);
                if (user.NewsArticles.Count > 0) throw new Exception("Cannot delete user.");
                if (user != null)
                {
                    context.SystemAccounts.Remove(user);
                    context.SaveChanges();
                }
            }
        }
        public void ToggleAccountStatus(short id)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
                if (user != null)
                {
                    user.AccountRole = (user.AccountRole == -1) ? 1 : -1; // Change Role to Active or Inactive
                    context.SaveChanges();
                }
            }
        }
        public void UpdateUserRole(short id, int role)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
                if (user != null && (role == 1 || role == 2))
                {
                    user.AccountRole = role;
                    context.SaveChanges();
                }
            }
        }
        public SystemAccount? GetUserById(short id)
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(u => u.AccountId == id);
        }

        public void UpdateAccount(SystemAccount user)
        {
            using var context = new FunewsManagementContext();
            var currentUser = context.SystemAccounts.FirstOrDefault(a => a.AccountId == user.AccountId);
            currentUser.AccountEmail = user.AccountEmail;
            currentUser.AccountName = currentUser.AccountName;
            context.SystemAccounts.Update(currentUser);
            context.SaveChanges();
        }
        public bool ChangePassword(short userId, string currentPassword, string newPassword)
        {
            using var context = new FunewsManagementContext();
            var user = GetUserById(userId);
            if (user == null || user.AccountPassword != currentPassword) return false;

            user.AccountPassword = newPassword;
            context.SystemAccounts.Update(user);
            context.SaveChanges();
            return true;
        }
        public void CreateUser(SystemAccount newUser)
        {
            using var context = new FunewsManagementContext();

            // Optional: Check if email already exists
            var exists = context.SystemAccounts.Any(u => u.AccountEmail == newUser.AccountEmail);
            if (exists)
            {
                throw new Exception("Email already exists.");
            }

            // Set AccountId to max + 1
            short maxId = context.SystemAccounts.Any()
                ? context.SystemAccounts.Max(u => u.AccountId)
                : (short)0;

            newUser.AccountId = (short)(maxId + 1);

            context.SystemAccounts.Add(newUser);
            context.SaveChanges();
        }
    }
}
