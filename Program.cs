using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace lab8
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Authors: Grybenko Yegor, Sukhanova Maria, Trembach Anastasia");
            Console.WriteLine("Group: IP-93");
            Console.WriteLine("Brigade: 3");
            Console.WriteLine("");

            {
                Console.WriteLine("Завдання 1");
                DateTime start = DateTime.Now;

                int sum1 = 0;
                for (int i = 1; i <= 100000000; i++) {
                    sum1 += i;
                }

                int sum2 = 0;
                for (int i = 1; i <= 100000000; i++) {
                    sum2 += i;
                }

                int sum3 = 0;
                for (int i = 1; i <= 100000000; i++) {
                    sum3 += i;
                }

                DateTime end = DateTime.Now;
                TimeSpan diff = end - start;
                Console.WriteLine("Час виконання циклiв в одному потоцi: " + (int)diff.TotalMilliseconds);
            }

            {
                DateTime start = DateTime.Now;

                Thread thread1 = new Thread(new ThreadStart(Sum));
                thread1.Start();

                Thread thread2 = new Thread(new ThreadStart(Sum));
                thread2.Start();

                int sum1 = 0;
                for (int i = 1; i <= 100000000; i++) {
                    sum1 += i;
                }

                DateTime end = DateTime.Now;
                TimeSpan diff = end - start;
                Console.WriteLine("Час виконання циклiв у трьох потоках: " + (int)diff.TotalMilliseconds);
            }


            {
                Console.WriteLine("\nЗавдання 2");

                string path = "../../../file.txt";
                using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    FileWorker fw = new FileWorker(file);

                    const int threadsNumber = 2;
                    List<Thread> threads = new List<Thread>();

                    for (int i = 1; i <= threadsNumber; i++) {
                        Thread thread = new Thread(new ParameterizedThreadStart(fw.write));
                        thread.Name = i.ToString();
                        thread.Start($"line {i}");
                        threads.Add(thread);
                    }

                    while (CheckThreads(threads)) { }
                    fw.Close();
                    file.Close();
                }

            }

            {
                Console.WriteLine("\nЗавдання 3");

                string path = "../../../file.txt";
                using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    FileWorker fw = new FileWorker(file);

                    const int threadsNumber = 2;
                    List<Thread> threads = new List<Thread>();

                    for (int i = 1; i <= threadsNumber; i++) {
                        Thread thread = new Thread(new ParameterizedThreadStart(fw.write_mutex));
                        thread.Name = i.ToString();
                        thread.Start($"line {i}");
                        threads.Add(thread);
                    }

                    while (CheckThreads(threads)) { }
                    fw.Close();
                    file.Close();
                }
            }
            
            {
                Console.WriteLine("\nЗавдання 4");
                string path = "../../../file.txt";
                using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    FileWorker fw = new FileWorker(file);

                    const int threadsNumber = 5;
                    List<Thread> threads = new List<Thread>();

                    for (int i = 1; i <= threadsNumber; i++) {
                        Thread thread = new Thread(new ParameterizedThreadStart(fw.write_sema));
                        thread.Name = i.ToString();
                        thread.Start($"line {i}");
                        threads.Add(thread);
                    }

                    while (CheckThreads(threads)) { }
                    fw.Close();
                    file.Close();
                }
            }


            Console.ReadKey();
        }

        public static void Sum() {
            int sum = 0;
            for (int i = 1; i <= 100000000; i++) {
                sum += i;
            }
        }

        public static bool CheckThreads(List<Thread> threads) {
            bool threadsRunning = false;
            threads.ForEach((thread) => { threadsRunning = thread.ThreadState == ThreadState.Stopped ? threadsRunning : true; });
            return threadsRunning;
        }

    }
}
