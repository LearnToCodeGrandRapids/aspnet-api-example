using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleApi.Core.Data;
using ExampleApi.Core.Models;

namespace ExampleApi.Core.Services
{
    public interface IPhonesService
    {
        List<Phone> GetAllPhones();
        List<Phone> GetPhonesByManufacturer(string manufacturer);
        List<Phone> GetPhonesByModel(string model);
        List<Phone> GetPhonesByManufacturerAndModel(string manufacturer, string model);
        Phone GetPhoneById(long id);
        Phone AddPhone(Phone phone);
        void DeletePhone(long id);
    }

    public class PhonesService : IPhonesService
    {
        private IPhonesRepository PhonesRepo { get; }

        public PhonesService(IPhonesRepository phonesRepo)
        {
            PhonesRepo = phonesRepo;
        }

        public List<Phone> GetAllPhones()
        {
            return PhonesRepo.GetAllPhones();
        }

        public List<Phone> GetPhonesByManufacturer(string manufacturer)
        {
            return PhonesRepo.GetPhonesByManufacturer(manufacturer);
        }

        public List<Phone> GetPhonesByModel(string model)
        {
            return PhonesRepo.GetPhonesByModel(model);
        }

        public List<Phone> GetPhonesByManufacturerAndModel(string manufacturer, string model)
        {
            return PhonesRepo.GetPhonesByManufacturerAndModel(manufacturer, model);
        }

        public Phone GetPhoneById(long id)
        {
            return PhonesRepo.GetPhone(id);
        }

        public Phone AddPhone(Phone phone)
        {
            return PhonesRepo.AddPhone(phone);
        }

        public void DeletePhone(long id)
        {
            PhonesRepo.DeletePhone(id);
        }
    }
}
