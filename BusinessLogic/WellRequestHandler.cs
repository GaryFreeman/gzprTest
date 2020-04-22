using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BusinessLogic
{
    public class WellRequestHandler : IWellRequestHandler
    {
        private readonly IWellRepository _wellRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IFieldRepository _fieldRepository;

        public WellRequestHandler(
            IWellRepository wellRepository,
            ICompanyRepository companyRepository,
            IWorkshopRepository workshopRepository,
            IFieldRepository fieldRepository)
        {
            _wellRepository = wellRepository;
            _companyRepository = companyRepository;
            _workshopRepository = workshopRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<IEnumerable<WellModel>> Get()
        {
            var wells = (await _wellRepository.Get()).ToList();

            var companyIds = wells.Select(x => x.CompanyId).Where(x => x != default).Distinct().ToList();
            var workshopIds = wells.Select(x => x.WorkshopId).Where(x => x != default).Distinct().ToList();
            var fieldIds = wells.Select(x => x.FieldId).Where(x => x != default).Distinct().ToList();

            var companiesTask = Task.FromResult(Enumerable.Empty<CompanyModel>());
            if (companyIds.Any())
            {
                companiesTask = _companyRepository.GetByIds(companyIds);
            }
            
            var workshopsTask = Task.FromResult(Enumerable.Empty<WorkshopModel>());
            if (workshopIds.Any())
            {
                workshopsTask = _workshopRepository.GetByIds(workshopIds);
            }
            
            var fieldsTask = Task.FromResult(Enumerable.Empty<FieldModel>());
            if (fieldIds.Any())
            {
                fieldsTask = _fieldRepository.GetByIds(fieldIds);
            }

            await Task.WhenAll(companiesTask, workshopsTask, fieldsTask);

            foreach (var well in wells)
            {
                var company = companiesTask.Result.FirstOrDefault(x => x.Id == well.CompanyId);
                var workshop = workshopsTask.Result.FirstOrDefault(x => x.Id == well.WorkshopId);
                var field = fieldsTask.Result.FirstOrDefault(x => x.Id == well.FieldId);

                well.Company = company;
                well.Workshop = workshop;
                well.Field = field;
            }

            return wells;
        }
        
        public Task<WellModel> Get(int id)
        {
            return _wellRepository.GetById(id);
        }

        public Task Create(WellModel well)
        {
            return _wellRepository.Create(well);
        }

        public Task Update(WellModel model)
        {
            return _wellRepository.Update(model);
        }

        public Task Delete(int id)
        {
            return _wellRepository.Delete(id);
        }
    }
}