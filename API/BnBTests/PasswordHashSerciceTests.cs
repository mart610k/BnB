using bnbAPI.Service;
using bnbAPI.Static;
using NUnit.Framework;

namespace BnBTests
{
    class PasswordHashSerciceTests
    {

        [TestCase("TestPassword", "$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2")]
        public void VerifyPasswordSuccessFullTest(string plaintextPass, string hashedPass)
        {
            string Configuration = "{\"workFactor\": \"12\"}";

            Config.ReadConfigurationFromJSON(Configuration);

            PasswordHashService passwordHashService = new PasswordHashService();

            Assert.IsTrue(passwordHashService.Verify(plaintextPass, hashedPass));
        }


        [TestCase("Testpassword", "$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2")]
        public void VerifyPasswordSuccessFailsTest(string plaintextPass, string hashedPass)
        {
            string Configuration = "{\"workFactor\": \"12\"}";

            Config.ReadConfigurationFromJSON(Configuration);

            PasswordHashService passwordHashService = new PasswordHashService();

            Assert.IsFalse(passwordHashService.Verify(plaintextPass, hashedPass));
        }

        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2","10")]
        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2", "14")]
        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2", "16")]
        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2", "20")]
        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2", "24")]
        public void CheckPasswordshouldBeUpdated(string hashedPassword, string workfactor)
        {
            string Configuration = "{\"workFactor\": \""+workfactor +"\"}";

            Config.ReadConfigurationFromJSON(Configuration);

            PasswordHashService passwordHashService = new PasswordHashService();

            Assert.IsTrue(passwordHashService.NeedsUpdate(hashedPassword));

        }


        [TestCase("$2a$12$D1wYr.4zSotsoU8Tpa20xuFjoruApVtKy0AWqJx9fpCuzAbL3cVT2", "12")]
        public void CheckPasswordDoNotNeedUpdate(string hashedPassword, string workfactor)
        {
            string Configuration = "{\"workFactor\": \"" + workfactor + "\"}";

            Config.ReadConfigurationFromJSON(Configuration);

            PasswordHashService passwordHashService = new PasswordHashService();

            Assert.IsFalse(passwordHashService.NeedsUpdate(hashedPassword));

        }

        [TestCase("hfiaosoiwrqr0912oiisa")]
        [TestCase("dsagfujw081fy")]
        [TestCase("90jsajh7s")]
        [TestCase("fnaoiseq0e2801nsa")]
        [TestCase("fasu907zx09ks")]
        public void GeneratePasswordHash(string plainTextpassword)
        {
            string Configuration = "{\"workFactor\": \"12\"}";

            Config.ReadConfigurationFromJSON(Configuration);

            PasswordHashService passwordHashService = new PasswordHashService();

            string hashedPass = passwordHashService.Hash(plainTextpassword);

            Assert.IsTrue(passwordHashService.Verify(plainTextpassword, hashedPass));
        }

    }
}
