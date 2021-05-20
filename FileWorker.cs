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
                Console.WriteLine(str);
            }
            return str;
        }

        public void write(object argument) {
            string str = (string)argument;
            sema.WaitOne();
            //mutex.WaitOne();
            //lock (sw) {
                for (int i = 0; i < 2; i++)
                {
                    sw.WriteLine(str);
                    Console.WriteLine("Thread name = {0}, value to write = {1}", Thread.CurrentThread.Name, str);
                }
            //}
            //mutex.ReleaseMutex();
            sema.Release();

        }

        public void Close() {
            sw.Dispose();
            sr.Dispose();
        }
    }
}