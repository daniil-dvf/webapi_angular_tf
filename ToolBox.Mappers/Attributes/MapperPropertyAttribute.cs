using System;

namespace ToolBox.Mappers.Attributes
{
    public class MapperPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public MapperPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
