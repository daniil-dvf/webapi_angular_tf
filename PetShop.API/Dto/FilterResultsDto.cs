using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.API.Dto
{
    public class FilterResultsDto<T>
    {
        public int Total { get; set; }
        
        public int FilterCount { get; set; }
        
        public int Limit { get; set; }
        
        public int Offset { get; set; }
        
        public IEnumerable<T> Results { get; private set; }

        public FilterResultsDto(IEnumerable<T> results)
        {
            Results = results;
        }
    }
}
