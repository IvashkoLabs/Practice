using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Practice
{
    class list
    {
        public class EventManagement
        {
            public enum EventType { Festival, Seminar, Meeting, Overtime, Conference }

            public EventType Type { get; set; }
            public int Duration { get; set; }
            public bool IsPublic { get; set; }

            public EventManagement(EventType type, int duration, bool isPublic)
            {
                Type = type;
                Duration = duration;
                IsPublic = isPublic;
            }
        }
        public class Node
        {
            public EventManagement Value { get; set; }
            public Node? Previous { get; set; }//0
            public Node? Next { get; set; }

            public Node(EventManagement value)
            {
                Value = value;
            }
        }
        public class BiList
        {
            private Node? head;
            private Node? tail;
            private int count = 0;
            public int Count
            {
                get { return count; }
            }
            public void AddFirst(EventManagement value)
            {
                Node newNode = new Node(value);
                if (head == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    newNode.Next = head;
                    head.Previous = newNode;
                    head = newNode;
                }
                count++;
            }
            public void AddLast(EventManagement value)
            {
                Node newNode = new Node(value);
                if (tail == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    newNode.Previous = tail;
                    tail.Next = newNode;
                    tail = newNode;
                }
                count++;
            }
            public void RemoveLast()
            {
                if (tail == null)
                {
                    return;
                }
                tail = tail.Previous;
                if (tail != null)
                {
                    tail.Next = null;
                }
                else
                {
                    head = null;
                }
                count--;
            }
            public EventManagement this[int index]
            {
                get
                {
                    if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                    Node? current = head;
                    for (int i = 0; i < index; i++) current = current.Next;
                    return current!.Value;
                }
                set
                {
                    if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                    Node? current = head;
                    for (int i = 0; i < index; i++) current = current.Next;
                    current!.Value = value;
                }
            }
            public double CalculateAverageDuration()
            {
                if (count == 0) return 0;
                int totalDuration = 0;
                Node? current = head;
                while (current != null)
                {
                    totalDuration += current.Value.Duration;
                    current = current.Next;
                }
                return (double)totalDuration / count;
            }
            public List<EventManagement> SearchPrivateConferences()
            {
                List<EventManagement> result = new List<EventManagement>();
                Node? current = head;
                while (current != null)
                {
                    if (!current.Value.IsPublic && current.Value.Type == EventManagement.EventType.Conference && current.Value.Duration > 4)
                    {
                        result.Add(current.Value);
                    }
                    current = current.Next; 
                }
                return result;
            }
            public void PrintList()
            {
                Console.WriteLine("\n Event type \t duration \t public ");//tab
                Node? current = head;
                while (current != null)
                {
                    Console.WriteLine($"{current.Value.Type}\t{current.Value.Duration}\t{current.Value.IsPublic}");
                    current = current.Next;
                }
            }
            public string Serialize()
            {
                List<EventManagement> events = new List<EventManagement>();
                Node? current = head;
                while (current != null)
                {
                    events.Add(current.Value);
                    current = current.Next;
                }
                return JsonSerializer.Serialize(events);
            }
            public static BiList Deserialize(string json)
            {
                BiList eventList = new BiList();
                try
                {
                    List<EventManagement>? events = JsonSerializer.Deserialize<List<EventManagement>>(json);
                    if (events != null)
                    {
                        foreach (EventManagement eventNode in events)
                        {
                            eventList.AddFirst(eventNode);
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("JSON Deserialize error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("unknown error: " + ex.Message);
                }

                return eventList;
            }
        }
    }
}