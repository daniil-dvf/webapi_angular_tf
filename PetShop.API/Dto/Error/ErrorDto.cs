using System.Collections.Generic;
using System.Linq;

namespace PetShop.API.Dto.Error
{
    public class ErrorDto
    {
        public Dictionary<string, List<string>> errors { get; set; }
        public ErrorDto(string name, params string[] errors)
        {
            this.errors = new Dictionary<string, List<string>>
            {
                { name, errors.ToList() }
            };
        }
    }
}
