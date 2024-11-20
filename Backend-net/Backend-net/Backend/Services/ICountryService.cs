using Backend.DTOs;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public interface ICountryService
{
    Task<List<Country>> GetAllAsync();

    Task<Country?> GetCountryById(int id);

    Task<Country?> GetCountryByName(string name);

    Task<Country> PostCountryAsync(CountryDto countryDto);

    Task PutCountryAsync(int id, CountryDto countryDto);

    Task DeleteAsync(int id);
};