using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Practice.list;

namespace Practice
{
    class Program
    {
        static void Main()
        {
            BiList eventList = new BiList();

            while (true)
            {
                Console.WriteLine("\n1. add event to start\n2. add event to end \n3. delete last event \n4. print list \n5. search \n6. Serialize\n7. DeSerialize\n8. Exit");
                Console.Write("action: ");
                string actionInput = Console.ReadLine();

                if (!int.TryParse(actionInput, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                    case 2://1||2
                        Console.WriteLine("1 - Festival\n2 - Seminar\n3 - Meeting\n4 - Overtime\n5 - Conference");
                        Console.Write("Event Type: ");
                        string inputType = Console.ReadLine();

                        EventManagement.EventType type;
                        if (int.TryParse(inputType, out int typeNumber) && typeNumber >= 1 && typeNumber <= 5)
                        {
                            type = (EventManagement.EventType)(typeNumber - 1);
                        }
                        else if (Enum.TryParse(typeof(EventManagement.EventType), inputType, true, out object? parsedType))
                        {
                            type = (EventManagement.EventType)parsedType;
                        }
                        else
                        {
                            Console.WriteLine("Invalid event type.");
                            continue;
                        }

                        Console.Write("duration (hours): ");
                        string durationInput = Console.ReadLine();
                        if (!int.TryParse(durationInput, out int duration))
                        {
                            Console.WriteLine("Invalid duration. Please enter a number.");
                            continue;
                        }

                        Console.Write("public? (1/t/true or 0/f/false): ");
                        string inputPublic = Console.ReadLine()?.ToLower();
                        bool isPublic = inputPublic == "1" || inputPublic == "t" || inputPublic == "true";

                        if (choice == 1) eventList.AddFirst(new EventManagement(type, duration, isPublic));
                        else eventList.AddLast(new EventManagement(type, duration, isPublic));
                        break;

                    case 3:
                        eventList.RemoveLast();
                        break;

                    case 4:
                        eventList.PrintList();
                        break;

                    case 5:
                        var results = eventList.SearchPrivateConferences();
                        Console.WriteLine("Private Conferences > 4 hours:");
                        foreach (var ev in results)
                        {
                            Console.WriteLine($"Type: {ev.Type}, Duration: {ev.Duration}");
                        }
                        break;

                    case 6:
                        Console.WriteLine("JSON: " + eventList.Serialize());
                        break;

                    case 7:
                        Console.Write("Type JSON: ");
                        string jsonInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(jsonInput))
                        {
                            eventList = BiList.Deserialize(jsonInput);
                        }
                        else
                        {
                            Console.WriteLine("Invalid JSON input.");
                        }
                        break;

                    case 8:
                        return;

                    default:
                        Console.WriteLine("Error: Invalid choice.");
                        break;
                }

            }

        }
    }
}