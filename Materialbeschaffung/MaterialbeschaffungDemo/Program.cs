using Gandalan.IDAS.WebApi.DTO;
using System;

namespace MaterialbeschaffungDemo
{
    class Program
    {
        /// <summary>
        /// This is a VERY simple way of providing material to IBOS3. If you need deeper 
        /// integration, contact Gandalan to implement your own IMaterialBeschaffungService
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var store = new DataStore();

            var requested = store.Get(item => item.AktuellerStatus == MaterialBeschaffungsJobStatiDTO.Angefragt);
            foreach (var item in requested)
            {
                Console.WriteLine($"Providing {item.Stueckzahl} of {item.KatalogNummer}");
                item.AktuellerStatus = MaterialBeschaffungsJobStatiDTO.Bereitgestellt;
                store.Put(item);
            }

            Console.ReadLine();
        }
    }
}
