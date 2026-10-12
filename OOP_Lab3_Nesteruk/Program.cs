using System;
using VirtualItemShop;

VirtualItem[] items = null;
int count = 0;
int N = 0;
Random rnd = new Random();

Console.WriteLine("=== Virtual Item Shop ===");

N = ReadN();
items = new VirtualItem[N];

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("1 - Add item");
    Console.WriteLine("2 - View all items");
    Console.WriteLine("3 - Find item");
    Console.WriteLine("4 - Demonstrate behavior");
    Console.WriteLine("5 - Delete item");
    Console.WriteLine("0 - Exit");
    Console.Write("Your choice: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddItem(); break;
        case "2": ViewAll(); break;
        case "3": FindItem(); break;
        case "4": DemonstrateBehavior(); break;
        case "5": DeleteItem(); break;
        case "0":
            Console.WriteLine("Program finished.");
            running = false;
            break;
        default:
            Console.WriteLine("Invalid menu option.");
            break;
    }
}

int ReadN()
{
    while (true)
    {
        Console.Write("Enter maximum number of items N (N > 0): ");
        if (int.TryParse(Console.ReadLine(), out int n) && n > 0) return n;
        Console.WriteLine("Error: N must be a positive integer!");
    }
}

void AddItem()
{
    if (count >= N)
    {
        Console.WriteLine($"Maximum number of items reached ({N}).");
        return;
    }

    VirtualItem draft = new VirtualItem();
    VirtualItem item;

    switch (rnd.Next(4))
    {
        case 0:
            Console.WriteLine("Randomly selected constructor: VirtualItem() + object initializer.");
            Console.WriteLine("You enter: name. All other fields get random values.");
            ReadName(draft);
            item = new VirtualItem { Name = draft.Name };
            Console.WriteLine("Constructor used: VirtualItem() + object initializer.");
            break;
        case 1:
            Console.WriteLine("Randomly selected constructor: VirtualItem(string name, double price).");
            Console.WriteLine("You enter: name, price. All other fields get random values.");
            ReadName(draft);
            ReadPrice(draft);
            item = new VirtualItem(draft.Name, draft.Price);
            Console.WriteLine("Constructor used: VirtualItem(string name, double price).");
            break;
        case 2:
            Console.WriteLine("Randomly selected constructor: VirtualItem(string name, ItemRarity rarity, double price, int durability).");
            Console.WriteLine("You enter: name, rarity, price, durability. Tradable gets a random value.");
            ReadName(draft);
            ReadRarity(draft);
            ReadPrice(draft);
            ReadDurability(draft);
            item = new VirtualItem(draft.Name, draft.Rarity, draft.Price, draft.Durability);
            Console.WriteLine("Constructor used: VirtualItem(string name, ItemRarity rarity, double price, int durability).");
            break;
        default:
            Console.WriteLine("Randomly selected constructor: VirtualItem(string name, ItemRarity rarity, double price, int durability, bool isTradable).");
            Console.WriteLine("You enter: all fields.");
            ReadName(draft);
            ReadRarity(draft);
            ReadPrice(draft);
            ReadDurability(draft);
            draft.IsTradable = ReadValidBool("Is the item tradable? (yes/no): ");
            item = new VirtualItem(draft.Name, draft.Rarity, draft.Price, draft.Durability, draft.IsTradable);
            Console.WriteLine("Constructor used: VirtualItem(string name, ItemRarity rarity, double price, int durability, bool isTradable).");
            break;
    }

    Console.WriteLine("Item added successfully:");

    items[count] = item;
    count++;
    PrintItem(item);
}

void ReadName(VirtualItem target)
{
    bool hasError;
    do
    {
        Console.Write("Item name (3-20 letters only): ");
        string name = Console.ReadLine();
        try
        {
            target.Name = name;
            hasError = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            hasError = true;
        }
    } while (hasError);
}

void ReadRarity(VirtualItem target)
{
    bool hasError;
    do
    {
        Console.Write("Rarity (0-Common,1-Uncommon,2-Rare,3-Epic,4-Legendary,5-Mythic): ");
        try
        {
            int val = int.Parse(Console.ReadLine());
            target.Rarity = (ItemRarity)val;
            hasError = false;
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: rarity must be a whole number!");
            hasError = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            hasError = true;
        }
    } while (hasError);
}

void ReadPrice(VirtualItem target)
{
    bool hasError;
    do
    {
        Console.Write($"Item price (0..{int.MaxValue}): ");
        try
        {
            double price = double.Parse(Console.ReadLine());
            target.Price = price;
            hasError = false;
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: price must be a number!");
            hasError = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            hasError = true;
        }
    } while (hasError);
}

void ReadDurability(VirtualItem target)
{
    bool hasError;
    do
    {
        Console.Write("Item durability (0..100): ");
        try
        {
            int durability = int.Parse(Console.ReadLine());
            target.Durability = durability;
            hasError = false;
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: durability must be a whole number!");
            hasError = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            hasError = true;
        }
    } while (hasError);
}

bool ReadValidBool(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine()?.Trim().ToLower();
        if (input == "yes") return true;
        if (input == "no") return false;
        Console.WriteLine("Error: enter 'yes' or 'no'!");
    }
}

void ViewAll()
{
    if (count == 0) { Console.WriteLine("No items found."); return; }

    Console.WriteLine("#   Name           Rarity      Price       Durability  Tradable  Created");
    for (int i = 0; i < count; i++)
        PrintRow(i + 1, items[i]);
}

void PrintRow(int index, VirtualItem item)
{
    Console.WriteLine($"{index,-4}{item.Name,-15}{item.Rarity,-12}{item.Price,-12}{item.Durability,-12}{(item.IsTradable ? "yes" : "no"),-10}{item.CreatedDate:dd.MM.yyyy HH:mm:ss}");
}

void PrintItem(VirtualItem item)
{
    Console.WriteLine(item.Summary);
}

int ReadValidIndex(string prompt)
{
    Console.Write(prompt);
    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= count) return index;
    Console.WriteLine("Error: invalid number!");
    return -1;
}

void FindItem()
{
    if (count == 0) { Console.WriteLine("No items found."); return; }

    ViewAll();
    int index = ReadValidIndex("Enter item number: ");
    if (index == -1) return;

    Console.WriteLine("#   Name           Rarity      Price       Durability  Tradable  Created");
    PrintRow(index, items[index - 1]);
}

void DemonstrateBehavior()
{
    if (count == 0) { Console.WriteLine("No items found."); return; }

    ViewAll();
    int index = ReadValidIndex("Enter item number: ");
    if (index == -1) return;
    VirtualItem item = items[index - 1];

    bool inSubMenu = true;
    while (inSubMenu)
    {
        Console.WriteLine("1 - Use()");
        Console.WriteLine("2 - Use(int times)");
        Console.WriteLine("3 - Repair()");
        Console.WriteLine("4 - Repair(int amount)");
        Console.WriteLine("5 - Enchant()");
        Console.WriteLine("6 - Enchant(int levels)");
        Console.WriteLine("7 - Show UsesCount");
        Console.WriteLine("8 - Show SellPrice");
        Console.WriteLine("9 - Show Summary");
        Console.WriteLine("0 - Back");
        Console.Write("Your choice: ");
        string sub = Console.ReadLine();
        switch (sub)
        {
            case "1":
                if (item.Use()) Console.WriteLine($"Use() called. Durability: {item.Durability}.");
                else Console.WriteLine("Error: durability too low to use item (must be at least 10)!");
                break;
            case "2":
                Console.Write("Number of uses: ");
                if (!int.TryParse(Console.ReadLine(), out int times) || times <= 0)
                {
                    Console.WriteLine("Error: number of uses must be a positive whole number!");
                    break;
                }
                int used = item.Use(times);
                Console.WriteLine($"Use(int times) called. Successful uses: {used} of {times}. Durability: {item.Durability}.");
                break;
            case "3":
                if (item.Repair()) Console.WriteLine($"Repair() called. Item fully repaired. Durability: {item.Durability}.");
                else Console.WriteLine("Error: item already at full durability!");
                break;
            case "4":
                Console.Write("Repair amount: ");
                int.TryParse(Console.ReadLine(), out int amount);
                if (item.Repair(amount)) Console.WriteLine($"Repair(int amount) called. Durability: {item.Durability}.");
                else Console.WriteLine("Error: invalid repair amount, or item already at full durability!");
                break;
            case "5":
                if (item.Enchant()) Console.WriteLine($"Enchant() called. New rarity: {item.Rarity}.");
                else Console.WriteLine("Error: item already has maximum rarity (Mythic)!");
                break;
            case "6":
                Console.Write("Number of levels: ");
                if (!int.TryParse(Console.ReadLine(), out int levels) || levels <= 0)
                {
                    Console.WriteLine("Error: number of levels must be a positive whole number!");
                    break;
                }
                if (item.Enchant(levels)) Console.WriteLine($"Enchant(int levels) called. New rarity: {item.Rarity}.");
                else Console.WriteLine("Error: item already has maximum rarity (Mythic)!");
                break;
            case "7":
                Console.WriteLine($"Use count: {item.UsesCount}.");
                break;
            case "8":
                Console.WriteLine($"Sell price: {item.SellPrice}.");
                break;
            case "9":
                Console.WriteLine(item.Summary);
                break;
            case "0":
                inSubMenu = false;
                break;
            default:
                Console.WriteLine("Invalid menu option.");
                break;
        }
    }
}

void DeleteItem()
{
    if (count == 0) { Console.WriteLine("No items found."); return; }

    ViewAll();
    int index = ReadValidIndex("Enter item number to delete: ");
    if (index == -1) return;

    for (int i = index - 1; i < count - 1; i++) items[i] = items[i + 1];
    items[count - 1] = null;
    count--;
    Console.WriteLine("Item deleted successfully.");
}