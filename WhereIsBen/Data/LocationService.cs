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

        public async Task<GraphQLResponse<Location>> Updatelocation(int updateId, string updateLocation)
        {
            var updateLocationMutation = new GraphQLRequest
            {
                Query = @"
                    mutation ($id: Int!, $location: String!) {
                        update_locations_by_pk(pk_columns: {id: $id}, _set: {location: $location}) {
                            id
                            location
                        }
                    }
                ",
                Variables = new
                {
                    id = updateId,
                    location = updateLocation
                }
            };

            var response = await _graphqlClient.SendMutationAsync<Location>(updateLocationMutation);

            return response;
        }

        public async Task<GraphQLResponse<LocationData>?> GetLocation()
        {
            var result = await _graphqlClient.SendQueryAsync<LocationData>(_getLocationQuery);

            return result;
        }
    }
}
