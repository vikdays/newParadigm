namespace newParadigms
{
    class Enclosure : BaseEntity, IEnclosure
    {
        public string Name { get; }
        private int Capacity { get; }

        public List<Animal> Animals { get; } = new List<Animal>();
        private readonly Dictionary<Food, int> foodInventory = new();
        public List<Animal> OpenPartOfEnclosure { get; } = new List<Animal>();
        public List<Animal> ClosePartOfEnclosure { get; } = new List<Animal>();

        public Enclosure(string name, int capacity, int initialFoodSupply)
        {
            Name = name;
            Capacity = capacity;
            FoodSupply = initialFoodSupply;
        }

        private bool CanAddAnimal(Type animalType)
        {
            return Animals.Count == 0 || Animals[0].GetType() == animalType;
        }

        public void AddEnclosureFoodType(Food firstFoodType, Food secondFoodType)
        {
            foodInventory[firstFoodType] = 0;
            foodInventory[secondFoodType] = 0;
        }

        public void AddAnimal(Animal animal)
        {
            if (Animals.Count < Capacity)
            {
                if (CanAddAnimal(animal.GetType()))
                {
                    Animals.Add(animal);
                    Console.WriteLine($"Животное {animal.Species} добавлено в {Name}.");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 3);
                    if (randomNumber == 1)
                    {
                        OpenPartOfEnclosure.Add(animal);
                    }
                    else
                    {
                        ClosePartOfEnclosure.Add(animal);
                    }
                }
                return;
            }
            else
            {
                
                Console.WriteLine($"Вольер {Name} заполнен.");
            }
        }

        public void MoveAnimal(Animal animal)
        {
            if (OpenPartOfEnclosure.Contains(animal))
            {
                ClosePartOfEnclosure.Add(animal);
                OpenPartOfEnclosure.Remove(animal);
            }
            else
            {
                OpenPartOfEnclosure.Add(animal);
                ClosePartOfEnclosure.Remove(animal);
            }
        }

        public void RemoveAnimalByName(string species)
        {
            for (int i = 0; i < Animals.Count; i++)
            {
                if (Animals[i].Species.Equals(species, StringComparison.OrdinalIgnoreCase))
                {
                    Animals.RemoveAt(i);
                    Console.WriteLine($"Животное {species} удалено из вольера {Name}.");
                    return;
                }
            }
            Console.WriteLine($"Животное {species} не найдено в вольере {Name}.");
        }

        public void ClearAnimals()
        {
            Animals.Clear();
            OpenPartOfEnclosure.Clear();
            ClosePartOfEnclosure.Clear();
            Console.WriteLine($"В {Name} больше нет животных.");
        }

        public bool HasSpace()
        {
            return Animals.Count < Capacity;
        }

        public void RemoveAnimal(Animal animal)
        {
            if (Animals.Contains(animal))
            {
                Animals.Remove(animal);
                OpenPartOfEnclosure.Remove(animal);
                ClosePartOfEnclosure.Remove(animal);
                Console.WriteLine($"Животное {animal.Species} удалено из {Name}!");
            }
            else
            {
                Console.WriteLine($"Животное {animal.Species} не найдено в {Name}!");
            }
        }

        public int FoodSupply { get; private set; }

        public void AddFood(int amount)
        {
            FoodSupply += amount;
            Console.WriteLine($"Добавлено {amount} единиц еды в вольер {Name}. Теперь запас еды: {FoodSupply}");
        }

        public void FeedAnimals()
        {
            if (FoodSupply <= 0)
            {
                Console.WriteLine($"В {Name} закончилась еда, животные не могут быть покормлены.");
                return;
            }

            foreach (var animal in Animals)
            {
                if (FoodSupply > 0)
                {

                    if ((animal.Species == "тигр" && animal.HungerLevel <= 70) || (animal.Species == "слон" && animal.HungerLevel <= 30) || (animal.Species == "волк" && animal.HungerLevel <= 50))
                    {
                        animal.HungerLevel += (animal.FirstFoodType.Value + animal.SecondFoodType.Value);
                        FoodSupply--;
                        Console.WriteLine($"Животное {animal.Species} покушало {animal.FirstFoodType} и {animal.SecondFoodType}. Запас еды в вольере {Name} теперь: {FoodSupply}");
                    }
                }
                else
                {
                    Console.WriteLine($"В {Name} закончилась еда, животные не могут быть покормлены.");
                    break;
                }
            }
        }

        public bool IsFoodDepleted()
        {
            return FoodSupply <= 0;
        }

        public void ReplenishFood(int amount)
        {
            FoodSupply += amount;
            Console.WriteLine($"Запасы еды в {Name} пополнились на {amount} ");
        }

        public int AnimalCount => Animals.Count;

        public string CheckStatus()
        {
            var animalTypes = new Dictionary<string, int>();
            foreach (var animal in Animals)
            {
                if (animalTypes.ContainsKey(animal.Species))
                {
                    animalTypes[animal.Species]++;
                }
                else
                {
                    animalTypes[animal.Species] = 1;
                }
            }
            string animalInfo = string.Join(", ", animalTypes.Select(pair => $"{pair.Value} {pair.Key}"));
            return $"Вольер {Name}: Количество животных: {AnimalCount}, Виды животных: {animalInfo}, Запас еды: {FoodSupply}";
        }

        public string CheckOpenPartStatus()
        {
            var animalTypes = new Dictionary<string, int>();
            foreach (var animal in OpenPartOfEnclosure)
            {
                if (animalTypes.ContainsKey(animal.Species))
                {
                    animalTypes[animal.Species]++;
                }
                else
                {
                    animalTypes[animal.Species] = 1;
                }
            }
            string animalInfo = string.Join(", ", animalTypes.Select(pair => $"{pair.Value} {pair.Key}"));
            return $"Открытая часть вольера {Name}: Количество животных: {OpenPartOfEnclosure.Count}, Виды животных: {animalInfo}";
        }
    }
}
