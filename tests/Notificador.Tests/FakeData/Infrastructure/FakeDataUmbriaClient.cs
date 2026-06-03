namespace Notificador.Tests.FakeData.Infrastructure
{
    internal static class FakeDataUmbriaClient 
    {
        public const string FAKE_USER_REAL = "RealUser";
        public const string FAKE_USER_NOEXIST = "NoExistUser";

        public const string FAKE_PASSWORD = "FakePassword";
        public const string FAKE_PASSWORD_WRONG = "WrongPassword";
        public const string FAKE_PASSWORD_EXCEPTION = "ExceptionPassword";

        public const string FAKE_URL = "http://fakeurl.com";

        public const string FAKE_HTML_RESPONSE = "<html><body>" +
            "<div class='mensaje' data-tipo='Director'>Mensaje Director 1</div>" +
            "<div class='mensaje' data-tipo='Jugador'>Mensaje Jugador 1</div>" +
            "<div class='mensaje' data-tipo='Privados'>Mensaje Privado 1</div>" +
            "</body></html>";

        public const string FAKE_HTML_EMPTY_RESPONSE = "<html><body>" +            
            "</body></html>";
    }
}