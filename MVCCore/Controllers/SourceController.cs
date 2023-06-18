using AutoMapper;
using LogicHelper.Consts;
using Microsoft.AspNetCore.Mvc;
using MVCCore.BaseData;
using POS.BL.UOW;
using POS.DAL.Models;

namespace MVCCore.Controllers
{
    public class SourceController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public SourceController(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int sourceTypeId , int pageNo = 0)
        {
            int pageSize = 20;
            var data = await _uow.Sources.FindAllAsync(x => x.SourceTypeId == sourceTypeId,20,0,x => x.AName,OrderBy.Descending, new[] { "SourceType" });
            return View(data);   
        }


        public async Task<IActionResult> Form(int id)
        {
            var data = new SourceDTO();
            if (id > 0)
            {
                var test = await _uow.Sources.GetByIdAsync(id);
                data = _mapper.Map<SourceDTO>(test);
                data = data??new SourceDTO();
            }
            FormLoad(data);
            return View(data);   
        }
        async Task<SourceDTO> FormLoad(SourceDTO model)
        {
            model.SourceTypes = _mapper.Map<IEnumerable<SourceTypeDTO>>(await _uow.SourceType.GetAllAsync()).ToList();
            return model;
        }
    }
}
