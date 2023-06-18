using POS.BL.UOW;
using POS.DAL.Database;

namespace MVCCore.BaseData
{
    public class BaseSelect
    {
        private readonly IUnitOfWork _uow;
        public BaseSelect(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
