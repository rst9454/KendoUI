using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace KendoUIApp1.Extension.ExtraOrdinary
{
    public static class QueryableExtensions
    {
        private static DataSourceResult ToDataSourceResult(this DataTableWrapper enumerable, DataSourceRequest request)
        {
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                DataTable dataTable2 = enumerable.Table;
                list.SelectMemberDescriptors().Each(delegate (FilterDescriptor f)
                {
                    f.MemberType = GetFieldByTypeFromDataColumn(dataTable2, f.Member);
                });
            }

            List<GroupDescriptor> list2 = new List<GroupDescriptor>();
            if (request.Groups != null)
            {
                list2.AddRange(request.Groups);
            }

            if (list2.Any())
            {
                DataTable dataTable = enumerable.Table;
                list2.Each(delegate (GroupDescriptor g)
                {
                    g.MemberType = GetFieldByTypeFromDataColumn(dataTable, g.Member);
                });
            }

            DataSourceResult dataSourceResult = enumerable.AsEnumerable().ToDataSourceResult(request);
            dataSourceResult.Data = dataSourceResult.Data.SerializeToDictionary(enumerable.Table);
            return dataSourceResult;
        }

        private static Type GetFieldByTypeFromDataColumn(DataTable dataTable, string memberName)
        {
            if (!dataTable.Columns.Contains(memberName))
            {
                return null;
            }

            return dataTable.Columns[memberName].DataType;
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty resullt.
        //
        // Parameters:
        //   dataTable:
        //     An instance of System.Data.DataTable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this DataTable dataTable, DataSourceRequest request)
        {
            return dataTable.WrapAsEnumerable().ToDataSourceResult(request);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty resullt.
        //
        // Parameters:
        //   dataTable:
        //     An instance of System.Data.DataTable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this DataTable dataTable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => dataTable.ToDataSourceResult(request));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty resullt.
        //
        // Parameters:
        //   dataTable:
        //     An instance of System.Data.DataTable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this DataTable dataTable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => dataTable.ToDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IEnumerable enumerable, DataSourceRequest request)
        {
            return enumerable.AsQueryable().ToDataSourceResult(request);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().ToDataSourceResult(request, modelState);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, modelState));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IQueryable queryable, DataSourceRequest request)
        {
            return queryable.ToDataSourceResult(request, null);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.AsQueryable().CreateDataSourceResult(request, null, selector);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, selector));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return enumerable.AsQueryable().CreateDataSourceResult(request, modelState, selector);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => enumerable.ToDataSourceResult(request, modelState, selector));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => enumerable.ToDataSourceResult(request, modelState, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.CreateDataSourceResult(request, null, selector);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, selector));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return enumerable.CreateDataSourceResult(request, modelState, selector);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, modelState, selector));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, modelState, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.DataSourceResult object, which contains the processed data after
        //     paging, sorting, filtering and grouping are applied.
        public static DataSourceResult ToDataSourceResult(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return queryable.CreateDataSourceResult<object, object>(request, modelState, null);
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateDataSourceResultAsync(() => queryable.ToDataSourceResult(request, modelState));
        }

        //
        // Summary:
        //     Applies paging, sorting, filtering and grouping using the information from the
        //     DataSourceRequest object. If the collection is already paged, the method returns
        //     an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.DataSourceResult object, which contains the processed
        //     data after paging, sorting, filtering and grouping are applied. It can be called
        //     with the "await" keyword for asynchronous operation.
        public static Task<DataSourceResult> ToDataSourceResultAsync(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateDataSourceCancellableResultAsync(() => queryable.ToDataSourceResult(request, modelState), cancellationToken);
        }

        private static DataSourceResult CreateDataSourceResult<TModel, TResult>(this IQueryable queryable, DataSourceRequest request, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            DataSourceResult dataSourceResult = new DataSourceResult();
            IQueryable queryable2 = queryable;
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                queryable2 = queryable2.Where(list);
            }

            List<SortDescriptor> sort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                sort.AddRange(request.Sorts);
            }

            List<SortDescriptor> temporarySortDescriptors = new List<SortDescriptor>();
            IList<GroupDescriptor> list2 = new List<GroupDescriptor>();
            if (request.Groups != null)
            {
                list2.AddRange(request.Groups);
            }

            List<AggregateDescriptor> aggregates = new List<AggregateDescriptor>();
            if (request.Aggregates != null)
            {
                aggregates.AddRange(request.Aggregates);
            }

            if (aggregates.Any() && !request.IncludeSubGroupCount)
            {
                IQueryable queryable3 = queryable2.AsQueryable();
                IQueryable source = queryable3;
                if (list.Any())
                {
                    source = queryable3.Where(list);
                }

                dataSourceResult.AggregateResults = source.Aggregate(aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                if (list2.Any() && aggregates.Any())
                {
                    list2.Each(delegate (GroupDescriptor g)
                    {
                        g.AggregateFunctions.AddRange(aggregates.SelectMany((AggregateDescriptor a) => a.Aggregates));
                    });
                }
            }

            if (!request.GroupPaging || !request.Groups.Any())
            {
                dataSourceResult.Total = queryable2.Count();
            }

            if (!sort.Any() && queryable.Provider.IsEntityFrameworkProvider())
            {
                SortDescriptor item = new SortDescriptor
                {
                    Member = queryable.ElementType.FirstSortableProperty()
                };
                sort.Add(item);
                temporarySortDescriptors.Add(item);
            }

            if (list2.Any())
            {
                list2.Reverse().Each(delegate (GroupDescriptor groupDescriptor)
                {
                    SortDescriptor item2 = new SortDescriptor
                    {
                        Member = groupDescriptor.Member,
                        SortDirection = groupDescriptor.SortDirection
                    };
                    sort.Insert(0, item2);
                    temporarySortDescriptors.Add(item2);
                });
            }

            if (sort.Any())
            {
                queryable2 = queryable2.Sort(sort);
            }

            IQueryable notPagedData = queryable2;
            if (!request.GroupPaging || !request.Groups.Any())
            {
                queryable2 = queryable2.Page(request.Page - 1, request.PageSize);
            }
            else if (request.GroupPaging && !request.Groups.Any())
            {
                queryable2 = queryable2.Skip(request.Skip).Take(request.Take);
            }

            if (list2.Any())
            {
                queryable2 = queryable2.GroupBy(notPagedData, list2, !request.GroupPaging);
                if (request.GroupPaging)
                {
                    dataSourceResult.Total = queryable2.Count();
                    queryable2 = queryable2.Skip(request.Skip).Take(request.Take);
                }
            }

            if (!request.IncludeSubGroupCount)
            {
                dataSourceResult.Data = queryable2.Execute(selector);
            }

            if (modelState != null && !modelState.IsValid)
            {
                dataSourceResult.Errors = modelState.SerializeErrors();
            }

            temporarySortDescriptors.Each(delegate (SortDescriptor sortDescriptor)
            {
                sort.Remove(sortDescriptor);
            });
            return dataSourceResult;
        }

        private static Task<DataSourceResult> CreateDataSourceResultAsync(Func<DataSourceResult> expression)
        {
            return Task.Factory.StartNew(expression, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        private static Task<DataSourceResult> CreateDataSourceCancellableResultAsync(Func<DataSourceResult> expression, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(expression, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        private static IQueryable CallQueryableMethod(this IQueryable source, string methodName, LambdaExpression selector)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new Type[2]
            {
                source.ElementType,
                selector.Body.Type
            }, source.Expression, Expression.Quote(selector)));
        }

        //
        // Summary:
        //     Sorts the elements of a sequence using the specified sort descriptors.
        //
        // Parameters:
        //   source:
        //     A sequence of values to sort.
        //
        //   sortDescriptors:
        //     The sort descriptors used for sorting.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are sorted according to a sortDescriptors.
        public static IQueryable Sort(this IQueryable source, IEnumerable<SortDescriptor> sortDescriptors)
        {
            return new SortDescriptorCollectionExpressionBuilder(source, sortDescriptors).Sort();
        }

        //
        // Summary:
        //     Pages through the elements of a sequence until the specified pageIndex using
        //     pageSize.
        //
        // Parameters:
        //   source:
        //     A sequence of values to page.
        //
        //   pageIndex:
        //     Index of the page.
        //
        //   pageSize:
        //     Size of the page.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are at the specified pageIndex.
        public static IQueryable Page(this IQueryable source, int pageIndex, int pageSize)
        {
            IQueryable source2 = source;
            source2 = source2.Skip(pageIndex * pageSize);
            if (pageSize > 0)
            {
                source2 = source2.Take(pageSize);
            }

            return source2;
        }

        //
        // Summary:
        //     Projects each element of a sequence into a new form.
        //
        // Parameters:
        //   source:
        //     A sequence of values to project.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are the result of invoking a projection
        //     selector on each element of source.
        public static IQueryable Select(this IQueryable source, LambdaExpression selector)
        {
            return source.CallQueryableMethod("Select", selector);
        }

        //
        // Summary:
        //     Groups the elements of a sequence according to a specified key selector function.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable whose elements to group.
        //
        //   keySelector:
        //     A function to extract the key for each element.
        //
        // Returns:
        //     An System.Linq.IQueryable with System.Linq.IGrouping`2 items, whose elements
        //     contains a sequence of objects and a key.
        public static IQueryable GroupBy(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("GroupBy", keySelector);
        }

        //
        // Summary:
        //     Sorts the elements of a sequence in ascending order according to a key.
        //
        // Parameters:
        //   source:
        //     A sequence of values to order.
        //
        //   keySelector:
        //     A function to extract a key from an element.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are sorted according to a key.
        public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("OrderBy", keySelector);
        }

        //
        // Summary:
        //     Sorts the elements of a sequence in descending order according to a key.
        //
        // Parameters:
        //   source:
        //     A sequence of values to order.
        //
        //   keySelector:
        //     A function to extract a key from an element.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are sorted in descending order according
        //     to a key.
        public static IQueryable OrderByDescending(this IQueryable source, LambdaExpression keySelector)
        {
            return source.CallQueryableMethod("OrderByDescending", keySelector);
        }

        //
        // Summary:
        //     Calls Kendo.Mvc.Extensions.QueryableExtensions.OrderBy(System.Linq.IQueryable,System.Linq.Expressions.LambdaExpression)
        //     or Kendo.Mvc.Extensions.QueryableExtensions.OrderByDescending(System.Linq.IQueryable,System.Linq.Expressions.LambdaExpression)
        //     depending on the sortDirection.
        //
        // Parameters:
        //   source:
        //     The source.
        //
        //   keySelector:
        //     The key selector.
        //
        //   sortDirection:
        //     The sort direction.
        //
        // Returns:
        //     An System.Linq.IQueryable whose elements are sorted according to a key.
        public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector, ListSortDirection? sortDirection)
        {
            if (sortDirection.HasValue)
            {
                if (sortDirection.Value == ListSortDirection.Ascending)
                {
                    return source.OrderBy(keySelector);
                }

                return source.OrderByDescending(keySelector);
            }

            return source;
        }

        //
        // Summary:
        //     Groups the elements of a sequence according to a specified groupDescriptors.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable whose elements to group.
        //
        //   groupDescriptors:
        //     The group descriptors used for grouping.
        //
        // Returns:
        //     An System.Linq.IQueryable with Kendo.Mvc.Infrastructure.IGroup items, whose elements
        //     contains a sequence of objects and a key.
        public static IQueryable GroupBy(this IQueryable source, IEnumerable<GroupDescriptor> groupDescriptors)
        {
            return source.GroupBy(source, groupDescriptors, includeParents: true);
        }

        public static IQueryable GroupBy(this IQueryable source, IQueryable notPagedData, IEnumerable<GroupDescriptor> groupDescriptors, bool includeParents)
        {
            GroupDescriptorCollectionExpressionBuilder groupDescriptorCollectionExpressionBuilder = new GroupDescriptorCollectionExpressionBuilder(source, groupDescriptors, notPagedData, includeParents);
            groupDescriptorCollectionExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
            return groupDescriptorCollectionExpressionBuilder.CreateQuery();
        }

        //
        // Summary:
        //     Calculates the results of given aggregates functions on a sequence of elements.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable whose elements will be used for aggregate calculation.
        //
        //   aggregateFunctions:
        //     The aggregate functions.
        //
        // Returns:
        //     Collection of Kendo.Mvc.Infrastructure.AggregateResults calculated for each function.
        public static AggregateResultCollection Aggregate(this IQueryable source, IEnumerable<AggregateFunction> aggregateFunctions)
        {
            List<AggregateFunction> list = aggregateFunctions.ToList();
            if (list.Count > 0)
            {
                QueryableAggregatesExpressionBuilder queryableAggregatesExpressionBuilder = new QueryableAggregatesExpressionBuilder(source, list);
                queryableAggregatesExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
                {
                    IEnumerator enumerator = queryableAggregatesExpressionBuilder.CreateQuery().GetEnumerator();
                    try
                    {
                        if (enumerator.MoveNext())
                        {
                            return ((AggregateFunctionsGroup)enumerator.Current).GetAggregateResults(list);
                        }
                    }
                    finally
                    {
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }

            return new AggregateResultCollection();
        }

        //
        // Summary:
        //     Filters a sequence of values based on a predicate.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable to filter.
        //
        //   predicate:
        //     A function to test each element for a condition.
        //
        // Returns:
        //     An System.Linq.IQueryable that contains elements from the input sequence that
        //     satisfy the condition specified by predicate.
        public static IQueryable Where(this IQueryable source, Expression predicate)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[1] { source.ElementType }, source.Expression, Expression.Quote(predicate)));
        }

        //
        // Summary:
        //     Filters a sequence of values based on a collection of Kendo.Mvc.IFilterDescriptor.
        //
        // Parameters:
        //   source:
        //     The source.
        //
        //   filterDescriptors:
        //     The filter descriptors.
        //
        // Returns:
        //     An System.Linq.IQueryable that contains elements from the input sequence that
        //     satisfy the conditions specified by each filter descriptor in filterDescriptors.
        public static IQueryable Where(this IQueryable source, IEnumerable<IFilterDescriptor> filterDescriptors)
        {
            if (filterDescriptors.Any())
            {
                FilterDescriptorCollectionExpressionBuilder filterDescriptorCollectionExpressionBuilder = new FilterDescriptorCollectionExpressionBuilder(Expression.Parameter(source.ElementType, "item"), filterDescriptors);
                filterDescriptorCollectionExpressionBuilder.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
                LambdaExpression predicate = filterDescriptorCollectionExpressionBuilder.CreateFilterExpression();
                return source.Where(predicate);
            }

            return source;
        }

        //
        // Summary:
        //     Returns a specified number of contiguous elements from the start of a sequence.
        //
        // Parameters:
        //   source:
        //     The sequence to return elements from.
        //
        //   count:
        //     The number of elements to return.
        //
        // Returns:
        //     An System.Linq.IQueryable that contains the specified number of elements from
        //     the start of source.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     source is null.
        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(count)));
        }

        //
        // Summary:
        //     Bypasses a specified number of elements in a sequence and then returns the remaining
        //     elements.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable to return elements from.
        //
        //   count:
        //     The number of elements to skip before returning the remaining elements.
        //
        // Returns:
        //     An System.Linq.IQueryable that contains elements that occur after the specified
        //     index in the input sequence.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     source is null.
        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(count)));
        }

        //
        // Summary:
        //     Returns the number of elements in a sequence.
        //
        // Parameters:
        //   source:
        //     The System.Linq.IQueryable that contains the elements to be counted.
        //
        // Returns:
        //     The number of elements in the input sequence.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     source is null.
        public static int Count(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Provider.Execute<int>(Expression.Call(typeof(Queryable), "Count", new Type[1] { source.ElementType }, source.Expression));
        }

        //
        // Summary:
        //     Returns the element at a specified index in a sequence.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable to return an element from.
        //
        //   index:
        //     The zero-based index of the element to retrieve.
        //
        // Returns:
        //     The element at the specified position in source.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     source is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than zero.
        public static object ElementAt(this IQueryable source, int index)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return source.Provider.Execute(Expression.Call(typeof(Queryable), "ElementAt", new Type[1] { source.ElementType }, source.Expression, Expression.Constant(index)));
        }

        //
        // Summary:
        //     Produces the set union of two sequences by using the default equality comparer.
        //
        // Parameters:
        //   source:
        //     An System.Linq.IQueryable whose distinct elements form the first set for the
        //     union.
        //
        //   second:
        //     An System.Linq.IQueryable whose distinct elements form the first set for the
        //     union.
        //
        // Returns:
        //     An System.Linq.IQueryable that contains the elements from both input sequences,
        //     excluding duplicates.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     source is null.
        public static IQueryable Union(this IQueryable source, IQueryable second)
        {
            return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Union", new Type[1] { source.ElementType }, source.Expression, second.Expression));
        }

        private static IEnumerable Execute<TModel, TResult>(this IQueryable source, Func<TModel, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source is DataTableWrapper)
            {
                return source;
            }

            Type elementType = source.ElementType;
            if (selector != null)
            {
                List<AggregateFunctionsGroup> list = new List<AggregateFunctionsGroup>();
                if (elementType == typeof(AggregateFunctionsGroup))
                {
                    foreach (AggregateFunctionsGroup item in source)
                    {
                        item.Items = item.Items.AsQueryable().Execute(selector);
                        list.Add(item);
                    }

                    return list;
                }

                List<TResult> list2 = new List<TResult>();
                {
                    foreach (TModel item2 in source)
                    {
                        list2.Add(selector(item2));
                    }

                    return list2;
                }
            }

            IList list3 = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            foreach (object item3 in source)
            {
                list3.Add(item3);
            }

            return list3;
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult(this IEnumerable enumerable, DataSourceRequest request)
        {
            return enumerable.AsQueryable().ToTreeDataSourceResult(request, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<object, object, object, object>(request, null, null, modelState, null, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, modelState));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync(this IEnumerable enumerable, DataSourceRequest request, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.ToTreeDataSourceResult<TModel, object, object, TResult>(request, null, null, selector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return enumerable.ToTreeDataSourceResult<TModel, object, object, TResult>(request, null, null, selector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return queryable.ToTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IQueryable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return enumerable.CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   queryable:
        //     An instance of System.Linq.IQueryable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => queryable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, null, null, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return queryable.AsQueryable().ToTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return enumerable.AsQueryable().CreateTreeDataSourceResult<TModel, T1, T2, TModel>(request, idSelector, parentIDSelector, modelState, null, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, null, selector, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, null);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector), cancellationToken);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed data
        //     after sorting, filtering and grouping are applied.
        public static TreeDataSourceResult ToTreeDataSourceResult<TModel, T1, T2, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return queryable.AsQueryable().CreateTreeDataSourceResult(request, idSelector, parentIDSelector, modelState, selector, rootSelector);
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector)
        {
            return CreateTreeDataSourceResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector));
        }

        //
        // Summary:
        //     Applies sorting, filtering and grouping using the information from the DataSourceRequest
        //     object. If the collection is already paged, the method returns an empty result.
        //
        // Parameters:
        //   enumerable:
        //     An instance of System.Collections.IEnumerable.
        //
        //   request:
        //     An instance of Kendo.Mvc.UI.DataSourceRequest.
        //
        //   idSelector:
        //     The lambda which configures the selector of the ID field.
        //
        //   parentIDSelector:
        //     The lambda which configures the parent selector.
        //
        //   rootSelector:
        //     The lambda which configures the root selector.
        //
        //   modelState:
        //     An instance of System.Web.Mvc.ModelStateDictionary.
        //
        //   selector:
        //     A projection function to apply to each element.
        //
        //   cancellationToken:
        //     An instance of System.Threading.CancellationToken.
        //
        // Returns:
        //     A Task of Kendo.Mvc.UI.TreeDataSourceResult object, which contains the processed
        //     data after sorting, filtering and grouping are applied. It can be called with
        //     the "await" keyword for asynchronous operation.
        public static Task<TreeDataSourceResult> ToTreeDataSourceResultAsync<TModel, T1, T2, TResult>(this IEnumerable<TModel> enumerable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, Expression<Func<TModel, bool>> rootSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, CancellationToken cancellationToken)
        {
            return CreateTreeDataSourceCancellableResultAsync(() => enumerable.ToTreeDataSourceResult(request, idSelector, parentIDSelector, rootSelector, modelState, selector), cancellationToken);
        }

        private static Task<TreeDataSourceResult> CreateTreeDataSourceResultAsync(Func<TreeDataSourceResult> expression)
        {
            return Task.Factory.StartNew(expression, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        private static Task<TreeDataSourceResult> CreateTreeDataSourceCancellableResultAsync(Func<TreeDataSourceResult> expression, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(expression, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        private static TreeDataSourceResult CreateTreeDataSourceResult<TModel, T1, T2, TResult>(this IQueryable queryable, DataSourceRequest request, Expression<Func<TModel, T1>> idSelector, Expression<Func<TModel, T2>> parentIDSelector, ModelStateDictionary modelState, Func<TModel, TResult> selector, Expression<Func<TModel, bool>> rootSelector)
        {
            TreeDataSourceResult treeDataSourceResult = new TreeDataSourceResult();
            IQueryable queryable2 = queryable;
            List<IFilterDescriptor> list = new List<IFilterDescriptor>();
            if (request.Filters != null)
            {
                list.AddRange(request.Filters);
            }

            if (list.Any())
            {
                queryable2 = queryable2.Where(list);
                queryable2 = queryable2.ParentsRecursive<TModel>(queryable, idSelector, parentIDSelector);
            }

            IQueryable allData = queryable2;
            if (rootSelector != null)
            {
                queryable2 = queryable2.Where(rootSelector);
            }

            List<SortDescriptor> list2 = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                list2.AddRange(request.Sorts);
            }

            List<AggregateDescriptor> list3 = new List<AggregateDescriptor>();
            if (request.Aggregates != null)
            {
                list3.AddRange(request.Aggregates);
            }

            if (list3.Any())
            {
                foreach (IGrouping<T2, TModel> item in queryable2.GroupBy(parentIDSelector))
                {
                    treeDataSourceResult.AggregateResults.Add(Convert.ToString(item.Key), item.AggregateForLevel(allData, list3, idSelector, parentIDSelector));
                }
            }

            treeDataSourceResult.Total = queryable2.Count();
            if (list2.Any())
            {
                queryable2 = queryable2.Sort(list2);
            }

            treeDataSourceResult.Data = queryable2.Execute(selector);
            if (modelState != null && !modelState.IsValid)
            {
                treeDataSourceResult.Errors = modelState.SerializeErrors();
            }

            return treeDataSourceResult;
        }
    }
}