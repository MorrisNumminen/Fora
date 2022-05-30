using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fora.Shared
{
    public class UserInterestModel
    {        

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public UserModel? User { get; set; }

        [ForeignKey(nameof(Interest))]
        public int InterestId { get; set; }
        public InterestModel? Interest { get; set; }
    }
}
