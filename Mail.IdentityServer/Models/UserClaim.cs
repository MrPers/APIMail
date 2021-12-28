//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Mail.IdentityServer.Models
//{
//    public class UserClaim: IdentityUserClaim<long>
//    {
//        //public override long UserId { get; set; }
//        [ForeignKey("UserId")]
//        public User User { get; set; }
//        //public long ClaimId { get; set; }
//        [ForeignKey("ClaimId")]
//        public Claim Claim { get; set; }
//        //public override string ClaimType { get; set; }
//        //public override string ClaimValue { get; set; }
//    }
//}
