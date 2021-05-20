using System;
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

                    Thread thread1 = new Thread(new ParameterizedThreadStart(fw.write));
                    thread1.Name = "1";
                    thread1.Start("first line");

                    Thread thread2 = new Thread(new ParameterizedThreadStart(fw.write));
                    thread2.Name = "2";
                    thread2.Start("second line");

                    while (thread1.ThreadState != ThreadState.Stopped || thread2.ThreadState != ThreadState.Stopped) { }
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

                    Thread thread1 = new Thread(new ParameterizedThreadStart(fw.write_mutex));
                    thread1.Name = "1";
                    thread1.Start("first line");

                    Thread thread2 = new Thread(new ParameterizedThreadStart(fw.write_mutex));
                    thread2.Name = "2";
                    thread2.Start("second line");

                    while (thread1.ThreadState != ThreadState.Stopped || thread2.ThreadState != ThreadState.Stopped) { }
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

                    Thread thread1 = new Thread(new ParameterizedThreadStart(fw.write_sema));
                    thread1.Name = "1";
                    thread1.Start("first line");

                    Thread thread2 = new Thread(new ParameterizedThreadStart(fw.write_sema));
                    thread2.Name = "2";
                    thread2.Start("second line");

                    Thread thread3 = new Thread(new ParameterizedThreadStart(fw.write_sema));
                    thread3.Name = "3";
                    thread3.Start("third line");

                    Thread thread4 = new Thread(new ParameterizedThreadStart(fw.write_sema));
                    thread4.Name = "4";
                    thread4.Start("forth line");

                    Thread thread5 = new Thread(new ParameterizedThreadStart(fw.write_sema));
                    thread5.Name = "5";
                    thread5.Start("fifth line");

                    while (thread1.ThreadState != ThreadState.Stopped || thread2.ThreadState != ThreadState.Stopped
                    || thread3.ThreadState != ThreadState.Stopped || thread4.ThreadState != ThreadState.Stopped || thread5.ThreadState != ThreadState.Stopped) { }
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

    }
}
