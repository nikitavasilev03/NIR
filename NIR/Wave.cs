using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Audio
{
    public static class Wave
    {
        private const int HEADER_LENGTH = 44;

        public static double[] Read(string path) //Чтение файла WAV и возвращение амплитудных занчений от -1 до 1 
        {
            byte[] allBytes = File.ReadAllBytes(path); //Считываем все байты файла
            byte[] buffer = new byte[allBytes.Length - HEADER_LENGTH]; //Байтовое представление .wav файла 
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = allBytes[i + HEADER_LENGTH]; //Считваем все байты файла
            return BytesToAmplitudes(buffer); //Получаем и возвращаем значения амплитуд
        }

        public static double[] Read(string path, out byte[] header) //Чтение файла WAV и возвращение амплитудных занчений от -1 до 1 
        {
            byte[] allBytes = File.ReadAllBytes(path); //Считываем все байты файла
            header = new byte[HEADER_LENGTH]; //Заголовок .wav файла
            byte[] buffer = new byte[allBytes.Length - HEADER_LENGTH]; //Байтовое представление .wav файла
            for (int i = 0; i < HEADER_LENGTH; i++)
                header[i] = allBytes[i]; //Считываем все байты заголовка
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = allBytes[i + HEADER_LENGTH]; //Считваем все байты файла
            return BytesToAmplitudes(buffer); //Получаем и возвращаем значения амплитуд
        }

        public static void Record(string path, byte[] header, double[] data)
        {
            Record(path, header, AmplitudesToBytes(data));
        }

        public static void Record(string path, byte[] header, byte[] buffer)
        {
            byte[] allBytes = new byte[HEADER_LENGTH + buffer.Length];
            for (int i = 0; i < HEADER_LENGTH; i++)
                allBytes[i] = header[i];
            for (int i = 0; i < buffer.Length; i++)
                allBytes[i + HEADER_LENGTH] = buffer[i];
            File.WriteAllBytes(path, allBytes);
        }

        public static double[] BytesToAmplitudes(byte[] buffer) //Получение амплитудных значений в промежутке от -1 до 1
        {
            short MSB, LSB; // старший и младший байты
            double[] amplitudes = new double[buffer.Length / 2];
            //Так как исходный WAV фал имеет 16-битное кодирование, то каждое амплитудное занчение кодируеться 16 битами
            //Следовательно что бы считать одно амплитудное значение в массив типа double необходимо:
            //1. Считать два последоватьноидущих байта из массива buffer
            //2. Объединить их в одно значение целочисленого типа short(16 бит), в ином случае, будет потерян знак у числа 
            for (int i = 0; i < amplitudes.Length; i++)
            {
                // первым байтом будет MSB
                MSB = buffer[2 * i];
                // вторым байтом будет LSB
                LSB = buffer[2 * i + 1];
                // склеиваем два байта, чтобы получить 16-битное вещественное число
                // все значения делятся на максимально возможное - 2^15
                amplitudes[i] = ((short)(MSB << 8) | LSB) / 32768.0; //Преобразуем все занчения к промежутку от -1 до 1
            }
            return amplitudes;
        }

        public static byte[] AmplitudesToBytes(double[] amplitudes) //Получение амплитудных значений в промежутке от -1 до 1
        {
            byte[] buffer = new byte[amplitudes.Length * 2];
            for (int i = 0; i < amplitudes.Length; i++)
            {
                short value = (short)(amplitudes[i] * 32768.0);
                buffer[2 * i] = (byte)(value >> 8);
                buffer[2 * i + 1] = (byte)(value << 8 >> 8);
            }
            return buffer;
        }
    }
}
