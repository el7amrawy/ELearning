using ELearning.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Attributes
{
    public class CourseAccessAttribute : TypeFilterAttribute
    {
        public CourseAccessAttribute() : base(typeof(CourseAccessFilter)) { }
    }
}