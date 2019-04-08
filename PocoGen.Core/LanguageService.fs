module PocoGen.Core.LanguageService

open PocoGen.Core.Models

type BuildClass = string ->RawRowData[] -> ClassObject
type CreateClass = Query -> string -> ClassResult