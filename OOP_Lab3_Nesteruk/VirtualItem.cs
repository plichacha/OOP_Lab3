using System;

namespace VirtualItemShop
{
    public class VirtualItem
    {
        private string name;
        private ItemRarity rarity = ItemRarity.COMMON;
        private double price;
        private int durability;

        public bool IsTradable { get; set; } = false;
        public DateTime CreatedDate { get; private set; }
        public int UsesCount { get; private set; }

        public VirtualItem()
        {
            Random rnd = new Random();
            string[] names = { "Sword", "Shield", "Bow", "Staff", "Amulet", "Ring" };

            Name = names[rnd.Next(names.Length)];
            Rarity = (ItemRarity)rnd.Next(Enum.GetValues(typeof(ItemRarity)).Length);
            Price = Math.Round(rnd.NextDouble() * 1000, 2);
            Durability = rnd.Next(0, 101);
            IsTradable = rnd.Next(2) == 1;
            CreatedDate = DateTime.Now;
        }

        public VirtualItem(string name, double price) : this()
        {
            Name = name;
            Price = price;
        }

        public VirtualItem(string name, ItemRarity rarity, double price, int durability) : this(name, price)
        {
            Rarity = rarity;
            Durability = durability;
        }

        public VirtualItem(string name, ItemRarity rarity, double price, int durability, bool isTradable) : this(name, rarity, price, durability)
        {
            IsTradable = isTradable;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Error: item name can't be null or empty!");
                if (value.Length < 3 || value.Length > 20)
                    throw new ArgumentException("Error: item name length must be between 3 and 20 characters!");
                foreach (char c in value)
                    if (!char.IsLetter(c))
                        throw new ArgumentException("Error: item name can only contain letters!");
                name = value;
            }
        }

        public ItemRarity Rarity
        {
            get => rarity;
            set
            {
                if (!Enum.IsDefined(typeof(ItemRarity), value))
                    throw new ArgumentException("Error: item rarity value isn't correct!");
                rarity = value;
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value < 0 || value > int.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Error: price must be in range [0..{int.MaxValue}]!");
                price = value;
            }
        }

        public int Durability
        {
            get => durability;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Error: durability must be in range [0..100]!");
                durability = value;
            }
        }

        public string Summary => $"{Name} ({Rarity}) - {Price:F2} gold, durability {Durability}%, tradable: {(IsTradable ? "yes" : "no")}";

        public double SellPrice => ComputeSellPrice();

        public bool Use()
        {
            if (!CanUse()) return false;
            ApplyUsage();
            return true;
        }

        public int Use(int times)
        {
            int used = 0;
            for (int i = 0; i < times; i++)
            {
                if (!Use()) break;
                used++;
            }
            return used;
        }

        public bool Repair()
        {
            return Repair(100 - durability);
        }

        public bool Repair(int amount)
        {
            if (!CanRepair(amount)) return false;
            ApplyRepair(amount);
            return true;
        }

        public bool Enchant()
        {
            return Enchant(1);
        }

        public bool Enchant(int levels)
        {
            if (levels <= 0 || IsMaxRarity()) return false;
            for (int i = 0; i < levels && !IsMaxRarity(); i++)
                rarity++;
            return true;
        }

        private bool CanUse()
        {
            return durability >= 10;
        }

        private void ApplyUsage()
        {
            durability -= 10;
            UsesCount++;
        }

        private bool CanRepair(int amount)
        {
            return durability < 100 && amount > 0;
        }

        private void ApplyRepair(int amount)
        {
            durability += amount;
            if (durability > 100) durability = 100;
        }

        private bool IsMaxRarity()
        {
            return rarity == ItemRarity.MYTHIC;
        }

        private double ComputeSellPrice()
        {
            double rarityMultiplier = 1 + (int)rarity * 0.5;
            double durabilityFactor = durability / 100.0;
            return Math.Round(price * rarityMultiplier * durabilityFactor, 2);
        }
    }
}