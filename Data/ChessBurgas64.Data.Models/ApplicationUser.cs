// ReSharper disable VirtualMemberCallInConstructor
namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;
    using ChessBurgas64.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Payments = new HashSet<Payment>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public ClubStatus ClubStatus { get; set; }

        public int FideRating { get; set; }

        public FideTitle FideTitle { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
