using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PhapY.TrinhDo.Dto
{
    [AutoMapTo(typeof(Model.TrinhDo))]
    public class TrinhDoDto : EntityDto
    {
        public string TenTrinhDo { get; set; }
        public string DienGiai { get; set; }
    }
}
