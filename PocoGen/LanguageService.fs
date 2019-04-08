module LanguageService

open Models

type BuildClass = string ->RawRowData[] -> ClassObject
type CreateClass = Query -> string -> ClassResult