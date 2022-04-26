namespace ChessBurgas64.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public abstract class CategoryInputModel
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
