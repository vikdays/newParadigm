namespace newParadigms
{
    public class Visitor : Person
    {
        private int Wallet { get; set; } // Кошелек посетителя

        public Visitor(string name, char gender, int initialMoney)
        {
            Name = name;
            Gender = gender;
            Wallet = initialMoney;
        }
        public void Edit(string newName, char newGender)
        {
            Name = newName;
            Gender = newGender;
        }
        public string CheckStatus()
        {
            return $"Посетитель {Name}, пол: {Gender}";
        }
        public bool BuyFood(int cost)
        {
            if (Wallet >= cost)
            {
                Wallet -= cost;
                Console.WriteLine($"Посетитель {Name} купил еду за {cost} денег. Осталось {Wallet} денег.");
                return true;
            }
            else
            {
                Console.WriteLine($"У посетителя {Name} недостаточно денег для покупки еды.");
                return false;
            }
        }

        public void FeedAnimal(IEnclosure enclosure, Animal animal)
        {
            if (enclosure.OpenPartOfEnclosure.Contains(animal))
            {
                if ((animal.Species == "тигр" && animal.HungerLevel <= 70) || (animal.Species == "слон" && animal.HungerLevel <= 30) || (animal.Species == "волк" && animal.HungerLevel <= 50))
                {
                    animal.DecreaseHungerLevel();
                    animal.HungerLevel = 100;
                    Console.WriteLine($"Посетитель {Name} покормил {animal.Species} едой {animal.FirstFoodType} в {enclosure.Name}.");
                }
                else
                {
                    Console.WriteLine($"Животное {animal.Species} не голодно и отказывается от еды.");
                }
            }
            else
            {
                Console.WriteLine($"Животное {animal.Species} не находится в открытой части {enclosure.Name}.");
            }
        }
    }
}
