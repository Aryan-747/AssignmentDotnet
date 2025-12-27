using System;

class PatientBill
{
    // Variables to Store User Inputs
    #region
    public string BillId;
    public string PatientName;
    public bool HasInsurance;
    public decimal ConsultationFee;
    public decimal LabCharges;
    public decimal MedicineCharges;
    #endregion

    public decimal GrossAmount; // To be Calculated By Program
    public decimal DiscountAmount; // To be Calculated By Program
    public decimal FinalPayable; // To be Calculated By Program


    // default constructor
    public PatientBill()
    { 
    }

    // Parametrized Constructor
    #region
    public PatientBill(string BillId, string PatientName, bool HasInsurance, decimal ConsultationFee, decimal LabCharges, decimal MedicineCharges)
    {
        this.BillId = BillId;
        this.PatientName = PatientName;
        this.HasInsurance = HasInsurance;
        this.ConsultationFee = ConsultationFee;
        this.LabCharges = LabCharges;
        this.MedicineCharges = MedicineCharges;
    }
    #endregion

    // Calcuating the Values Method
    public void Calculate()
    {
        this.GrossAmount = this.ConsultationFee + this.LabCharges + this.MedicineCharges;
        this.DiscountAmount = 0;

        if (HasInsurance)
        {
            this.DiscountAmount = this.GrossAmount * (decimal)0.10; // converting double to decimal for * operation
        }

        this.FinalPayable = this.GrossAmount - this.DiscountAmount;
    }

}

class MediSure
{
    // Register Method
    public static PatientBill Register(PatientBill p1)
    {
        // New User has to be created so we take required inputs and create an instance of PatientBill
        #region

        // taking all string inputs and then passing them onto the constructor after relevant conversion.
        string Billid;
        string PatientName;
        string isPatientInsured;
        bool Insured = false;
        string ConsultationFee;
        string LabCharges;
        string MedicineCharges;

        Console.Write("Enter Bill Id: ");
        Billid = Console.ReadLine();

        // Invalid Input, hence we ask again
        if (string.IsNullOrEmpty(Billid))
        {
            Console.WriteLine("Bill Id cannot be empty, enter again!");
            return p1;
            // Program moves to next iteration for fresh start
        }

        // taking name input
        Console.Write("Enter Patient Name: ");
        PatientName = Console.ReadLine();

        // taking insurance status input
        Console.Write("Does Patient Have Insurance?(Y/N): ");
        isPatientInsured = Console.ReadLine();

        if (isPatientInsured == "Y" || isPatientInsured == "y")
        {
            Insured = true;
        }

        else if (isPatientInsured == "N" || isPatientInsured == "n")
        {
            Insured = false;
        }

        else
        {
            Console.WriteLine("Invalid Choice! Enter details again!");
            return p1;
            // Program moves to next iteration for fresh start
        }

        // Taking consultation fee input
        Console.Write("Enter Consultation Fee: ");
        ConsultationFee = Console.ReadLine();

        // Taking Lab Charges Amount Input
        Console.Write("Enter Lab Charges: ");
        LabCharges = Console.ReadLine();

        // Taking Medicine Charges Amount Input
        Console.Write("Enter Medicine Charges: ");
        MedicineCharges = Console.ReadLine();

        // Converting the inputs into decimal and passing onto the constructor
        if ((decimal.TryParse(ConsultationFee, out decimal constfee) && constfee > 0) && (decimal.TryParse(LabCharges, out decimal labcharge) && labcharge >= 0)
            && (decimal.TryParse(MedicineCharges, out decimal medicinecharge) && medicinecharge >= 0))
        {

            // passing inputs into new object
            p1 = new PatientBill(Billid, PatientName,Insured,constfee,labcharge,medicinecharge);

            // User Registered sucessfully display function

            Console.WriteLine("Bill Created Sucessfully.");

            // Calculating Gross Amount,Final Payable
            p1.Calculate();
            Console.WriteLine("Gross Amount: " + p1.GrossAmount);
            Console.WriteLine("Discount Amount: " + p1.DiscountAmount);
            Console.WriteLine("Final Payable: " + p1.FinalPayable);
            return p1;
        }

        else
        {
            Console.WriteLine("Charges Cannot be Zero!");
            return p1;
        }
        #endregion
    }

    public static void DisplayLastBill(PatientBill p1)
    {
        // Diplaying Data
        #region
        Console.WriteLine("\n---- Last Bill ----\n");
        Console.WriteLine("Bill Id: " + p1.BillId);
        Console.WriteLine("Patient: " + p1.PatientName);
        Console.WriteLine("Insured: " + p1.HasInsurance);
        Console.WriteLine("Consultation Fee: " + p1.ConsultationFee);
        Console.WriteLine("Lab Charges: " + p1.LabCharges);
        Console.WriteLine("Medicine Charges: " + p1.MedicineCharges);
        Console.WriteLine("Gross Amount: " + p1.GrossAmount);
        Console.WriteLine("Discount Amount: " + p1.DiscountAmount);
        Console.WriteLine("Final Payable: " + p1.FinalPayable);
        Console.Write('\n');
        #endregion
    }

    public static PatientBill ClearLastBill(PatientBill p1)
    {
        // Clearing Last Bill
        #region
        p1 = new PatientBill();
        Console.WriteLine("\nLast Bill Cleared!\n");
        #endregion

        return p1;
    }

    // Main Class
    public static void Main(string[] args)
    {
        // Displaying GUI Menu For User Inputs
        string choice;
        PatientBill p1 = new PatientBill();

        while(true)
        {
            Console.WriteLine("\n------------MediSure Clinic Billing------------\n");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter Your Option: ");
            choice = Console.ReadLine();

            // if - else if conditions to check
            if(choice == "1")
            {
                p1 = Register(p1);
            }

            else if (choice == "2")
            {
                DisplayLastBill(p1);
            }

            else if (choice == "3")
            {
                p1 = ClearLastBill(p1);
            }

            // terminate the loop
            else if (choice == "4")
            {
                Console.WriteLine("\nApplication Closed Successfully! Thank You!!\n");
                break;
            }

            // Invalid Choice Entered
            else
            {
                Console.WriteLine("Invalid Choice! Enter again..");
            }

        }
    }
}