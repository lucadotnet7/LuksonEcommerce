namespace Ecommerce.WebApp.Model.DTOs
{
    public record class DashboardDto(string? TotalRevenue, int TotalSales, int TotalClients, int TotalProducts);
}
