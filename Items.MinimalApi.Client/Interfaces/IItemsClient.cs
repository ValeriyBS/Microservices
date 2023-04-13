using System.Threading.Tasks;
using Items.Contracts.Responses;

namespace Items.MinimalApi.Client.Interfaces
{
    public interface IItemsClient
    {
        Task<ItemsResponseDto> GetItems();
    }
}
