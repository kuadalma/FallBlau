﻿using Javax.Net.Ssl;
using System.Net.Security;
using Xamarin.Android.Net;
using Object = Java.Lang.Object;

namespace LocalhostMAUI
{
    public partial class HttpClientService
    {
        public static HttpMessageHandler PlatfromSpecificHttpMessageHandler
        {
            get
            {
                // Func<HttpRequestMessage, X509Certificate2?, X509Chain?, SslPolicyErrors, bool>? ServerCertificateCustomValidationCallback
                var androidHttpHandler = new LocalhostAndroidMessageHandler
                {
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                    {
                        if (certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None)
                            return true;
                        return false;
                    },

                };

                return androidHttpHandler;
            }
        }

        class LocalhostAndroidMessageHandler : AndroidMessageHandler
        {
            protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection) => new LocalhostHostNameVerifier();
        }
        class LocalhostHostNameVerifier : Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
            {
                if (HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) || hostname == "127.0.0.1")
                {
                    return true;
                }
                return false;
            }
        }
    }
}