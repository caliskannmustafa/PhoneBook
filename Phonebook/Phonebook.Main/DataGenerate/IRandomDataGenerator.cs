using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.DataGenerate
{
    public interface IRandomDataGenerator
    {
        void Generate(int dataCount);
    }
}
