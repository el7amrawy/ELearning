using ELearning.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Attributes
{
    public class SectionExistsAttribute : TypeFilterAttribute
    {
        public SectionExistsAttribute() : base(typeof(SectionExistsFilter)) { }
    }
}