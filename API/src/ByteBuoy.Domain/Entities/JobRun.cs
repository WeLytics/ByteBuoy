using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuoy.Domain.Entities
{
    public class JobRun
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }   
        /// domain entity for JobRun    
        /// 
    }
}
