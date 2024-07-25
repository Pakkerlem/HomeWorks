namespace Task3
{
    internal class Program
    {

        static int[] arr1 = { 1, 2, 3 };
        static int[] arr2 = { 4, 5, 6 };

        static int val1;
        static int val2;

        public static void summ()
        {
            val1 = 0;
            for (int i = 0; i < arr1.Length; i++) 
            {
                val1 += arr1[i];
            }
        }

        public static void summ2()
        {
            val2 = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                val2 += arr2[i];
            }
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(summ);
            thread.Start();
            thread.Join();
            Console.WriteLine(val1);

            Thread thread2 = new Thread(summ2);
            thread.Start();
            thread.Join();
            Console.WriteLine(val2);

            Console.WriteLine(val1+val2);

        }
    }
}
