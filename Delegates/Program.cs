using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    /*
     * Delegate'ler nerede kullanılır ? 
     * Daha çok LINQ TO DATA konularından yoğun olarak kullanılır.
     * Aslında Database işlemleri konusunda Delegate'ler sıklıkla karşımıza çıkacak yapılardır.
     * func delegate - predicate delegate konuları geldiğinde bunlar daha iyi anlaşılacaktır!
     */

    internal class Program
    {
        
        static void Main(string[] args)
        {

            #region Genel olarak Delegate'ler
            //Summation(10, 20);
            //Substraction(10, 20);
            //Multiplication(10, 20);
            //Division(10, 20);

            MathematicalOperations m1 = new MathematicalOperations(Summation); // Bu şekilde örnekledikten sonra
                                                                               // içine delagate'in istediği biçimde bir
                                                                               // metot tanımlıyoruz.
            // m1.Invoke(40, 2);
            // Console.ReadLine();

            // Delegate'in yeni metotları işaret etmesi için aşağıdaki gibi tanımlamalar yapmamız gerekiyor.

            m1 += Substraction;
            m1 += Multiplication;
            m1 += Division;

            m1.Invoke(40,20); // Bu koddan sonra programın bütün metotları uygulayıp sonuçlar döndürmesini beklemeliyiz.
            Console.WriteLine();

            Delegate[] delegates = m1.GetInvocationList();  // Bu kod delegate'in işaret ettiği bütün fonksiyonların listesini döndürür.
            foreach (var item in delegates)
            {
                Console.WriteLine(item.Method.Name);        // item.Method.Name -> sadece işaret edilen metotların isimlerini döndürür.
            }
            
            /*
             * Delegate'leri kullanmamızın amacı onları Runtime süresinde istediğimiz sıra ile işleme sokmak içindir.
             * Yani demek oluyor ki bu metotların çalışma zamanları yazılan koda göre değişiyor.
             * Biz de bunu manüple etmek için Delegate'leri kullanıyoruz.
             */

            m1 -= Multiplication;                           // Bu işlem delagate'in içinden yanda verdiğimiz metodu çıkaracaktır.
            Console.WriteLine();
            foreach (var item in m1.GetInvocationList())    // Güncel listeyi döndürmek için GetInvocationList() ile çağırmamız gerekiyor.
            {
                Console.WriteLine(item.Method.Name);
            }

            m1 -= Substraction;
            Console.WriteLine();
            foreach (var item in m1.GetInvocationList())
            {
                Console.WriteLine(item.Method.Name);
            }
            
            m1 -= Division;
            m1 -= Summation;
            m1 += Substraction;
                 
            Console.WriteLine();
            foreach (var item in m1.GetInvocationList())
            {
                Console.WriteLine(item.Method.Name);
            }
            Console.WriteLine();
            #endregion


            #region Delegate İsimsiz Metot Kullanımı
            // Var olan bir metodu kullanarak delegate içine ekleme
            Print test1 = new Print(testMetot1);

            // isimsiz metot kullanımı
            Print test2 = new Print(delegate (string name, string surname)
            {
                Console.WriteLine($"{name} {surname}");
            });

            // Linq ile hayatımıza giren Lambda Exp kullanımı
            Print test3 = (name, surname) => 
            {
                Console.WriteLine($"{name} {surname}");
            };

            // Aşağıda delegate'in tanımlanma biçimine göre üç farklı çağırımı mevcut!
            test1("Ozan Berkay", "ÖZCAN");
            test2("Fatma", "Turan");
            test3("Metehan", "İncirci");


            #endregion

        }
        #region Delegate'ler için bazı metotlar
        delegate void MathematicalOperations(int value1, int value2);
        delegate void Print(string name, string surname);

        static void testMetot1(string name, string surname)
        {
            Console.WriteLine($"İsim : {name} \nSoyisim : {surname}");
        }

        static void Summation(int value1, int value2) // Metot imzası void - value1 - value2 olarak düşünülebilir.
                                                      // Yani fonksiyonun döndüreceği değer ile aldığı değerlerdir.
        {
            Console.WriteLine($"Toplam işleminin sonucu: {value1 + value2} ");
        }
        static void Substraction(int value1, int value2)
        {
            Console.WriteLine($"Çıkarma işleminin sonucu: {value1 - value2}");
        }

        static void Multiplication(int value1, int value2)
        {
            Console.WriteLine($"Çarpma işleminin sonucu: {value1 * value2}");
        }

        static void Division(int value1, int value2)
        {
            Console.WriteLine($"Bölme işleminin sonucu: {value1 / value2}");
        }
        #endregion
    }
}
