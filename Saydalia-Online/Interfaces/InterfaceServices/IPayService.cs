using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IPayService
    {
        Task<JsonResult> CreateOrder([FromBody] JsonObject data);

        Task<JsonResult> CompleteOrder([FromBody] JsonObject data);



    }
}
