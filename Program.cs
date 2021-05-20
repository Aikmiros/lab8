using System;
using System.Threading;

namespace lab8 {
    class Program {
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
                FileWorker fw = new FileWorker("../../../file.txt");

                Thread thread1 = new Thread(new ParameterizedThreadStart(fw.write));
                thread1.Name = "First";
                thread1.Start("first line");

                Thread thread2 = new Thread(new ParameterizedThreadStart(fw.write));
                thread2.Name = "Second";
                thread2.Start("second line");
            }




            Console.ReadKey();
        }

        public static void Sum() {
            int sum = 0;
            for(int i = 1; i <= 100000000; i++) {
                sum += i;
            }
        }

    }
}
