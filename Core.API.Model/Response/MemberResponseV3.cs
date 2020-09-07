using Core.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public class MemberResponseV3
    {
        public int Id { get; set; }
        public int CountOfMembers { get; set; }
        public string MPI { get; set; }
        public string MPIShort { get; set; }
        public int MCOId { get; set; }
        public int PMGId { get; set; }
        public int PCPId { get; set; }
    }
}
