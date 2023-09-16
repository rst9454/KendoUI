using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KendoUIApp1.Extension.ExtraOrdinary
{
    internal interface IFilterDescriptor
    {
        Expression CreateFilterExpression(Expression instance);
    }
}
