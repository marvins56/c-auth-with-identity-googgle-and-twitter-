using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace STMIS.Models.ViewModels
{
    public class ClassViewModel
    {
        public IEnumerable<SelectListItem>? ClassList { get; set; }
        public string? classnames { get; set; }
    }
}
