using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ExampleApi.Models;

namespace ExampleApi.Data
{
    public interface IPhonesRepository
    {
        List<Phone> GetAllPhones();
        List<Phone> GetPhonesByManufacturer(string manufacturer);
        List<Phone> GetPhonesByModel(string model);
        Phone GetPhone(long id);
        Phone AddPhone(Phone phone);
        void DeletePhone(long id);
    }

    public class PhonesRepository : IPhonesRepository
    {
        private static ConcurrentDictionary<long, Phone> PhonesDataStore { get; }

        static PhonesRepository()
        {
            // Preload the data store with some default values.
            PhonesDataStore = new ConcurrentDictionary<long, Phone>
            {
                [1] = new Phone
                {
                    Id = 1,
                    Manufacturer = "Samsung",
                    Model = "Galaxy S9",
                    Cost = 719.99m,
                    StorageCapacity = 64,
                },
                [2] = new Phone
                {
                    Id = 2,
                    Manufacturer = "Samsung",
                    Model = "Galaxy S9+",
                    Cost = 839.99m,
                    StorageCapacity = 64,
                },
                [3] = new Phone
                {
                    Id = 3,
                    Manufacturer = "Apple",
                    Model = "iPhone X",
                    Cost = 999.00m,
                    StorageCapacity = 64,
                },
                [4] = new Phone
                {
                    Id = 4,
                    Manufacturer = "Apple",
                    Model = "iPhone X",
                    Cost = 1149.00m,
                    StorageCapacity = 256,
                },
                [5] = new Phone
                {
                    Id = 5,
                    Manufacturer = "Motorola",
                    Model = "Z2 Force",
                    Cost = 720m,
                    StorageCapacity = 64,
                },
                [6] = new Phone
                {
                    Id = 6,
                    Manufacturer = "OnePlus",
                    Model = "5T",
                    Cost = 499m,
                    StorageCapacity = 64,
                },
                [7] = new Phone
                {
                    Id = 7,
                    Manufacturer = "OnePlus",
                    Model = "5T",
                    Cost = 599m,
                    StorageCapacity = 128,
                }
            };
        }

        public List<Phone> GetAllPhones()
        {
            return PhonesDataStore.Values.ToList();
        }

        public List<Phone> GetPhonesByManufacturer(string manufacturer)
        {
            return PhonesDataStore.Values
                .Where(p => p.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Phone> GetPhonesByModel(string model)
        {
            return PhonesDataStore.Values
                .Where(p => p.Model.Equals(model, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Phone GetPhone(long id)
        {
            if (PhonesDataStore.TryGetValue(id, out var phone))
            {
                return phone;
            }

            return null;
        }

        public Phone AddPhone(Phone phone)
        {
            var id = PhonesDataStore.Values.Select(p => p.Id).Max();
            phone.Id = ++id;

            return PhonesDataStore.AddOrUpdate(id, phone, (k, v) => phone);
        }

        public void DeletePhone(long id)
        {
            PhonesDataStore.TryRemove(id, out var _);
        }
    }
}