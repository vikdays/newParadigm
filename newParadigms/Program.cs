using System;

namespace newParadigms
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            var zoo = new Zoo<BaseEntity>();
            Enclosure tigerEnclosure = new Enclosure("Вольер 1", 5, 0);
            Enclosure elephantEnclosure = new Enclosure("Вольер 2", 5, 0);
            Enclosure wolfEnclosure = new Enclosure("Вольер 3", 5, 0);

            // Добавляем вольеры в зоопарк
            zoo.AddNewEntity(tigerEnclosure);
            zoo.AddNewEntity(elephantEnclosure);
            zoo.AddNewEntity(wolfEnclosure);

            // Создаем и добавляем не менее 15 рандомных животных в зоопарк
            Random random = new Random();
            int totalAnimals = 15;
            for (int i = 0; i < totalAnimals; i++)
            {
                Animal randomAnimal;
                int animalType = random.Next(3); // 0 - Tiger, 1 - Elephant, 2 - Wolf
                switch (animalType)
                {
                    case 0:
                        randomAnimal = new Tiger();
                        tigerEnclosure.AddAnimal(randomAnimal);
                        break;
                    case 1:
                        randomAnimal = new Elephant();
                        elephantEnclosure.AddAnimal(randomAnimal);
                        break;
                    case 2:
                        randomAnimal = new Wolf();
                        wolfEnclosure.AddAnimal(randomAnimal);
                        break;
                    default:
                        throw new Exception("Invalid animal type.");
                }
                zoo.AddNewEntity(randomAnimal);
            }

            Employee employee1 = new Employee("Иван", 'М', "Кормильщик");
            zoo.AddNewEntity(employee1);

            Employee employee2 = new Employee("Мария", 'Ж', "Уборщица");
            zoo.AddNewEntity(employee2);

            Employee employee3 = new Employee("Алексей", 'М', "Ветеринар");
            zoo.AddNewEntity(employee3);

            employee1.ReplenishFood(tigerEnclosure, 20);
            employee2.ReplenishFood(wolfEnclosure, 15);
            employee3.ReplenishFood(elephantEnclosure, 10);

            System.Timers.Timer timer = new System.Timers.Timer();
            var time = 0;
            timer.Interval = 1000;
            timer.Elapsed += (sender, e) =>
            {
                time++;
                zoo.ShuffleAnimals();
                zoo.UpdateAnimalStatus();
                if (time % 5 == 0)
                {
                    zoo.FeedAnimalsByVisitors();
                }

            };
            timer.AutoReset = true; // Устанавливаем автоматический перезапуск таймера
            timer.Start();
            DisplayMenu(zoo);
        }

        static void DisplayMenu(Zoo<BaseEntity> zoo)
        {
            Console.WriteLine("Добро пожаловать, директор!");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить посетителя");
            Console.WriteLine("2. Добавить сотрудника");
            Console.WriteLine("3. Добавить животное");
            Console.WriteLine("4. Удалить объект");
            Console.WriteLine("5. Проверить статус зоопарка");
            Console.WriteLine("6. Проверить статус посетителей");
            Console.WriteLine("7. Проверить статус сотрудников");
            Console.WriteLine("8. Проверить статус животных");
            Console.WriteLine("9. Приказать животному подать голос");
            Console.WriteLine("10. Редактировать посетителя");
            Console.WriteLine("11. Редактировать сотрудника");
            Console.WriteLine("12. Редактировать животного");
            Console.WriteLine("13. Удалить вольер");
            Console.WriteLine("14. Создать новый вольер");
            Console.WriteLine("15. Посмотреть статус вольеров");
            Console.WriteLine("16. Посетители просматривают открытую часть вольера");
            Console.WriteLine("0. Выйти");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddVisitor(zoo);
                    break;
                case 2:
                    AddEmployee(zoo);
                    break;
                case 3:
                    AddAnimal(zoo);
                    break;
                case 4:
                    DeleteObject(zoo);
                    break;
                case 5:
                    CheckZooStatus(zoo);
                    break;
                case 6:
                    CheckVisitorStatus(zoo);
                    break;
                case 7:
                    CheckEmployeeStatus(zoo);
                    break;
                case 8:
                    CheckAnimalStatus(zoo);
                    break;
                case 9:
                    MakeAnimalSpeak(zoo);
                    break;
                case 10:
                    EditVisitor(zoo);
                    break;
                case 11:
                    EditEmployee(zoo);
                    break;
                case 12:
                    EditAnimal(zoo);
                    break;
                case 13:
                    DeleteObject(zoo);
                    break;
                case 14:
                    AddEnclosure(zoo);
                    break;
                case 15:
                    CheckEnclosureStatus(zoo);
                    break;
                case 16:
                    CheckPartEnclosureStatus(zoo);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    DisplayMenu(zoo);
                    break;
            }
        }
        static void AddVisitor(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите имя посетителя:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите пол посетителя (М/Ж):");
            char gender = char.ToUpper(Console.ReadLine()[0]);
            Visitor visitor = new Visitor(name, gender, 100);
            zoo.AddNewEntity(visitor);
            Console.WriteLine("Посетитель добавлен!");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }
        static void AddEnclosure(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите название вольера");
            string name = Console.ReadLine();

            int capacity;
            do
            {
                Console.WriteLine("Введите вместимость вольера (целое число больше нуля)");
            } while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0);

            int foodSupply;
            do
            {
                Console.WriteLine("Введите начальный запас еды в вольере (целое число больше нуля)");
            } while (!int.TryParse(Console.ReadLine(), out foodSupply) || foodSupply <= 0);
            Enclosure enclosure = new Enclosure(name, capacity, foodSupply);
            zoo.AddNewEntity(enclosure);

            Console.WriteLine("Вольер добавлен!");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void AddEmployee(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите имя сотрудника:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите пол сотрудника (М/Ж):");
            char gender = char.ToUpper(Console.ReadLine()[0]);
            Console.WriteLine("Введите должность сотрудника:");
            string position = Console.ReadLine();

            // Запросить у пользователя вид животного, за которым будет ухаживать сотрудник
            Console.WriteLine("Введите вид животного, за которым будет ухаживать сотрудник:");
            string animalSpecies = Console.ReadLine();

            if (animalSpecies == null)
            {
                Console.WriteLine($"Животное вида {animalSpecies} не найдено. Сотрудник не может быть добавлен.");
                DisplayMenu(zoo);
                return;
            }

            Employee employee = new Employee(name, gender, position);
            zoo.AddNewEntity(employee);
            Console.WriteLine($"Сотрудник {name} добавлен! Он будет ухаживать за животным вида {animalSpecies}.");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void AddAnimal(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите вид животного:");
            string species = Console.ReadLine();
            switch (species.ToLower())
            {
                case "тигр":
                    AddTiger(zoo);
                    break;
                case "слон":
                    AddElephant(zoo);
                    break;
                case "волк":
                    AddWolf(zoo);
                    break;
                default:
                    Console.WriteLine("Вид животного не распознан.");
                    zoo.ResumeTimer();
                    DisplayMenu(zoo);
                    break;
            }
        }
        static void AddTiger(Zoo<BaseEntity> zoo)
        {
            Animal tiger = new Tiger();
            zoo.AddNewEntity(tiger);
            Console.WriteLine("Тигр добавлен!");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }
        static void AddElephant(Zoo<BaseEntity> zoo)
        {
            Animal elephant = new Elephant();
            zoo.AddNewEntity(elephant);
            Console.WriteLine("Слон добавлен!");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void AddWolf(Zoo<BaseEntity> zoo)
        {
            Animal wolf = new Wolf();
            zoo.AddNewEntity(wolf);
            Console.WriteLine("Волк добавлен!");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void DeleteObject(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите имя объекта для удаления:");
            string name = Console.ReadLine();

            var visitor = zoo.FindEntityByName<Visitor>(name);
            if (visitor != null)
            {
                zoo.RemoveEntity(visitor.Id);
                Console.WriteLine("Посетитель удален!");
                zoo.ResumeTimer();
                DisplayMenu(zoo);
                return;
            }

            var employee = zoo.FindEntityByName<Employee>(name);
            if (employee != null)
            {
                zoo.RemoveEntity(employee.Id);
                Console.WriteLine("Сотрудник удалён!");
                zoo.ResumeTimer();
                DisplayMenu(zoo);
                return;
            }

            var animal = zoo.FindEntityByName<Animal>(name);
            if (animal != null)
            {
                zoo.RemoveAnimalBySpecies(animal.Species);
                Console.WriteLine("Животное удалено!");
                zoo.ResumeTimer();
                DisplayMenu(zoo);
                return;
            }

            var enclosure = zoo.FindEntityByName<Enclosure>(name);
            if (enclosure != null)
            {
                zoo.RemoveEntity(enclosure.Id);
                Console.WriteLine("Вольер и все животные в нем удалены!");
                zoo.ResumeTimer();
                DisplayMenu(zoo);
                return;
            }

            Console.WriteLine("Объект не найден.");
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void CheckZooStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine(zoo.GetZooStatus());
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }
        static void CheckEnclosureStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            foreach (IEnclosure enclosure in zoo.GetEnclosures())
            {
                Console.WriteLine(enclosure.CheckStatus());
            }
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }
        static void CheckPartEnclosureStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            foreach (IEnclosure enclosure in zoo.GetEnclosures())
            {
                Console.WriteLine(enclosure.CheckOpenPartStatus());
            }
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void CheckVisitorStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            foreach (Visitor visitor in zoo.GetVisitors())
            {
                Console.WriteLine(visitor.CheckStatus());
            }
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void CheckEmployeeStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            foreach (Employee employee in zoo.GetEmployees())
            {
                Console.WriteLine(employee.CheckStatus());
            }
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void CheckAnimalStatus(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            foreach (IEnclosure enclosure in zoo.GetEnclosures())
            {
                Console.WriteLine($"Статус животных в {enclosure.Name}:");
                foreach (Animal animal in enclosure.Animals)
                {
                    if (animal is Tiger tiger)
                    {
                        Console.WriteLine(tiger.CheckStatus());
                    }
                    else if (animal is Elephant elephant)
                    {
                        Console.WriteLine(elephant.CheckStatus());
                    }
                    else if (animal is Wolf wolf)
                    {
                        Console.WriteLine(wolf.CheckStatus());
                    }
                }
                Console.WriteLine();
            }
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void EditVisitor(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите имя посетителя для редактирования:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите новое имя посетителя:");
            string newName = Console.ReadLine();
            Console.WriteLine("Введите новый пол посетителя (М/Ж):");
            char newGender = char.ToUpper(Console.ReadLine()[0]);
            zoo.EditVisitor(name, newName, newGender);
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void EditEmployee(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите имя сотрудника для редактирования:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите новое имя сотрудника:");
            string newName = Console.ReadLine();
            Console.WriteLine("Введите новый пол сотрудника (М/Ж):");
            char newGender = char.ToUpper(Console.ReadLine()[0]);
            Console.WriteLine("Введите новую должность сотрудника:");
            string newPosition = Console.ReadLine();
            zoo.EditEmployee(name, newName, newGender, newPosition);
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void EditAnimal(Zoo<BaseEntity> zoo)
        {
            zoo.PauseTimer();
            Console.WriteLine("Введите вид животного для редактирования:");
            string species = Console.ReadLine();
            Console.WriteLine("Введите новый вид животного:");
            string newSpecies = Console.ReadLine();
            Console.WriteLine("Введите новый уровень голода животного:");
            int newHungerLevel = int.Parse(Console.ReadLine());
            zoo.EditAnimal(species, newSpecies, newHungerLevel);
            zoo.ResumeTimer();
            DisplayMenu(zoo);
        }

        static void MakeAnimalSpeak(Zoo<BaseEntity> zoo)
        {
            Console.WriteLine("Введите вид животного:");
            string species = Console.ReadLine();
            zoo.MakeAnimalSpeak(species);
            DisplayMenu(zoo);
        }
    }
}