# KPS-service
Kimlik Paylaþým Sistemi .Net Framework 4.8

# KPS SOAP Sistemi Kütüphanesi
Bu kütüphane, KPS SOAP sistemine .Net Framework 4.8 ile eriþim saðlamak için tasarlanmýþtýr. Kütüphaneyi NuGet üzerinde yayýnlamayý planlýyorsanýz, bu README dosyasý size baþlangýç için yardýmcý olacaktýr.

## Teknik Bilgiler
KPS sisteminin teknik alt yapýsý hakkýnda detaylý bilgi için bu [linki](https://kpsbasvuru.nvi.gov.tr/Acik/KpsTeknikBilgi) ziyaret edebilirsiniz.

## Kurulum

Kütüphaneyi NuGet üzerinden projenize eklemek için þu komutu kullanabilirsiniz:

```
dotnet add package KPSService
```
## Gereksinim
```
dotnet add package Microsoft.IdentityModel --version 7.0.0
```
## App.config dosyasýna eklenecekler
```
<configuration>
.
.
.
<system.serviceModel>
		<bindings>
			<customBinding>
				<binding name="CustomBinding_BilesikKutukSorgulaKimlikNoServis">
					<security defaultAlgorithmSuite="Default" authenticationMode="IssuedTokenOverTransport"
                        requireDerivedKeys="false" includeTimestamp="true" messageSecurityVersion="WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10">
						<issuedTokenParameters>
							<additionalRequestParameters>
								<trust:SecondaryParameters xmlns:trust="http://docs.oasis-open.org/ws-sx/ws-trust/200512">
									<trust:KeyType xmlns:trust="http://docs.oasis-open.org/ws-sx/ws-trust/200512">http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey</trust:KeyType>
								</trust:SecondaryParameters>
							</additionalRequestParameters>
							<issuer address="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13"
                                binding="ws2007HttpBinding" bindingConfiguration="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13" />
							<issuerMetadata address="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/mex" />
						</issuedTokenParameters>
						<!--<alternativeIssuedTokenParameters>
  <issuedTokenParameters>
    <issuer address="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13" bindingConfiguration="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13" binding="ws2007HttpBinding" />
  </issuedTokenParameters>
  <issuedTokenParameters>
    <issuer address="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13" bindingConfiguration="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13" binding="ws2007HttpBinding" />
  </issuedTokenParameters>
</alternativeIssuedTokenParameters>-->
						<localClientSettings detectReplays="false" />
						<localServiceSettings detectReplays="false" />
					</security>
					<textMessageEncoding />
					<httpsTransport />
				</binding>
			</customBinding>
			<ws2007HttpBinding>
				<binding name="IssuedTokenBinding">
					<readerQuotas maxArrayLength="2147483647" />
					<security mode="TransportWithMessageCredential">
						<message clientCredentialType="Windows" establishSecurityContext="false" />
					</security>
				</binding>
				<binding name="WS2007HttpBinding_IWSTrust13Sync" closeTimeout="00:01:00"
                    openTimeout="00:01:00" receiveTimeout="00:10:00" sendTimeout="00:01:00"
                    bypassProxyOnLocal="false" transactionFlow="false" hostNameComparisonMode="StrongWildcard"
                    maxBufferPoolSize="524288" maxReceivedMessageSize="65536"
                    messageEncoding="Text" textEncoding="utf-8" useDefaultWebProxy="true"
                    allowCookies="false">
					<readerQuotas maxDepth="32" maxStringContentLength="8192" maxArrayLength="2147483647"
                        maxBytesPerRead="4096" maxNameTableCharCount="16384" />
					<reliableSession ordered="true" inactivityTimeout="00:10:00"
                        enabled="false" />
					<security mode="TransportWithMessageCredential">
						<transport clientCredentialType="None" proxyCredentialType="None"
                            realm="" />
						<message clientCredentialType="None" negotiateServiceCredential="true"
                            algorithmSuite="Default" establishSecurityContext="false" />
					</security>
				</binding>
				<binding name="stsIssuerServiceBinding">
					<security mode="TransportWithMessageCredential">
						<transport clientCredentialType="None" />
						<message clientCredentialType="UserName" establishSecurityContext="false" />
					</security>
				</binding>
				<binding name="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13">
					<security mode="TransportWithMessageCredential">
						<transport clientCredentialType="None" />
						<message establishSecurityContext="false" />
					</security>
				</binding>
			</ws2007HttpBinding>
		</bindings>
		<client>
			<endpoint address="https://kimlikdogrulama.nvi.gov.tr/Services/Issuer.svc/IWSTrust13"
                binding="ws2007HttpBinding" bindingConfiguration="stsIssuerServiceBinding"
                contract="Microsoft.IdentityModel.Protocols.WSTrust.IWSTrustChannelContract"
                name="STSIssuerService" />
			<endpoint address="https://kpsv2.nvi.gov.tr/Services/RoutingService.svc"
                binding="customBinding" bindingConfiguration="CustomBinding_BilesikKutukSorgulaKimlikNoServis"
                contract="BilesikKutukSorgula.BilesikKutukSorgulaKimlikNoServis"
                name="CustomBinding_BilesikKutukSorgulaKimlikNoServis" />
		</client>
	</system.serviceModel>
	.
	.
	.
	</configuration>
```

## Kullanýmý
```bash
BilesikKutuk bilesikKutuk = new BilesikKutuk(17497644136, 1996, 12, 10);
bilesikKutuk.setConf(KPSConfiguration.Instance
    .setUsername("KRM-123456")
    .setPassword("123456"));
Console.WriteLine(bilesikKutuk.getSonuc().SorguSonucu[0].KimlikNo);
```
