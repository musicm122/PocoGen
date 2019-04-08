module PocoGen.Core.Models

open System

type LogMessage =
    | Debug of string
    | Info of string
    | Failure of string

type ConnectionString = String

type FileOutputPath = String

type ConnectionTestState =
    | Pass
    | Fail
    | WIP
    | NotStarted

type ConnectionTestResult =
    { State : ConnectionTestState
      Message : string }

type Database =
    { Name : string }

type Table =
    { name : string
      database : Database }

type Query =
    { database : Database
      sql : string
      connectionString : ConnectionString }

type Language =
    | CSharp
    | VbNet

type GetSetValue =
    | Get
    | Set
    | GetAndSet

type AccessModifier =
    | Public
    | Private
    | Internal
    | Protected

type ValueType =
    { name : string
      value : Type
      isNullable : bool }

type Property =
    { typeDetail : ValueType
      isNullable : bool
      columnName : string
      access : AccessModifier
      GetSet : GetSetValue }

type ClassObject =
    { className : string
      access : AccessModifier
      properties : Property []
      language : Language }

type ClassResult =
    { raw : string }

type RawRowData =
    { IsNullable : bool
      DataType : string
      ColumnName : string }
