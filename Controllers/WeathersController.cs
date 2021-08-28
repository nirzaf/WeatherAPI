using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class WeathersController : Controller
    {
        private readonly AppDbContext _context;

        public WeathersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Weathers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Weather.ToListAsync());
        }

        public async Task<IActionResult> FromApi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://demo4567044.mockable.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var product = new WeatherViewModel();
                var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    //var result = JsonSerializer.Deserialize
                    //    <IEnumerable<WeatherViewModel>>(responseStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View(null);
            //return View(await _context.Weather.ToListAsync());
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