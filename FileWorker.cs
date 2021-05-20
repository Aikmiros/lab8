using System;
using System.IO;
using System.Threading;

namespace lab8
{
    class FileWorker
    {
        StreamWriter sw;
        StreamReader sr;
        static Mutex mutex = new Mutex();
        static Semaphore sema = new Semaphore(2, 5);

        public FileWorker(FileStream file) {
            sw = new StreamWriter(file);
            sr = new StreamReader(file);
        }

        public string read() {
            string str;
            lock (sr) {
                str = sr.ReadLine();
                Console.WriteLine("Reading in thread {0}: {1}", Thread.CurrentThread.Name, str);
            }
            return str;
        }

        public void write(object argument) {
            string str = (string)argument;
            lock (sw) {
             sw.WriteLine(str);
             Console.WriteLine("Writing in thread {0}: {1}", Thread.CurrentThread.Name, str);               
            }
        }

        public void write_mutex(object argument)
        {
            string str = (string)argument;
            mutex.WaitOne();

            sw.WriteLine(str);
            Console.WriteLine("Writing in thread {0}: {1}", Thread.CurrentThread.Name, str);

            mutex.ReleaseMutex();
        }

        public void write_sema(object argument)
        {
            string str = (string)argument;
            sema.WaitOne();

            sw.WriteLine(str);
            Console.WriteLine("Writing in thread {0}: {1}", Thread.CurrentThread.Name, str);

            sema.Release();
        }

        public void Close() {
            sw.Dispose();
            sr.Dispose();
        }
    }
}