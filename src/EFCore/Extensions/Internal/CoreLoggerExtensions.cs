// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Remotion.Linq;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class CoreLoggerExtensions
    {
        private const int QueryModelStringLengthLimit = 100;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SaveChangesFailed(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Update> diagnostics,
            [NotNull] DbContext context,
            [NotNull] Exception exception)
        {
            var definition = CoreStrings.LogExceptionDuringSaveChanges;

            definition.Log(
                diagnostics,
                context.GetType(), Environment.NewLine, exception,
                exception);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new DbContextErrorEventData(
                        definition,
                        SaveChangesFailed,
                        context,
                        exception));
            }
        }

        private static string SaveChangesFailed(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<Type, string, Exception>)definition;
            var p = (DbContextErrorEventData)payload;
            return d.GenerateMessage(p.Context.GetType(), Environment.NewLine, p.Exception);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void DuplicateDietInstanceWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Update> diagnostics,
            [NotNull] string diet1,
            [NotNull] string diet2)
        {
            var definition = CoreStrings.LogDuplicateDietInstance;

            definition.Log(diagnostics, diet1, diet2);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        Diet1 = diet1,
                        Diet2 = diet2
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void QueryIterationFailed(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] Type contextType,
            [NotNull] Exception exception)
        {
            var definition = CoreStrings.LogExceptionDuringQueryIteration;

            definition.Log(
                diagnostics,
                contextType, Environment.NewLine, exception,
                exception);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new DbContextTypeErrorEventData(
                        definition,
                        QueryIterationFailed,
                        contextType,
                        exception));
            }
        }

        private static string QueryIterationFailed(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<Type, string, Exception>)definition;
            var p = (DbContextTypeErrorEventData)payload;
            return d.GenerateMessage(p.ContextType, Environment.NewLine, p.Exception);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void QueryModelCompiling(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] QueryModel queryModel)
        {
            var definition = CoreStrings.LogCompilingQueryModel;

            // Checking for enabled here to avoid printing query model if not needed.
            if (diagnostics.GetLogBehavior(definition.EventId, definition.Level) != WarningBehavior.Ignore)
            {
                definition.Log(
                    diagnostics,
                    Environment.NewLine, queryModel.Print());
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new QueryModelEventData(
                        definition,
                        QueryModelCompiling,
                        queryModel));
            }
        }

        private static string QueryModelCompiling(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string, string>)definition;
            var p = (QueryModelEventData)payload;
            return d.GenerateMessage(Environment.NewLine, p.QueryModel.Print());
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void RowLimitingOperationWithoutOrderByWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] QueryModel queryModel)
        {
            var definition = CoreStrings.LogRowLimitingOperationWithoutOrderBy;

            // Checking for enabled here to avoid printing query model if not needed.
            if (diagnostics.GetLogBehavior(definition.EventId, definition.Level) != WarningBehavior.Ignore)
            {
                definition.Log(
                    diagnostics,
                    queryModel.Print(removeFormatting: true, characterLimit: QueryModelStringLengthLimit));
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new QueryModelEventData(
                        definition,
                        RowLimitingOperationWithoutOrderByWarning,
                        queryModel));
            }
        }

        private static string RowLimitingOperationWithoutOrderByWarning(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (QueryModelEventData)payload;
            return d.GenerateMessage(p.QueryModel.Print(removeFormatting: true, characterLimit: QueryModelStringLengthLimit));
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void FirstWithoutOrderByAndFilterWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] QueryModel queryModel)
        {
            var definition = CoreStrings.LogFirstWithoutOrderByAndFilter;

            // Checking for enabled here to avoid printing query model if not needed.
            if (diagnostics.GetLogBehavior(definition.EventId, definition.Level) != WarningBehavior.Ignore)
            {
                definition.Log(
                    diagnostics,
                    queryModel.Print(removeFormatting: true, characterLimit: QueryModelStringLengthLimit));
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new QueryModelEventData(
                        definition,
                        FirstWithoutOrderByAndFilterWarning,
                        queryModel));
            }
        }

        private static string FirstWithoutOrderByAndFilterWarning(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (QueryModelEventData)payload;
            return d.GenerateMessage(p.QueryModel.Print(removeFormatting: true, characterLimit: QueryModelStringLengthLimit));
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void QueryModelOptimized(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] QueryModel queryModel)
        {
            var definition = CoreStrings.LogOptimizedQueryModel;

            // Checking for enabled here to avoid printing query model if not needed.
            if (diagnostics.GetLogBehavior(definition.EventId, definition.Level) != WarningBehavior.Ignore)
            {
                definition.Log(
                    diagnostics,
                    Environment.NewLine, queryModel.Print());
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new QueryModelEventData(
                        definition,
                        QueryModelOptimized,
                        queryModel));
            }
        }

        private static string QueryModelOptimized(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string, string>)definition;
            var p = (QueryModelEventData)payload;
            return d.GenerateMessage(Environment.NewLine, p.QueryModel.Print());
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void NavigationIncluded(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] string includeSpecification)
        {
            var definition = CoreStrings.LogIncludingNavigation;

            definition.Log(
                diagnostics,
                includeSpecification);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new IncludeEventData(
                        definition,
                        NavigationIncluded,
                        includeSpecification));
            }
        }

        private static string NavigationIncluded(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (IncludeEventData)payload;
            return d.GenerateMessage(p.IncludeSpecification);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void QueryExecutionPlanned(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] IExpressionPrinter expressionPrinter,
            [NotNull] Expression queryExecutorExpression)
        {
            var definition = CoreStrings.LogQueryExecutionPlanned;

            // Checking for enabled here to avoid printing query model if not needed.
            if (diagnostics.GetLogBehavior(definition.EventId, definition.Level) != WarningBehavior.Ignore)
            {
                definition.Log(
                    diagnostics,
                    expressionPrinter.Print(queryExecutorExpression));
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new QueryExpressionEventData(
                        definition,
                        QueryExecutionPlanned,
                        queryExecutorExpression,
                        expressionPrinter));
            }
        }

        private static string QueryExecutionPlanned(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (QueryExpressionEventData)payload;
            return d.GenerateMessage(p.ExpressionPrinter.Print(p.Expression));
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SensitiveDataLoggingEnabledWarning<TLoggerCategory>(
            [NotNull] this IDiagnosticsLogger<TLoggerCategory> diagnostics)
            where TLoggerCategory : LoggerCategory<TLoggerCategory>, new()
        {
            var definition = CoreStrings.LogSensitiveDataLoggingEnabled;

            definition.Log(diagnostics);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new EventDataBase(
                        definition,
                        (d, p) => ((EventDefinition)d).GenerateMessage()));
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void IncludeIgnoredWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] string includeSpecification)
        {
            var definition = CoreStrings.LogIgnoredInclude;

            definition.Log(
                diagnostics,
                includeSpecification);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new IncludeEventData(
                        definition,
                        IncludeIgnoredWarning,
                        includeSpecification));
            }
        }

        private static string IncludeIgnoredWarning(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (IncludeEventData)payload;
            return d.GenerateMessage(p.IncludeSpecification);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void PossibleUnintendedCollectionNavigationNullComparisonWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] string navigationPath)
        {
            var definition = CoreStrings.LogPossibleUnintendedCollectionNavigationNullComparison;

            definition.Log(
                diagnostics,
                navigationPath);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new NavigationPathEventData(
                        definition,
                        PossibleUnintendedCollectionNavigationNullComparisonWarning,
                        navigationPath));
            }
        }

        private static string PossibleUnintendedCollectionNavigationNullComparisonWarning(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (NavigationPathEventData)payload;
            return d.GenerateMessage(p.NavigationPath);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void PossibleUnintendedReferenceComparisonWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Query> diagnostics,
            [NotNull] Expression left,
            [NotNull] Expression right)
        {
            var definition = CoreStrings.LogPossibleUnintendedReferenceComparison;

            definition.Log(
                diagnostics,
                left,
                right);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new BinaryExpressionEventData(
                        definition,
                        PossibleUnintendedReferenceComparisonWarning,
                        left,
                        right));
            }
        }

        private static string PossibleUnintendedReferenceComparisonWarning(EventDefinitionBase definition, EventDataBase payload)
        {
            var d = (EventDefinition<object, object>)definition;
            var p = (BinaryExpressionEventData)payload;
            return d.GenerateMessage(p.Left, p.Right);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ServiceProviderCreated(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Infrastructure> diagnostics,
            [NotNull] IServiceProvider serviceProvider)
        {
            var definition = CoreStrings.LogServiceProviderCreated;

            definition.Log(diagnostics);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new ServiceProviderEventData(
                        definition,
                        (d, p) => ((EventDefinition)d).GenerateMessage(),
                        serviceProvider));
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ManyServiceProvidersCreatedWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Infrastructure> diagnostics,
            [NotNull] ICollection<IServiceProvider> serviceProviders)
        {
            var definition = CoreStrings.LogManyServiceProvidersCreated;

            definition.Log(diagnostics);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new ServiceProvidersEventData(
                        definition,
                        (d, p) => ((EventDefinition)d).GenerateMessage(),
                        serviceProviders));
            }
        }
    }
}
