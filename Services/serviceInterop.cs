using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.JSInterop;

namespace ConsoleEncryption.Services
{
    public static class serviceInterop
    {
        public static async Task<string> GetFileData(ElementRef fileInputRef)
            {
                return (await JSRuntime.Current.InvokeAsync<string>("getFileData", fileInputRef));

            }

    }
}