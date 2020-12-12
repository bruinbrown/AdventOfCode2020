module Day4

open System
open System.IO

let private requiredKeys = ["byr";"iyr";"eyr";"hgt";"hcl";"ecl";"pid";]

let private parseInput filePath =
    let currentPassport, allPassports =
        filePath
        |> File.ReadAllLines
        |> Seq.fold (fun (currentPassport, allPassports) currentLine ->
            if String.IsNullOrWhiteSpace currentLine then (Map.empty, currentPassport::allPassports)
            else
                let kvps = currentLine.Split(' ')
                let currentPassport =
                    kvps
                    |> Seq.fold (fun s t ->
                        let kvp = t.Split(':')
                        s |> Map.add kvp.[0] kvp.[1]) currentPassport
                currentPassport, allPassports
        ) (Map.empty, [])
    currentPassport::allPassports

let private isPassportValid passport =
    requiredKeys
    |> List.forall (fun t -> passport |> Map.containsKey t)

let private calculate = parseInput >> Seq.filter isPassportValid >> Seq.length

let run filePath =
    filePath
    |> calculate
    |> printfn "Valid passprts: %i"

let private rules =
    let eyeColours = [ "amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth" ] |> Set.ofList
    let validHex = [for i = '0' to '9' do yield char i; for i = 'a' to 'f' do yield i] |> Set.ofList
    [ "byr", fun v -> let i = Int32.Parse(v) in i >= 1920 && i <= 2002
      "iyr", fun v -> let i = Int32.Parse(v) in i >= 2010 && i <= 2020
      "eyr", fun v -> let i = Int32.Parse(v) in i >= 2020 && i <= 2030
      "ecl", fun v -> eyeColours |> Set.contains v
      "pid", fun v -> v.Length = 9 && v |> String.forall Char.IsNumber
      "hcl", fun v -> v.[0] = '#' && (v.TrimStart('#') |> String.forall (fun t -> Set.contains t validHex))
      "hgt", fun v -> 
        if v.EndsWith("cm") then let len = Int32.Parse(v.TrimEnd([|'c'; 'm'|])) in len <= 193 && len >= 150
        elif v.EndsWith("in") then let len = Int32.Parse(v.TrimEnd([| 'i'; 'n' |])) in len >= 59 && len <= 76
        else false 
    ]

let private isPassportValidForRules (passport: Map<string, string>) =
    rules
    |> List.forall (fun (key, rule) -> rule passport.[key])

let private calculatePart2 = parseInput >> Seq.filter isPassportValid >> Seq.filter isPassportValidForRules >> Seq.length

let runPart2 filePath =
    filePath
    |> calculatePart2
    |> printfn "Valid passports: %i"