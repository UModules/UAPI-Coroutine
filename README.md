# UAPI Coroutine
UAPI Coroutine is a C# library designed to manage asynchronous API calls and processes within Unity using `IEnumerator`. This package leverages Unity's coroutine system to handle tasks that would otherwise require complex callback handling.

## Features
* **Coroutine-Based Operations:** Simplifies handling of asynchronous API requests using `IEnumerator`.
* **Modular Design:** Flexible and customizable to fit into a variety of Unity projects.
* **Seamless Integration:** Works naturally with Unity's existing coroutine system, without needing to introduce `async/await` or `Task`.

## Installation
### Git URL
UAPI Coroutine supports Unity Package Manager. To install the project as a Git package, follow these steps:
1. In Unity, open `Window -> Package Manager`.
2. Press the `+` button and choose **Add package from git URL...**.
3. Enter `"https://github.com/UModules/UAPI-Coroutine.gitupm"` and press **Add**.

### Unity Package
Alternatively, you can add the code directly to your project:
1. Clone the repo or download the latest release.
2. Add the UAPI Coroutine folder to your Unity project or import the `.unitypackage`.

### Manifest File
To install via git URL by editing the `manifest.json`, add this entry:

`"com.useffarahmand.uapi-coroutine": "https://github.com/UModules/UAPI-Coroutine.git#upm"`

## Usage
### Sample Usage
To see how UAPI Coroutine works, you can explore the sample provided:
1. Open Unity and load the `Sample/Scenes/APISample.unity` scene.
2. Run the sample to see how API requests are handled asynchronously using coroutines.

To demonstrate how to use UAPI Coroutine, here's a simple function that sends a request and handles the response:
```C#
private void OnRequest()
{
    APIClient.CreateAndSendRequest(/*REQUEST KEY*/,
                                   /*NetworkLoadingHandlerCreator*/,
                                   /*APIConfigData*/, 
                                   /*RequestFeedbackConfig*/,
                                   /*RequestSendConfig*/,
                                   Response);

    void Response(NetworkResponse response)
    {
        if (response.isSuccessful)
        {
            Debug.Log($"Response: {response.ToString()}");
        }
        else
        {
            Debug.LogError("Request failed: " + response.errorMessage);
        }
    }
}
```
#### Key Classes and Configurations:
1. **`REQUEST KEY`:** The API key or authentication token needed to authorize the API request.
2. **`NetworkLoadingHandlerCreator`:** Handles the loading state of the network request (e.g., showing a loading spinner). You can use `NetworkLoadingHandlerCreator.CreateAndGet()` to generate a loading handler.
3. **`APIConfigData`:** Configuration data for the API request. This includes the base URL, endpoint, HTTP method, headers, and other request settings.
4. **`RequestFeedbackConfig`:** Configuration that controls how feedback is displayed during the request, such as showing success or error messages.
5. **`RequestSendConfig`:** Configuration for sending the request, such as retry settings or timeout options.
6. **`Response(NetworkResponse response)`:** The callback function that handles the API response. It checks whether the response is successful, and logs the result accordingly.

## Alternative Version: `UniTask` Support
If you prefer using UniTask for asynchronous operations, we also offer a version of this package that integrates with `UniTask`. You can find it [here](https://github.com/UModules/UAPI).

## Documentation
For detailed documentation, please refer to the [UAPI Coroutine Documentation](https://github.com/UModules/UAPI-Coroutine/wiki).

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/UModules/UAPI-Coroutine/blob/main/LICENSE) file for details.
