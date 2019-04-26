using System;

using NAudio.Wave; //Библиотека для чтения WAV файлов

namespace Audio
{
    public static class WaveReader
    {
        public static double[] GetWAVasDoubleArray(string path) //Чтение файла WAV и возвращение амплитудных занчений от -1 до 1 
        {
            byte[] buffer;
            using (WaveFileReader reader = new WaveFileReader("Test1.wav")) //Создаем объект для чтения WAV файла и прередаем ему путь у WAV файлу 
            {
                buffer = new byte[reader.Length]; //Создаем массив байтов куда поместим последовательность байтов файла  
                reader.Read(buffer, 0, buffer.Length); //Считываем файл и помещая каждый следующий байт в следующий элемент массива buffer
                reader.Close(); //Закрывем файл
            }
            return readAmplitudeValues(buffer); //Получаем и возвращаем значения амплитуд
        }

        private static double[] readAmplitudeValues(byte[] buffer) //Получение амплитудных значений в промежутке от -1 до 1
        {
            short MSB, LSB; // старший и младший байты
            double[] data = new double[buffer.Length / 2];
            //Так как исходный WAV фал имеет 16-битное кодирование, то каждое амплитудное занчение кодируеться 16 битами
            //Следовательно что бы считать одно амплитудное значение в массив типа double необходимо:
            //1. Считать два последоватьноидущих байта из массива buffer
            //2. Объединить их в одно значение целочисленого типа short(16 бит), в ином случае, будет потерян знак у числа 
            for (int i = 0; i < data.Length; i++)
            {
                // первым байтом будет MSB
                MSB = buffer[2 * i];
                // вторым байтом будет LSB
                LSB = buffer[2 * i + 1];
                // склеиваем два байта, чтобы получить 16-битное вещественное число
                // все значения делятся на максимально возможное - 2^15
                data[i] = ((short)(MSB << 8) | LSB) / 32768.0; //Преобразуем все занчения к промежутку от -1 до 1
            }
            return data;
        }
    }
}
