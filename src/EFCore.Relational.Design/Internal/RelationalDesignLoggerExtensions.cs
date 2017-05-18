// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class RelationalDesignLoggerExtensions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void MissingSchemaWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string schemaName)
        {
            var definition = RelationalDesignStrings.LogMissingSchema;

            definition.Log(diagnostics, schemaName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        SchemaName = schemaName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SequenceTypeNotSupportedWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string sequenceName,
            [CanBeNull] string dataTypeName)
        {
            var definition = RelationalDesignStrings.LogBadSequenceType;

            definition.Log(diagnostics, sequenceName, dataTypeName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        SequenceName = sequenceName,
                        DataTypeName = dataTypeName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void UnableToGenerateEntityTypeWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName)
        {
            var definition = RelationalDesignStrings.LogUnableToGenerateEntityType;

            definition.Log(diagnostics, tableName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        TableName = tableName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ColumnTypeNotMappedWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string columnName,
            [CanBeNull] string dataTypeName)
        {
            var definition = RelationalDesignStrings.LogCannotFindTypeMappingForColumn;

            definition.Log(diagnostics, columnName, dataTypeName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        ColumnName = columnName,
                        DataTypeName = dataTypeName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void MissingPrimaryKeyWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName)
        {
            var definition = RelationalDesignStrings.LogMissingPrimaryKey;

            definition.Log(diagnostics, tableName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        TableName = tableName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void PrimaryKeyColumnsNotMappedWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName,
            [NotNull] IList<string> unmappedColumnNames)
        {
            var definition = RelationalDesignStrings.LogPrimaryKeyErrorPropertyNotFound;

            definition.Log(
                diagnostics,
                tableName,
                string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, unmappedColumnNames));

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        TableName = tableName,
                        UnmappedColumnNames = unmappedColumnNames
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyReferencesNotMappedTableWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string foreignKeyName,
            [NotNull] string principalTableName)
        {
            var definition = RelationalDesignStrings.LogForeignKeyScaffoldErrorPrincipalTableScaffoldingError;

            definition.Log(diagnostics, foreignKeyName, principalTableName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        ForeignKeyName = foreignKeyName,
                        PrincipalTableName = principalTableName
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyReferencesMissingPrincipalKeyWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string foreignKeyName,
            [CanBeNull] string principalEntityTypeName,
            [NotNull] IList<string> principalColumnNames)
        {
            var definition = RelationalDesignStrings.LogForeignKeyScaffoldErrorPrincipalKeyNotFound;

            definition.Log(
                diagnostics,
                foreignKeyName,
                string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, principalColumnNames),
                principalEntityTypeName);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        ForeignKeyName = foreignKeyName,
                        PrincipalEntityTypeName = principalEntityTypeName,
                        PrincipalColumnNames = principalColumnNames
                    });
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyPrincipalEndContainsNullableColumnsWarning(
            [NotNull] this IDiagnosticsLogger<LoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string foreignKeyName,
            [CanBeNull] string indexName,
            [CanBeNull] IList<string> nullablePropertyNames)
        {
            var definition = RelationalDesignStrings.LogForeignKeyPrincipalEndContainsNullableColumns;

            definition.Log(
                diagnostics,
                foreignKeyName,
                indexName,
                nullablePropertyNames.Aggregate((a, b) => a + "," + b));

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new
                    {
                        ForeignKeyName = foreignKeyName,
                        IndexName = indexName,
                        NullablePropertyNames = nullablePropertyNames
                    });
            }
        }
    }
}
