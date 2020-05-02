using GraphQL.Types;
using PizzaGraphQL.Services;

namespace PizzaGraphQL.GraphQL.SecurityGraphQL
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