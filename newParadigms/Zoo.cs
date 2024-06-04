namespace newParadigms
{
    class Zoo<T> where T : BaseEntity
    {
        private readonly List<T> entityList = new List<T>();
        private bool isPaused = false;


        public void PauseTimer()
        {
            isPaused = true; 
            Console.WriteLine("Таймер поставлен на паузу");
        }

        public void ResumeTimer()
        {
            isPaused = false;
            Console.WriteLine("Таймер возобновлён");
        }

        public void AddNewEntity(T newEntity)
        {
            switch (newEntity)
            {
                case Animal animal:
                    var enclosures = entityList.OfType<IEnclosure>().ToList();
                    var added = false;
                    foreach (IEnclosure enclosure in enclosures)
                    {
                        if (enclosure.HasSpace())
                        {
                            enclosure.AddAnimal(animal);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        string newEnclosureName = $"Вольер {enclosures.Count + 1}";
                        int newInitialFoodSupply = 15;
                        int newEnclosureCapacity = 5;
                        IEnclosure newEnclosure = new Enclosure(newEnclosureName, newEnclosureCapacity, newInitialFoodSupply);
                        newEnclosure.AddAnimal(animal);
                        enclosures.Add(newEnclosure);
                        Console.WriteLine($"Животное {animal.Species} добавлено в новый {newEnclosureName}!");
                        entityList.Add((T)(BaseEntity)newEnclosure);
                    }
                    
                    break;
                case Employee employee:
                    entityList.Add((T)(BaseEntity)employee);
                    break;
                case Enclosure enclosure:
                    entityList.Add((T)(BaseEntity)enclosure);
                    break;
                case Visitor visitor:
                    entityList.Add((T)(BaseEntity)visitor);
                    break;
            }   
        }
        public void ShuffleAnimals()
        {
            var random = new Random();

            foreach (var enclosure in entityList.OfType<Enclosure>())
            {
                int randomNumber = random.Next(1, enclosure.Animals.Count + 1);

                for (int i = 0; i < randomNumber; i++)
                {
                    var randomAnimal = enclosure.Animals[random.Next(enclosure.Animals.Count)];
                    enclosure.MoveAnimal(randomAnimal);
                }
            }
        }

        public void FeedAnimalsByVisitors()
        {
            Random rand = new Random();

            var visitors = entityList.OfType<Visitor>().ToList();
            var enclosures = entityList.OfType<Enclosure>().ToList();

            foreach (var visitor in visitors)
            {
                if (visitor.BuyFood(10))
                {
                    if (enclosures.Count == 0)
                        continue;

                    var randomEnclosure = enclosures[rand.Next(enclosures.Count)];

                    foreach (var animalToFeed in randomEnclosure.OpenPartOfEnclosure)
                    {

                        visitor.FeedAnimal(randomEnclosure, animalToFeed);

                    }
                }
            }
        }

        public void RemoveEntity(Guid id)
        {
            var entityToRemove = entityList.FirstOrDefault(entity => entity.Id == id);
            if (entityToRemove != null)
            {
                if (entityToRemove is IEnclosure enclosure)
                {
                    enclosure.ClearAnimals();
                }
                entityList.Remove(entityToRemove);
                Console.WriteLine($"Сущность с ID {id} была удалена.");
            }
            else
            {
                Console.WriteLine($"Сущность с ID {id} не найдена.");
            }
        }

        public T FindEntityByName<T>(string name) where T : BaseEntity
        {
            var entities = entityList.OfType<T>().ToList();
            if (typeof(T) == typeof(Visitor))
            {
                return entities.FirstOrDefault(entity => ((Visitor)(object)entity).Name.Equals(name));
            }
            else if (typeof(T) == typeof(Employee))
            {
                return entities.FirstOrDefault(entity => ((Employee)(object)entity).Name.Equals(name));
            }
            else if (typeof(T) == typeof(Animal))
            {
                var enclosures = entityList.OfType<IEnclosure>().ToList();
                foreach (IEnclosure enclosure in enclosures)
                {
                    var animal = enclosure.Animals.FirstOrDefault(a => a.Species.Equals(name));
                    if (animal != null)
                    {
                        return (T)(object)animal;
                    }
                }
            }
            else if (typeof(T) == typeof(Enclosure))
            {
                var enclosures = entityList.OfType<IEnclosure>().ToList();
                return (T)(object)enclosures.FirstOrDefault(enclosure => enclosure.Name.Equals(name));
            }

            return null;
        }

        public void RemoveEnclosureAndAnimals(string name)
        {
            var enclosures = entityList.OfType<IEnclosure>().ToList();
            var enclosureToRemove = enclosures.FirstOrDefault(enclosure => enclosure.Name.Equals(name));
            if (enclosureToRemove != null)
            {
                enclosureToRemove.ClearAnimals();

                enclosures.Remove(enclosureToRemove);
            }
        }

        public void RemoveAnimalBySpecies(string species)
        {
            var enclosures = entityList.OfType<IEnclosure>().ToList();
            foreach (var enclosure in enclosures)
            {
                var animalToRemove = enclosure.Animals.FirstOrDefault(a => a.Species.Equals(species));
                if (animalToRemove != null)
                {
                    enclosure.RemoveAnimal(animalToRemove);
                    entityList.Remove((T)(BaseEntity)animalToRemove);
                    Console.WriteLine($"Животное {species} удалено из вольера {enclosure.Name} и из зоопарка.");
                    return;
                }
            }
            Console.WriteLine($"Животное {species} не найдено в зоопарке.");
        }

        public string GetZooStatus()
        {
            var visitorCount = entityList.OfType<Visitor>().Count();
            var employeeCount = entityList.OfType<Employee>().Count();
            var animalCount = entityList.OfType<IEnclosure>().Sum(enclosure => enclosure.AnimalCount);

            return $"В зоопарке находится {visitorCount} посетителей, {employeeCount} сотрудников и {animalCount} животных.";
        }

        public void MakeAnimalSpeak(string species)
        {
            Animal animal = FindEntityByName<Animal>(species);
            if (animal != null)
            {
                animal.MakeSound();
            }
            else
            {
                Console.WriteLine($"Животное вида {species} не найдено.");
            }
        }

        public void UpdateAnimalStatus()
        {
            var enclosures = entityList.OfType<IEnclosure>().ToList();

            foreach (var enclosure in enclosures)
            {
                enclosure.FeedAnimals();

                foreach (var animal in enclosure.Animals)
                {
                    if (!isPaused)
                    {
                        animal.DecreaseHungerLevel(); 
                    }
                }

                if (enclosure.IsFoodDepleted())
                {
                    ReplenishEnclosureFood(enclosure);
                }
            }
        }

        private Employee GetRandomEmployee()
        {
            var employees = entityList.OfType<Employee>().ToList();

            if (employees.Count == 0)
                throw new Exception("No employees available.");

            Random random = new Random();
            int index = random.Next(employees.Count);
            return employees[index];
        }

        private void ReplenishEnclosureFood(IEnclosure enclosure)
        {
            try
            {
                Employee randomEmployee = GetRandomEmployee();
                int foodAmount = 15;
                randomEmployee.ReplenishFood(enclosure, foodAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditVisitor(string name, string newName, char newGender)
        {
            Visitor visitor = FindEntityByName<Visitor>(name);
            if (visitor != null)
            {
                visitor.Edit(newName, newGender);
                Console.WriteLine("Посетитель успешно отредактирован!");
            }
            else
            {
                Console.WriteLine("Посетитель не найден.");
            }
        }

        public void EditEmployee(string name, string newName, char newGender, string newPosition)
        {
            Employee employee = FindEntityByName<Employee>(name);
            if (employee != null)
            {
                employee.Edit(newName, newGender, newPosition);
                Console.WriteLine("Сотрудник успешно отредактирован!");
            }
            else
            {
                Console.WriteLine("Сотрудник не найден.");
            }
        }

        public void EditAnimal(string species, string newSpecies, int newHungerLevel)
        {
            Animal animal = FindEntityByName<Animal>(species);
            if (animal != null)
            {
                animal.Edit(newSpecies, newHungerLevel);
                Console.WriteLine("Животное успешно отредактировано!");
            }
            else
            {
                Console.WriteLine("Животное не найдено.");
            }
        }
        public List<IEnclosure> GetEnclosures()
        {
            return entityList.OfType<IEnclosure>().ToList();
        }
        public List<Visitor> GetVisitors()
        {
            return entityList.OfType<Visitor>().ToList();
        }
        public List<Employee> GetEmployees()
        {
            return entityList.OfType<Employee>().ToList();
        }
    }
}
