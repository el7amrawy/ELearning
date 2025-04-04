using ELearning.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Attributes
{
    public class CourseOwnerAttribute : TypeFilterAttribute
    {
        public CourseOwnerAttribute() : base(typeof(CourseOwnerFilter)) { }
    }
}