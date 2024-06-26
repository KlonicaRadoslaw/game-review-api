﻿using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country getCountryById(int id);
        Country getCountryByProducer(int producerId);
        ICollection<Country> GetProducersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}
