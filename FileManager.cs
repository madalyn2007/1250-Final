internal class FileManager
{
    //lists and paths for catalogue and checkout items
    public static string path = "C:\\Users\\millermf3\\OneDrive - East Tennessee State University\\Code\\repo\\bin\\Debug\\net9.0\\libraryCatalogue.csv";
    public static List<LibraryItemList> libraryItems = new List<LibraryItemList>();

    public static List<LibraryItemList> tempCheckoutList = new List<LibraryItemList>();

    public static string checkoutPath = "C:\\Users\\millermf3\\OneDrive - East Tennessee State University\\Code\\repo\\bin\\Debug\\net9.0\\savedCheckoutList.csv";
    



    public static void LoadCatalogue()
    {
        
        //read all the lines from the file
        File.ReadAllLines(path);
        //store each line in a string array
        string[] lines = File.ReadAllLines(path);
        
        //loop through each line in the file and parse the data
        foreach (var item in lines)
        {
            var split = item.Split(',');
            libraryItems.Add(new LibraryItemList(int.Parse(split[0]), split[1], split[2], decimal.Parse(split[3]), bool.Parse(split[4]), split[5]));
        }
    }


    public static void AddItem(LibraryItemList newItem)
    {
        //append the new item to the file
        File.AppendAllText(path, newItem.ToCSV());

    }

    public static void updateAvailability(int Id, bool isCheckedOut)
    {

        //update the availability of the item in the file
        foreach (var item in libraryItems)
        {
            if (item.Id == Id)
            {
                item.IsCheckedOut = isCheckedOut;
            }
        
       }
        //store all items temporarily in a string
        string allItems = "";

        foreach (var libraryItem in libraryItems)
        {
            
            allItems += libraryItem.ToCSV();
        }

       File.Delete(path);
       File.WriteAllText(path, allItems);

    }

    public static void SaveCheckoutListToFile()
    {
        //write all items in the temp checkout list to the file
        File.WriteAllLines(checkoutPath, tempCheckoutList.Select(item => item.ToCSV()));
    }


    public static void LoadPreviousCheckoutListFromFile()
    {
        File.ReadAllLines(checkoutPath);
        string[] lines = File.ReadAllLines(checkoutPath);

        foreach(var item in lines)
        {
            var split = item.Split(',');
            tempCheckoutList.Add(new LibraryItemList(int.Parse(split[0]), split[1], split[2], decimal.Parse(split[3]), bool.Parse(split[4]), split[5]));
        }   
    }
}
