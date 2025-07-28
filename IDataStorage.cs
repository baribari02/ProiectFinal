using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Final
{
    internal interface IDataStorage
    {
        void SaveProducts(List<Product> products, string filePath);
        List<Product> LoadProducts(string filePath);
    }
}
