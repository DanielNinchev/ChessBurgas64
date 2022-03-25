namespace ChessBurgas64.Web.ViewModels.GroupMembers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupMemberInputModel
    {
        [Required]
        public string MemberId { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
    }
}
