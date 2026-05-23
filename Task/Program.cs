
using System;

public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        this.Name = name;
        this.Balance = balance;
    }

    public bool Deposit(double amount)
    {
        if (amount < 0)
            return false;
        else
        {
            Balance += amount;
            return true;
        }
    }

    public bool Withdraw(double amount)
    {
        if (Balance - amount >= 0)
        {
            Balance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

class Program
{
    static void Main()
    {
        // 1. Accounts العادية
        var accounts = new List<Account>
        {
            new Account(),
            new Account("Larry"),
            new Account("Moe", 2000),
            new Account("Curly", 5000)
        };

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        // 2. Savings حسابات التوفير
        var savAccounts = new List<SavingsAccount>
        {
            new SavingsAccount(),
            new SavingsAccount("Superman"),
            new SavingsAccount("Batman", 2000),
            new SavingsAccount("Wonderwoman", 5000, 5.0)
        };

        AccountUtil.Deposit(savAccounts, 1000);
        AccountUtil.Withdraw(savAccounts, 2000);

        // 3. Checking الحسابات الجارية
        var checAccounts = new List<CheckingAccount>
        {
            new CheckingAccount(),
            new CheckingAccount("Larry2"),
            new CheckingAccount("Moe2", 2000),
            new CheckingAccount("Curly2", 5000)
        };

        AccountUtil.Deposit(checAccounts, 1000);
        AccountUtil.Withdraw(checAccounts, 2000);
        AccountUtil.Withdraw(checAccounts, 2000);

        // 4. Trust حسابات الأمانة
        var trustAccounts = new List<TrustAccount>
        {
            new TrustAccount(),
            new TrustAccount("Superman2"),
            new TrustAccount("Batman2", 2000),
            new TrustAccount("Wonderwoman2", 5000, 5.0)
        };

        AccountUtil.Deposit(trustAccounts, 1000);
        AccountUtil.Deposit(trustAccounts, 6000);
        AccountUtil.Withdraw(trustAccounts, 2000);
        AccountUtil.Withdraw(trustAccounts, 3000);
        AccountUtil.Withdraw(trustAccounts, 500);

        Console.WriteLine();
    }
}

// الكلاس الذكي الذي تم اختصاره لميثودين فقط بفضل الـ Generics والـ Polymorphism
public class AccountUtil
{
    public static void Deposit<T>(List<T> accounts, double amount) where T : Account
    {
        Console.WriteLine($"\n=== Depositing to {typeof(T).Name}s =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void Withdraw<T>(List<T> accounts, double amount) where T : Account
    {
        Console.WriteLine($"\n=== Withdrawing from {typeof(T).Name}s ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }
}

#region بنية الكلاسات الأساسية والمشتقة (تطبيق مفهوم تعدد الأشكال)

public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        Name = name;
        Balance = balance;
    }

    // تم تعريفها كـ virtual لتسمح لباقي الحسابات بالتعديل عليها
    public virtual bool Deposit(double amount)
    {
        if (amount <= 0) return false;
        Balance += amount;
        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (Balance - amount >= 0)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"[{Name}: {Balance:C}]";
    }
}

public class SavingsAccount : Account
{
    public double InterestRate { get; set; }

    public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance)
    {
        InterestRate = interestRate;
    }

    // حساب التوفير بيضيف فائدة مع الإيداع كمثال
    public override bool Deposit(double amount)
    {
        if (amount <= 0) return false;
        double interest = amount * (InterestRate / 100);
        return base.Deposit(amount + interest);
    }
}

public class CheckingAccount : Account
{
    private const double Fee = 1.50; // رسوم سحب ثابتة

    public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
        : base(name, balance)
    {
    }

    // الحساب الجاري يخصم رسوم عند كل عملية سحب
    public override bool Withdraw(double amount)
    {
        return base.Withdraw(amount + Fee);
    }
}

public class TrustAccount : SavingsAccount
{
    private int _withdrawalCount = 0;
    private const int MaxWithdrawals = 3;
    private const double MaxWithdrawPercent = 0.20; // 20% كحد أقصى للسحب

    public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance, interestRate)
    {
    }

    // حساب الأمانة يعطي بونص 50 دولار إذا كان الإيداع 5000 أو أكثر
    public override bool Deposit(double amount)
    {
        if (amount >= 5000)
        {
            return base.Deposit(amount + 50);
        }
        return base.Deposit(amount);
    }

    // شروط معقدة للسحب (لا يتعدى 3 مرات، ولا يتعدى 20% من الرصيد)
    public override bool Withdraw(double amount)
    {
        if (_withdrawalCount >= MaxWithdrawals || amount > (Balance * MaxWithdrawPercent))
        {
            return false;
        }

        if (base.Withdraw(amount))
        {
            _withdrawalCount++;
            return true;
        }
        return false;
    }
}

#endregion
}


    