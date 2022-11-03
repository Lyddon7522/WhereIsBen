using GraphQL;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Http;

namespace WhereIsBen.Data
{
    public class LocationService
    {
        private readonly GraphQLHttpClient _graphqlClient =
            new GraphQLHttpClient("https://wheres-bendo.hasura.app/v1/graphql", new NewtonsoftJsonSerializer());

        private readonly GraphQLRequest _getLocationQuery = new GraphQLRequest
        {
            Query = @"
                query {
                  locations {
                    id
                    location
                  }
                }
            "
        };

        public async Task<GraphQLResponse<LocationData>> GetLocation()
        {
            var result = await _graphqlClient.SendQueryAsync<LocationData>(_getLocationQuery);

            Console.WriteLine(result.Data.locations);

            return result;
        }
    }
}