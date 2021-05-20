using System;
using System.IO;
using System.Threading;

namespace lab8 {
    class FileWorker {
        object block = new object();
        string path;

        public FileWorker(string path) {
            this.path = path;
        }

        public string read() {
            using StreamReader sr = File.OpenText(path); 
            string str = sr.ReadLine();
            Console.WriteLine(str);
            return str;
        }

        public void write(object argument) {
            string str = (string)argument;
            lock (block) {
                using StreamWriter sw = File.CreateText(path); 
                sw.WriteLine(str);
                Console.WriteLine("Thread name = {0}, value to write = {1}", Thread.CurrentThread.Name, str);
            }
            
        }

    }
}
