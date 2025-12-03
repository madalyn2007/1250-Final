
internal class Program
{
    static void Main(string[] args)
    {

        //loads the catalogue
        FileManager.LoadCatalogue();

        //displays menu
        DisplayMenu();

    }

    public static void DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine("----------------- Library Checkout System ----------------");
            Console.WriteLine("[1] Add a library item");
            Console.WriteLine("[2] Check out an item");
            Console.WriteLine("[3] Return an item");
            Console.WriteLine("[4] View my checkout receipt");
            Console.WriteLine("[5] Save my checkout list to file");
            Console.WriteLine("[6] Load my previous checkout list from file");
            Console.WriteLine("[7] Exit");

            Console.Write("Please select an option:");
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice))
            {
                Console.WriteLine("Input cannot be empty. Please enter a number between 1 and 8.");
                continue;
            }

            //if input cant be parsed to int, return to main menu
            int intChoice;
            bool isValidNumber = int.TryParse(choice, out intChoice);
            
            if (intChoice >= 1 && intChoice <= 7)
            {

                if (intChoice == 1)
                {
                    Checkout.AddItemToCatalogue();
                }
                if (intChoice == 2)
                {
                    Checkout.CheckoutItem();
                }
                if (intChoice == 3)
                {
                    Checkout.ReturnItem();
                }
                if (intChoice == 4)
                {
                    Checkout.ViewCheckoutReceipt();
                }
                if (intChoice == 5)
                {
                    FileManager.SaveCheckoutListToFile();
                    Console.WriteLine("Checkout list saved to file.");
                    return;
                }
                if (intChoice == 6)
                {
                    FileManager.LoadPreviousCheckoutListFromFile();
                    Console.WriteLine("Previous checkout list loaded from file.");
                }
                if (intChoice == 7)
                {
                    Console.WriteLine("Press any key to exit the program...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
            }
        }
    }
}