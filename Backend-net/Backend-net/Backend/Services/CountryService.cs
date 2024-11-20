using Backend.Data;
using Backend.DTOs;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class CountryService : ICountryService
{
    private readonly DataContext _dataContext;

    public CountryService(DataContext context)
    {
        _dataContext = context;
    }

    public async Task<List<Country>> GetAllAsync()
    {
        var countries = await _dataContext.Countries.ToListAsync();

        return countries;
    }

    public async Task<Country?> GetCountryById(int id)
    {
        return await _dataContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Country?> GetCountryByName(string name)
    {
        return await _dataContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<Country> PostCountryAsync(CountryDto countryDto)
    {
        var country = new Country() { Name = countryDto.Name };

        _dataContext.Add(country);
        await _dataContext.SaveChangesAsync();
        return country;
    }

    public async Task PutCountryAsync(int id, CountryDto countryDto)
    {
        var country = await GetCountryById(id);

        country.Name = countryDto.Name;

        _dataContext.Update(country);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var country = await GetCountryById(id);
        _dataContext.Countries.Remove(country!);
        await _dataContext.SaveChangesAsync();
    }
}