using FreeSecur.API.Core.Mailing;
using FreeSecur.API.Core.Reflection;
using FreeSecur.API.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.UnitTests.Mailing
{
    [TestClass]
    public class MailServiceTests
    {
        [TestMethod]
        public async Task ShouldSendMail()
        {
            var smtpClientMock = new Mock<ISmtpClient>();
            smtpClientMock.Setup(x => x.SendMailAsync(It.IsAny<MailMessage>())).Returns(Task.CompletedTask);
            var interpolationService = new StringInterpolationService(new ReflectionService());

            var mailOptionsMock = new OptionsMock<FsMail>(new FsMail { FromAddress = "test@test.nl", Host = "123", Port = 123 });
            var mailService = new MailService(mailOptionsMock, interpolationService, smtpClientMock.Object);

            var message = new MailMessage<TestModel>("to@nl.nl", "test subject", "test body {Test}", new TestModel());

            await mailService.SendMail(message);
        }

        private class TestModel
        {
            public string Test => "123";
        }
    }
}
