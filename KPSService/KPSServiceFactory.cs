using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Security;
using WSTrustChannel = Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannel;
using WSTrustChannelFactory = Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannelFactory;

namespace KPSService
{
    public class KPSServiceFactory
    {
        #region Constructors

        private KPSServiceFactory() { }

        #endregion

        #region Fields

        private static KPSServiceFactory instance;

        private SecurityToken token;

        #endregion

        #region Properties

        public static KPSServiceFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new KPSServiceFactory();

                return instance;
            }
        }

        #endregion

        #region Methods

        public SecurityToken CreateToken(KPSConfiguration _kpsConfiguration)
        {
            if (token == null || token.ValidTo <= DateTime.Now.ToUniversalTime())
            {
                if (_kpsConfiguration.Username == null || _kpsConfiguration.Password == null){
                    throw new ArgumentNullException("Kullanıcı adı veya şifre girilmemiş");
                }
                WSTrustChannelFactory trustChannelFactory = new WSTrustChannelFactory("STSIssuerService");
                trustChannelFactory.TrustVersion = TrustVersion.WSTrust13;
                trustChannelFactory.Credentials.UserName.UserName = _kpsConfiguration.Username;
                trustChannelFactory.Credentials.UserName.Password = _kpsConfiguration.Password;

                WSTrustChannel channel = (WSTrustChannel)trustChannelFactory.CreateChannel();
                RequestSecurityToken rst = new RequestSecurityToken(RequestTypes.Issue);
                rst.AppliesTo = new EndpointAddress(_kpsConfiguration.EndPoint);
                RequestSecurityTokenResponse rstr;

                token = channel.Issue(rst, out rstr);
            }

            return token;
        }

        #endregion
    }
}
