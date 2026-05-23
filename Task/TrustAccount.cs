namespace Task
{
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
            
            