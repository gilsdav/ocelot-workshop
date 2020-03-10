using GraphQL.Types;
using PizzaGraphQL.Services;

namespace PizzaGraphQL.GraphQL
{
    public class SecuritySchema: Schema
    {
        public SecuritySchema(IAuthenticationService authenticationService)
        {
            Query = new SecurityQuery(); // Requis même vide
            Mutation = new SecurityMutation(authenticationService);
        }
    }
}