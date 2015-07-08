namespace FYP_Music_Theory.Domain
{
    public sealed class Bank
    {
        public int Silver { get; private set; }

        /// <summary>
        /// Take some silver out of the bank, if there is enough available.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>The silver withdrew from the bank.</returns>
        public int Withdraw(int amount)
        {
            if (Silver < amount)
            {
                return 0;
            }

            Silver -= amount;
            return amount;
        }

        /// <summary>
        /// Put some silver into the bank.
        /// </summary>
        /// <param name="amount">The amount to put in to the bank.</param>
        public void Deposit(int amount)
        {
            Silver += amount;
        }
    }
}