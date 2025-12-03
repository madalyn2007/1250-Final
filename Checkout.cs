
internal class Checkout
{
    public static void AddItemToCatalogue()
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            //ask user for item details
            Console.WriteLine("Enter the ID of the item:");
            var id = Console.ReadLine();

            //if input cant be parsed to int, return to main menu
            int newID;
            bool isValidID = int.TryParse(id, out newID);
            if (!isValidID)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the ID:");
                return;
            }

            Console.WriteLine("-------------------------");

            Console.WriteLine("Enter the title of the item:");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Invalid input. Please enter a valid title:");
                return;
            }

            Console.WriteLine("-------------------------");

            Console.WriteLine("Enter the type of the item (e.g., Book, DVD):");
            string type = Console.ReadLine();
            if (string.IsNullOrEmpty(type))
            {
                Console.WriteLine("Invalid input. Please enter a valid type:");
                return;
            }
            Console.WriteLine("-------------------------");

            Console.WriteLine("Enter the daily late fee for the item:");
            var dailyLateFee = Console.ReadLine();

            decimal lateFee;
            bool isValidLateFee = decimal.TryParse(dailyLateFee, out lateFee);
            if (!isValidLateFee)
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the daily late fee:");
                return;
            }

            //set the item as not checked out by default
            bool isCheckedOut = false;

            //determine loan period based on item type
            string loanPeriod;
            if(type.ToLower() == "book")
            {
                loanPeriod = "21 days";
            }
            else if (type.ToLower() == "dvd")
            {
                loanPeriod = "7 days";
            }
            else
            {
                loanPeriod = "14 days"; 
            }

            //create a new LibraryItemList object for the new item
            LibraryItemList newItem = new LibraryItemList(newID, title, type, lateFee, isCheckedOut, loanPeriod);


            //add the new item to the catalogue
            FileManager.AddItem(newItem);

            Console.WriteLine("Item added successfully!");
            Console.WriteLine("-------------------------");
            //ask if the user wants to add another item
            Console.WriteLine("Do you want to add another item? (y/n):");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                AddItemToCatalogue();
            }
            else
            {
                break;
            }
        }

    }
 
    public static void CheckoutItem()
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Available Library Items:");
            
            //loop through the items and display those that are not checked out
            foreach (var item in FileManager.libraryItems)
            {                
                if (item.IsCheckedOut == false)
                {
                    Console.WriteLine($"{item.Id}: {item.Title} ({item.Type})");
                    Console.WriteLine("-------------------------");
                }
            }
            Console.WriteLine("Enter the ID of the item you want to check out:");
            var input = Console.ReadLine();

            int checkoutID;
            bool isValidID = int.TryParse(input, out checkoutID);
            if (!isValidID)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the ID:");
                return;
            }
            foreach (var item in FileManager.libraryItems)
            {
                if (item.Id == checkoutID)
                {
                    item.IsCheckedOut = true;
                    FileManager.tempCheckoutList.Add(item);
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"Item '{item.Title}' checked out successfully! Loan Period: {item.LoanPeriod}");                   
                    FileManager.updateAvailability(item.Id, item.IsCheckedOut);
                    return;
                }
            }
        }

    }

    public static void ReturnItem()
    {
        while (true)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Items Currently Checked Out:");
            //loop through the items and display those that are checked out
            
            foreach (var item in FileManager.libraryItems)
            {
                if (item.IsCheckedOut == true)
                {
                    Console.WriteLine($"{item.Id}: {item.Title} ({item.Type})");
                    Console.WriteLine("-------------------------");

                }
            }
            Console.WriteLine("Enter the ID of the item you want to return:");
            var input = Console.ReadLine();

            int checkoutID;
            bool isValidID = int.TryParse(input, out checkoutID);
            if (!isValidID)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the ID:");
                return;
            }
            foreach (var item in FileManager.libraryItems)
            {
                if (item.Id == checkoutID)
                {
                    item.IsCheckedOut = false;
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"Item '{item.Title}' returned successfully!");
                    FileManager.updateAvailability(item.Id, item.IsCheckedOut);
                    return;
                }
            }
        }

    }

    public static void ViewCheckoutReceipt()
    {
        while (true)
        {
            Console.WriteLine("-------------------------");
            decimal totalLateFee = 0;

            if (FileManager.tempCheckoutList.Count == 0)
            {
                Console.WriteLine("No items have been checked out.");
                return;
            }

            Console.WriteLine("How many days late is the item(s)?");
            var daysLate = Console.ReadLine();

            int lateDays;
            bool isValidLateDays = int.TryParse(daysLate, out lateDays);
            if (!isValidLateDays)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the days late:");
                return;
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("Check out Receipt:");
            Console.WriteLine("-------------------------");
            foreach (var item in FileManager.tempCheckoutList)
            {
                if (!lateDays.Equals(0))
                {
                    totalLateFee += item.DailyLateFee * lateDays;
                }
                Console.WriteLine($"ID:{item.Id}  Title:{item.Title}  Type:{item.Type}  Fee:${item.DailyLateFee}  Loan Period:{item.LoanPeriod}  Days Late:{lateDays}");
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine($"Total Estimated Late Fees: ${totalLateFee}");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Press any key to checkout...");
            FileManager.tempCheckoutList.Clear();
            if (File.Exists(FileManager.checkoutPath))
            {
                File.Delete(FileManager.checkoutPath);
            }
            Console.ReadKey();
            return;
        }
        }

    }
