namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITrainersService
    {
        IEnumerable<SelectListItem> GetAllTrainersInSelectList();
    }
}
