using System.Threading;
using System.Threading.Tasks;
using Hivemind.Data;
using Hivemind.Repositories.DataRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hivemind.Controllers
{
    [Route("DataItem")]
    public class DataItemController : Controller
    {
        private static class RouteNames
        {
            public const string Create = nameof(Create);
            public const string Edit = nameof(Edit);
            public const string Delete = nameof(Delete);
            public const string GetAll = nameof(GetAll);
        }

        private readonly DataItemRepository _repository;

        public DataItemController(IConfiguration configuration)
        {
            this._repository = new DataItemRepository(configuration);
        }

        [HttpGet(Name = RouteNames.GetAll)]
        public async Task<IActionResult> GetAllItems(CancellationToken token = default(CancellationToken))
        {
            var items = await this._repository.FindAll();

            return this.Ok(items);
        }

        [HttpPost(Name = RouteNames.Create)]
        public async Task<IActionResult> Create(DataItem item, CancellationToken token = default(CancellationToken))
        {
            await this._repository.Add(item);

            return this.CreatedAtRoute(nameof(Create), item);
        }
        
        [HttpPatch]
        public async Task<IActionResult> Update(DataItem obj, CancellationToken token = default(CancellationToken))
        {
            await this._repository.Update(obj);

            return this.Ok();
        }

        [Route("{id:int}")]
        [HttpPost(Name = RouteNames.Delete)]
        public async Task<IActionResult> Delete(int? id, CancellationToken token = default(CancellationToken))
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this._repository.Remove(id.Value);

            return this.Ok();
        }
    }
}