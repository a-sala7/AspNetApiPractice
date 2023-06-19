using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Models.Interfaces
{
    public interface ILocalizable
    {
        /*
         * This interface contains no members
         * But it indicates that the implementing clas
         * has properties ending in _(LangCode)
         * which can be localized with LocalizationHelper
        */
    }
}
