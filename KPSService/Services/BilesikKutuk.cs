using KPSService.BilesikKutukSorgula;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace KPSService
{
    public class BilesikKutuk
    {

        public long tc { get; set; } 
        public int yil { get; set; }
        public int ay { get; set; }
        public int gun { get; set; }
        ChannelFactory<BilesikKutukSorgulaKimlikNoServis> channelFactory = null;
        private KPSConfiguration kpsConfiguration = KPSConfiguration.Instance;

        public BilesikKutuk(long _tc, int _yil, int _ay, int _gun)
        {
            this.tc = _tc;
            this.yil = _yil;
            this.ay = _ay;
            this.gun = _gun;

            channelFactory = new ChannelFactory<BilesikKutukSorgulaKimlikNoServis>("CustomBinding_BilesikKutukSorgulaKimlikNoServis", new EndpointAddress(kpsConfiguration.EndPoint));
            if (channelFactory.Credentials != null)
            {
                //channelFactory.Credentials.SupportInteractive = false;
            }

        }

        public BilesikKutukBilgileriSonucu getSonuc()
        {

            BilesikKutukSorgulaKimlikNoServis servis = channelFactory.CreateChannelWithIssuedToken(KPSServiceFactory.Instance.CreateToken(kpsConfiguration));

            List<BilesikKutukSorgulaKimlikNoSorguKriteri> kriter = new List<BilesikKutukSorgulaKimlikNoSorguKriteri>();
            BilesikKutukSorgulaKimlikNoSorguKriteri kk = new BilesikKutukSorgulaKimlikNoSorguKriteri();
            kk.KimlikNo = this.tc;
            kk.DogumYil = this.yil;
            kk.DogumAy = this.ay;
            kk.DogumGun = this.gun;
            kriter.Add(kk);
            return servis.Sorgula(kriter);
        }

        public BilesikKutuk setConf(KPSConfiguration _conf)
        {
            this.kpsConfiguration = _conf;
            return this;
        }

    }
}
