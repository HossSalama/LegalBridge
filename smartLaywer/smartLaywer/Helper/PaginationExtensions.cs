namespace smartLaywer.Helper
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
             this IQueryable<TDestination> query,
             int pageNumber,
             int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
        }
    }
}
