namespace ChessBurgas64.Web.ViewModels.GroupMembers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupMemberInputModel
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string MemberId { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
    }
}
