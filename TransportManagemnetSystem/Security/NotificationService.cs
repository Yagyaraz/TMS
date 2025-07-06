using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Headers;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Utilities;
using static System.Formats.Asn1.AsnWriter;

namespace TransportManagemnetSystem.Security
{
    public class NotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConfigureSmsModel _smsModel;


        public NotificationService(IServiceProvider serviceProvider, IOptions<ConfigureSmsModel> smsModel)
        {
            _serviceProvider = serviceProvider;
            _smsModel = smsModel.Value;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<TransportDbContext>();

                    var today = DateTime.Today;

                    var records = await _context.bluebookRouteBima.ToListAsync();

                    foreach (var item in records)
                    {
                        var vehicleNo = _context.VehicleDarta.Where(x => x.Id == item.VehicleId).Select(x => x.VehicleNumber).FirstOrDefault();
                        if (item.BluebookEndDate.HasValue && item.BluebookEndDate.Value <= today.AddDays(7))
                        {
                                                        var sms = SendSms(item.VehicleId, $"Bluebook of {vehicleNo} is expiring soon (on {item.BluebookEndMiti:yyyy-MM-dd})");
                            Console.WriteLine($"Vehicle {item.VehicleId}: Bluebook is expiring soon (on {item.BluebookEndDate:yyyy-MM-dd})");
                        }

                        if (item.BimaEndDate.HasValue && item.BimaEndDate.Value <= today.AddDays(7))
                        {
                            var sms = SendSms(item.VehicleId, $"Beema of {vehicleNo} is expiring soon (on {item.BimaEndMiti:yyyy-MM-dd})");
                            Console.WriteLine($"Vehicle {item.VehicleId}: Beema is expiring soon (on {item.BimaEndDate:yyyy-MM-dd})");
                        }

                        if (item.RouteEndDate.HasValue && item.RouteEndDate.Value <= today.AddDays(7))
                        {
                            var sms = SendSms(item.VehicleId, $"Route Permit of {vehicleNo} is expiring soon (on {item.RouteEndMiti:yyyy-MM-dd})");
                            Console.WriteLine($"Vehicle {item.VehicleId}: Route Permit is expiring soon (on {item.RouteEndDate:yyyy-MM-dd})");
                        }

                        if (item.PollutionEndDate.HasValue && item.PollutionEndDate.Value <= today.AddDays(7))
                        {
                            var sms = SendSms(item.VehicleId, $"Pollution Certificate of {vehicleNo} is expiring soon (on {item.PollutionEndMiti:yyyy-MM-dd})");
                            Console.WriteLine($"Vehicle {item.VehicleId}: Pollution Certificate is expiring soon (on {item.PollutionEndDate:yyyy-MM-dd})");
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // run once per day
            }
        }
        private async Task<bool> SendSms(int id, string text)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var aa = await SendMsg(id);
                    var number = aa;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response;

                    var parameters = $"token={_smsModel.Token}&from={_smsModel.From}" + $"&to={number}&text={text}";
                    //var parameters = string.Format("sender={0}&to={1}&message={2}&token={3}", "BiratnagarMun", number, text, _smsModel.Token);

                    response = await client.GetAsync("https://api.sparrowsms.com/v2/sms/?" + parameters);
                    var a = response.Content.ReadAsStringAsync().Result;

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        private async Task<string> SendMsg(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<TransportDbContext>();

                var orgid = await _context.VehicleDarta.Where(x => x.Id == id).Select(x => x.OrganizationId).FirstOrDefaultAsync();
                var Mobile = await _context.OrganizationDetail.Where(x => x.Id == orgid).Select(x => x.PhoneNumber).FirstOrDefaultAsync();

                return Mobile;
            }
        }
    }
}