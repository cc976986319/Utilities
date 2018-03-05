using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension.Mapping.Base
{
    public class BaseMapping<TIn, TOut>
    {
        public static TOut MappingTo(TIn data)
        {
            return new BaseMapping<TIn, TOut>().Mapping(data);
        }
    }
}
