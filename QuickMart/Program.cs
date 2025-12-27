using System;


    // Base Class
    public class SaleTransaction
    {
        // Variables to Store All User Inputs
        public string InvoiceNo;
        public string CustomerName;
        public string ItemName;
        public int Quantity;
        public decimal PurchaseAmount;
        public decimal SellingAmount;
        public string ProfitOrLossStatus;
        public decimal ProfitOrLossAmount; // To Be Calculated By Program
        public decimal ProfitMarginPercent; // To Be Calculated By Program

        // Business Logic Calculation
        public void CalculateProfitOrLoss()
        {
            #region
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            if (PurchaseAmount > 0)
            {
                ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
            }
            else
            {
                ProfitMarginPercent = 0;
            }


            #endregion
        }
    }

    public class TransactionManager
    {
        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        // Create Transaction Method
        public static void CreateTransaction()
        {
            #region
            SaleTransaction tx = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            tx.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tx.InvoiceNo))
            {
                Console.WriteLine("Invoice number cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            tx.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            tx.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out tx.Quantity) || tx.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out tx.PurchaseAmount) || tx.PurchaseAmount <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than zero.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out tx.SellingAmount) || tx.SellingAmount < 0)
            {
                Console.WriteLine("Selling amount cannot be negative.");
                return;
            }

            tx.CalculateProfitOrLoss();

            LastTransaction = tx;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculation(tx);

            #endregion
        }

        // View Transaction Method
        public static void ViewTransaction()
        {
            #region
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction tx = LastTransaction;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {tx.InvoiceNo}");
            Console.WriteLine($"Customer: {tx.CustomerName}");
            Console.WriteLine($"Item: {tx.ItemName}");
            Console.WriteLine($"Quantity: {tx.Quantity}");
            Console.WriteLine($"Purchase Amount: {tx.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {tx.SellingAmount:F2}");
            Console.WriteLine($"Status: {tx.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {tx.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {tx.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");

            #endregion
        }

        // Method to ReCalculate
        public static void Recalculate()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            LastTransaction.CalculateProfitOrLoss();
            PrintCalculation(LastTransaction);
        }

        private static void PrintCalculation(SaleTransaction tx)
        {
            // Using $ this time for better formatting
            Console.WriteLine($"Status: {tx.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {tx.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {tx.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------");
        }
    }

    // Main Program Class
    class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TransactionManager.CreateTransaction();
                        break;

                    case "2":
                        TransactionManager.ViewTransaction();
                        break;

                    case "3":
                        TransactionManager.Recalculate();
                        break;

                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select a valid menu option.");
                        break;
                }
            }
        }
    }

