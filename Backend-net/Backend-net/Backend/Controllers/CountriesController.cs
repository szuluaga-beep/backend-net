using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var countries = await _countryService.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var country = await _countryService.GetCountryById(id);

        if (country == null)
        {
            return NotFound($"The country with id {id} not found");
        }
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CountryDto countryDto)
    {
        var existCountry = await _countryService.GetCountryByName(countryDto.Name);

        if (existCountry != null)
        {
            return BadRequest("The country already exists");
        }

        try
        {
            var country = await _countryService.PostCountryAsync(countryDto);
            return Created("Created", country);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, CountryDto countryDto)
    {
        var country = await _countryService.GetCountryById(id);
        if (country == null)
        {
            return NotFound();
        }

        var existCountry = await _countryService.GetCountryByName(countryDto.Name);

        if (existCountry != null)
        {
            return BadRequest("The country already exists");
        }

        await _countryService.PutCountryAsync(id, countryDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var country = await _countryService.GetCountryById(id);
        if (country == null)
        {
            return NotFound();
        }

        await _countryService.DeleteAsync(id);
        return NoContent();
    }
}