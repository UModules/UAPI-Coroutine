using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UAPIModule.SharedTypes;
using UnityEngine;
using UnityEngine.TestTools;

namespace UAPIModule.Test
{
    [TestFixture]
    public class HEADTests
    {
        [UnityTest]
        public IEnumerator SendRequest_RequestAndReceiveCallbackFromAPI()
        {
            // Arrange
            var baseURL = "https://httpbin.org";
            var endpoint = "/headers";
            var headers = new Dictionary<string, string>();
            var bodies = new Dictionary<string, object>();
            var screenConfig = RequestScreenConfig.GetNoScreen();

            bool callbackInvoked = false;
            NetworkResponse receivedResponse = null;

            // Define the callback
            Action<NetworkResponse> callback = (response) =>
            {
                callbackInvoked = true;
                receivedResponse = response;
            };

            // Act
            var config = APIRequestConfig.GetWithoutToken(
                baseURL: baseURL,
                endpoint: endpoint,
                methodType: HTTPRequestMethod.HEAD,
                headers: headers,
                bodies: bodies,
                timeout: 10000
            );

            APIClient.SendRequest(config, screenConfig, callback);

            // Wait for callback or timeout
            float timeout = Time.time + 10000f;
            while (!callbackInvoked && Time.time < timeout)
            {
                yield return null;
            }

            // Assert
            Assert.IsTrue(callbackInvoked, "The callback was not invoked.");
            Assert.NotNull(receivedResponse, "The response passed to the callback is null.");
            Assert.IsTrue(receivedResponse.isSuccessful, "The response indicates the request was not successful.");
            Assert.AreEqual(200, receivedResponse.statusCode, "The status code is incorrect.");
            Assert.IsNull(receivedResponse.errorMessage, "The error message should be null.");
        }
    }
}
