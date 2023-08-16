namespace KPSService
{
    public class KPSConfiguration
    {
        #region Fields

        public static KPSConfiguration Instance = new KPSConfiguration();

        private string endPoint = "https://kpsv2.nvi.gov.tr/Services/RoutingService.svc";  
        private string username = null; //KRM ile başlayan kurum adı
        private string password = null;

        #endregion

        #region Constructors

        private KPSConfiguration()
        {
        }

        public KPSConfiguration setUsername(string _username)
        {
            this.username = _username;
            return Instance;
        }

        public KPSConfiguration setPassword(string _password)
        {
            this.password = _password;
            return Instance;
        }
        #endregion

        #region Properties

        public string EndPoint
        {
            
            get { return endPoint; }
        }

        public string Username
        {
            get { return username; }
        }

        public string Password
        {
            get { return password; }
        }

        #endregion

    }
}
