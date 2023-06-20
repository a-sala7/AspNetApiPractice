using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Models.Interfaces
{
    public interface IBilingual : ILocalizable
    {
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
    }
}
