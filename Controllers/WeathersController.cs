using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherAPI.Filters;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class WeathersController : Controller
    {
        private readonly AppDbContext _context;

        [AuthenticationFilter]
        public WeathersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Weathers
        public async Task<IActionResult> Index()
        {
            var newWeather = new Weather();
            var weatherModel = new List<Weather>();
            var w = FromApi();
            newWeather.Id = new Guid();
            newWeather.Humidity = w.Humidity;
            newWeather.Temperature = w.Temperature;
            newWeather.MinTemperature = w.MinTemperature;
            newWeather.MaxTemperature = w.MaxTemperature;
            weatherModel.Add(newWeather);
            weatherModel.AddRange(await _context.Weather.ToListAsync());
            return View(weatherModel);
        }

        public WeatherViewModel FromApi()
        {
            var weather = new WeatherViewModel();
            try
            {
                var w = new WeatherViewModel();
                var httpClient1 = new HttpClient();
                var apiUrl = "http://demo4567044.mockable.io/";
                httpClient1.BaseAddress = new Uri(apiUrl);
                var response = httpClient1.GetAsync("weather").Result;
                if (response.IsSuccessStatusCode)
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    weather = JsonConvert.DeserializeObject<WeatherViewModel>(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return weather;
        }

        // GET: Weathers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var weather = await _context.Weather
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weather == null) return NotFound();

            return View(weather);
        }

        // GET: Weathers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weathers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Humidity,Temperature,MinTemperature,MaxTemperature")]
            Weather weather)
        {
            if (ModelState.IsValid)
            {
                weather.Id = Guid.NewGuid();
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(weather);
        }

        // GET: Weathers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var weather = await _context.Weather.FindAsync(id);
            if (weather == null) return NotFound();
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Humidity,Temperature,MinTemperature,MaxTemperature")]
            Weather weather)
        {
            if (id != weather.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherExists(weather.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(weather);
        }

        // GET: Weathers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var weather = await _context.Weather
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weather == null) return NotFound();

            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var weather = await _context.Weather.FindAsync(id);
            _context.Weather.Remove(weather);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherExists(Guid id)
        {
            return _context.Weather.Any(e => e.Id == id);
        }
    }
}