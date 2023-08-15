namespace KPSService
{
    public class KPSConfiguration
    {
        #region Fields

        public static KPSConfiguration Instance = new KPSConfiguration();

        private string endPoint = "https://kpsv2.nvi.gov.tr/Services/RoutingService.svc";  
        private string username = "kurumusername";
        private string password = "kurumsifre";

        #endregion

        #region Constructors

        private KPSConfiguration()
        {
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
