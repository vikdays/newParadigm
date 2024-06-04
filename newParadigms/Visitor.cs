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
                Random random = new Random();
                if (enclosure.OpenPartOfEnclosure.Contains(animal))
                {
                    if ((animal.Species == "тигр" && animal.HungerLevel <= 70) || (animal.Species == "слон" && animal.HungerLevel <= 30) || (animal.Species == "волк" && animal.HungerLevel <= 50))
                    {
                        int randomNumber = random.Next(1, 3);
                        if (randomNumber == 1)
                        {
                            animal.HungerLevel += animal.FirstFoodType.Value;
                            Console.WriteLine($"Посетитель {Name} покормил {animal.Species} едой {animal.FirstFoodType.Type} в {enclosure.Name}.");
                        }
                        else
                        {
                            animal.HungerLevel += animal.SecondFoodType.Value;
                            Console.WriteLine($"Посетитель {Name} покормил {animal.Species} едой {animal.SecondFoodType.Type} в {enclosure.Name}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Животное {animal.Species} не голодно и отказывается от еды.");
                }
            }
           
        }
    }
}
