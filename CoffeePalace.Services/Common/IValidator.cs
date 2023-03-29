using CoffeePalace.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePalace.Services.Common
{
    public interface IValidator<T> where T : Entity
    {
        Result Validate(T model);
    }
}
