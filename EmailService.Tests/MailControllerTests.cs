using EmailService.Models;
using EmailService.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using Xunit;

namespace WebService.Tests
{
    public class MailControllerTests : IClassFixture<WebServiceFactory<Program>>
    { 
        private const string ApiRoute = "/api/mails";
        private const string SendToEmailAddress = "<Put the send to email address here>";
        private readonly HttpClient _client;
    
        public MailControllerTests(WebServiceFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task CanSendMail()
        {
            MailCreateDto m =
                new MailCreateDto { Recipients = new string[] { SendToEmailAddress } };

            var response = await _client.PostAsJsonAsync(ApiRoute, m);
            var mail = await response.Content.ReadFromJsonAsync<MailReadDto>();

            response.EnsureSuccessStatusCode();
            Assert.Equal("OK", mail?.Result);
        }

        [Fact]
        public async Task CanGetMails()
        {
            MailCreateDto m =
                new MailCreateDto { Recipients = new string[] { SendToEmailAddress } };

            for (int i = 2; i > 0; i--)
            {
                await _client.PostAsJsonAsync(ApiRoute, m);
            }
            var response = await _client.GetAsync($"{ApiRoute}");
            var mails = await response.Content.ReadFromJsonAsync<List<MailReadDto>>();

            response.EnsureSuccessStatusCode();
            Assert.True(mails?.Count >= 2);
        }

        [Fact]
        public async Task CannotSendMailWithoutRecipients()
        {
            MailCreateDto m = new MailCreateDto();

            var response = await _client.PostAsJsonAsync($"{ApiRoute}", m);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("The Recipients field is required.", responseString);
        }

        [Fact]
        public async Task CannotSendMailWithNoRecipients()
        {
            MailCreateDto m =
                new MailCreateDto { Recipients = new string[0] };

            var response = await _client.PostAsJsonAsync(ApiRoute, m);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("At least one recipient is required.", responseString);
        }

        [Fact]
        public async Task CannotSendMailToInvalidAddress()
        {
            MailCreateDto m =
                new MailCreateDto { Recipients = new string[] { string.Empty } };

            var response = await _client.PostAsJsonAsync(ApiRoute, m);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("At least one recipient is not a valid email address.", responseString);


            //var mail = await response.Content.ReadFromJsonAsync<Mail>();
            
            //response.EnsureSuccessStatusCode();
            //Assert.Equal("Failed", mail?.Result);
        }
    }
}
