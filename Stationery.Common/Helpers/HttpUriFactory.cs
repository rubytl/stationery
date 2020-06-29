namespace Stationery.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// HttpUriFactory
    /// </summary>
    public static class HttpUriFactory
    {
        #region Methods

        /// <summary>
        /// Creates the authentication login.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetAuthLoginRequest(string apiUrl)
        {
            return apiUrl + "/auth/login";
        }

        /// <summary>
        /// Creates the authentication login.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetAuthRequest(string apiUrl)
        {
            return apiUrl + "/auth";
        }

        /// <summary>
        /// Creates the site request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetNewUserRequest(string apiUrl)
        {
            return apiUrl + "/auth/create";
        }

        /// <summary>
        /// Creates the site request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetUpdateUserRequest(string apiUrl)
        {
            return apiUrl + "/auth/updateUser";
        }

        /// <summary>
        /// Creates the user and roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static string GetUserDetailsRequest(string apiUrl, int userId)
        {
            return apiUrl + "/auth/userDetails/" + userId;
        }

        /// <summary>
        /// Creates the user request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="usernameSearch">The username search.</param>
        /// <param name="emailSearch">The email search.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static string GetUserRequest(string apiUrl)
        {
            return apiUrl + "/auth/user";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetUsersRequest(string apiUrl)
        {
            return apiUrl + "/auth/users";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetRolesRequest(string apiUrl)
        {
            return apiUrl + "/auth/roles";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetProductsRequest(string apiUrl)
        {
            return apiUrl + "/product/products";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetNewProductRequest(string apiUrl)
        {
            return apiUrl + "/product/create";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetProductByIdRequest(string apiUrl, int id)
        {
            return apiUrl + "/product/productById/" + id;
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetUpdateProductRequest(string apiUrl)
        {
            return apiUrl + "/product/update";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetDeleteProductRequest(string apiUrl)
        {
            return apiUrl + "/product/delete";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetStocksRequest(string apiUrl)
        {
            return apiUrl + "/stock/stocks";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetNewStockRequest(string apiUrl)
        {
            return apiUrl + "/stock/create";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetStockByIdRequest(string apiUrl, int id)
        {
            return apiUrl + "/stock/stocktById/" + id;
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetUpdateStockRequest(string apiUrl)
        {
            return apiUrl + "/stock/update";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetDeleteStockRequest(string apiUrl)
        {
            return apiUrl + "/stock/delete";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetOrdersRequest(string apiUrl)
        {
            return apiUrl + "/order/orders";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetNewOrderRequest(string apiUrl)
        {
            return apiUrl + "/order/create";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetOrderByIdRequest(string apiUrl, int id)
        {
            return apiUrl + "/order/orderById/" + id;
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetUpdateOrderRequest(string apiUrl)
        {
            return apiUrl + "/order/update";
        }

        /// <summary>
        /// Creates the roles request.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <returns></returns>
        public static string GetDeleteOrderRequest(string apiUrl)
        {
            return apiUrl + "/order/delete";
        }

        #endregion Methods
    }
}