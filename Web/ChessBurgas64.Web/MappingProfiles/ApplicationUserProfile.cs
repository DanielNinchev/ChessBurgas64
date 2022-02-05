namespace ChessBurgas64.Web.MappingProfiles
{
    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.Areas.Identity.Pages.Account;

    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            this.CreateMap<ApplicationUser, RegisterModel.InputModel>().ReverseMap()
                .ForMember(apu => apu.UserName, rm => rm.MapFrom(apu => apu.Email));
        }
    }
}
